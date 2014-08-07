using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Caching;
using BLL;

[SqlClientPermission(SecurityAction.Demand, Unrestricted = true)]
public class TBHSiteMapProvider : StaticSiteMapProvider
{
    private const string _cacheDependencyName = "__SiteMapCacheDependency";
    private const string _errmsg4 = "Invalid parent ID";
    private readonly object _lock = new object();
    private readonly Dictionary<int, SiteMapNode> _nodes = new Dictionary<int, SiteMapNode>(16);

    private SiteMapNode _root;
    protected List<SiteMapInfo> lSiteMapNodes;

    public override void Initialize(string name, NameValueCollection config)
    {
        // Verify that config isn't null 
        if (config == null)
        {
            throw new ArgumentNullException("config");
        }

        // Assign the provider a default name if it doesn't have one 
        if (String.IsNullOrEmpty(name))
        {
            name = "TBHSiteMapProvider";
        }

        // Add a default "description" attribute to config if the 
        // attribute doesn’t exist or is empty 
        if (string.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "SQL site map provider");
        }

        // Call the base class's Initialize method 
        base.Initialize(name, config);

        if (config["securityTrimmingEnabled"] != null)
        {
            config.Remove("securityTrimmingEnabled");
        }

        // Throw an exception if unrecognized attributes remain 
        if (config.Count > 0)
        {
            string attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
            {
                throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }
    }

    public override SiteMapNode BuildSiteMap()
    {
        lock (_lock)
        {
            // Return immediately if this method has been called before 
            if (_root != null)
            {
                return _root;
            }

            using (var lSiteMapContext = new SiteMapRepository())
            {
                lSiteMapNodes = lSiteMapContext.GetSiteMapNodes();

                if (lSiteMapNodes.Count > 0)
                {
                    SiteMapInfo node = (from lSiteMapNode in lSiteMapNodes
                                        where lSiteMapNode.Parent == 0
                                        select lSiteMapNode).FirstOrDefault();

                    if ((node != null))
                    {
                        _root = CreateSiteMapNodeFromSiteMapEntity(node);
                        AddNode(_root, null);

                        AddChildNodes(_root, node.SiteMapId);
                    }
                }
            }

            return _root;
        }
    }

    private void AddChildNodes(SiteMapNode vParentNode, int SiteMapId)
    {
        List<SiteMapInfo> lChildNodes = (from lChildren in lSiteMapNodes
                                         where lChildren.Parent == SiteMapId
                                         select lChildren).ToList();

        foreach (SiteMapInfo lChildNode in lChildNodes)
        {
            SiteMapNode lnode = CreateSiteMapNodeFromSiteMapEntity(lChildNode);
            AddNode(lnode, vParentNode);


            AddChildNodes(lnode, lChildNode.SiteMapId);
        }
    }

    protected override SiteMapNode GetRootNodeCore()
    {
        lock (_lock)
        {
            BuildSiteMap();
            return _root;
        }
    }

    // Helper methods 
    private SiteMapNode CreateSiteMapNodeFromSiteMapEntity(SiteMapInfo node)
    {
        string roles = node.Roles ?? null;

        // If roles were specified, turn the list into a string array 
        string[] rolelist = null;
        if (!String.IsNullOrEmpty(roles))
        {
            rolelist = roles.Split(new[] {',', ';'}, 512);
        }

        // Create a SiteMapNode 
        var _node = new SiteMapNode(this, node.SiteMapId.ToString(), node.URL, node.Title, node.Description, rolelist,
                                    null, null, null);

        // Record the node in the _nodes dictionary 
        _nodes.Add(node.SiteMapId, _node);

        // Return the node 

        return _node;
    }

    private SiteMapNode GetParentNodeFromSiteMapEntity(SiteMapInfo node)
    {
        // Get the parent ID from the DataReader 
        int pid = node.Parent;

        // Make sure the parent ID is valid 
        if (!_nodes.ContainsKey(pid))
        {
            throw new ProviderException(_errmsg4);
        }

        // Return the parent SiteMapNode 
        return _nodes[pid];
    }

    private void OnSiteMapChanged(string key, object item, CacheItemRemovedReason reason)
    {
        lock (_lock)
        {
            if (key == _cacheDependencyName && reason == CacheItemRemovedReason.DependencyChanged)
            {
                // Refresh the site map 
                Clear();
                _nodes.Clear();
                _root = null;
            }
        }
    }
}