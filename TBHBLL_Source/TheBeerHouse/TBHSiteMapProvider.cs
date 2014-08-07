namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration.Provider;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Permissions;
    using System.Web;
    using System.Web.Caching;
    using TheBeerHouse.BLL;

    /// <summary> 
    /// Summary description for TBHSiteMapProvider 
    /// </summary>
    [SqlClientPermission(SecurityAction.Demand, Unrestricted=true)]
    public class TBHSiteMapProvider : StaticSiteMapProvider
    {
        private bool _2005dependency = false;
        private const string _cacheDependencyName = "__SiteMapCacheDependency";
        private const string _errmsg1 = "Missing node ID";
        private const string _errmsg2 = "Duplicate node ID";
        private const string _errmsg3 = "Missing parent ID";
        private const string _errmsg4 = "Invalid parent ID";
        private const string _errmsg5 = "Empty or missing connectionStringName";
        private const string _errmsg6 = "Missing connection string";
        private const string _errmsg7 = "Empty connection string";
        private const string _errmsg8 = "Invalid sqlCacheDependency";
        private int _indexDesc;
        private int _indexID;
        private int _indexParent;
        private int _indexRoles;
        private int _indexTitle;
        private int _indexUrl;
        private readonly object _lock = RuntimeHelpers.GetObjectValue(new object());
        private Dictionary<int, SiteMapNode> _nodes = new Dictionary<int, SiteMapNode>(0x10);
        private SiteMapNode _root;
        protected List<SiteMapInfo> lSiteMapNodes;

        [CompilerGenerated, DebuggerStepThrough]
        private static bool _Lambda$__16(SiteMapInfo lSiteMapNode)
        {
            return (lSiteMapNode.Parent == 0);
        }

        private void AddChildNodes(SiteMapNode vParentNode, int SiteMapId)
        {
            List<SiteMapInfo>.Enumerator VB$t_struct$L0;
            _Closure$__30 $VB$Closure_ClosureVariable_70_8 = new _Closure$__30();
            $VB$Closure_ClosureVariable_70_8.$VB$Local_SiteMapId = SiteMapId;
            List<SiteMapInfo> lChildNodes = this.lSiteMapNodes.Where<SiteMapInfo>(new Func<SiteMapInfo, bool>($VB$Closure_ClosureVariable_70_8._Lambda$__17)).ToList<SiteMapInfo>();
            try
            {
                VB$t_struct$L0 = lChildNodes.GetEnumerator();
                while (VB$t_struct$L0.MoveNext())
                {
                    SiteMapInfo lChildNode = VB$t_struct$L0.Current;
                    SiteMapNode lnode = this.CreateSiteMapNodeFromSiteMapEntity(lChildNode);
                    this.AddNode(lnode, vParentNode);
                    this.AddChildNodes(lnode, lChildNode.SiteMapId);
                }
            }
            finally
            {
                VB$t_struct$L0.Dispose();
            }
        }

        public override SiteMapNode BuildSiteMap()
        {
            object VB$t_ref$L0 = this._lock;
            ObjectFlowControl.CheckForSyncLockOnValueType(VB$t_ref$L0);
            lock (VB$t_ref$L0)
            {
                if (this._root == null)
                {
                    using (SiteMapRepository lSiteMapContext = new SiteMapRepository())
                    {
                        this.lSiteMapNodes = lSiteMapContext.GetSiteMapNodes();
                        if (this.lSiteMapNodes.Count > 0)
                        {
                            SiteMapInfo node = this.lSiteMapNodes.Where<SiteMapInfo>(new Func<SiteMapInfo, bool>(TBHSiteMapProvider._Lambda$__16)).FirstOrDefault<SiteMapInfo>();
                            if (!Information.IsNothing(node))
                            {
                                this._root = this.CreateSiteMapNodeFromSiteMapEntity(node);
                                this.AddNode(this._root, null);
                                this.AddChildNodes(this._root, node.SiteMapId);
                            }
                        }
                    }
                }
                return this._root;
            }
        }

        private SiteMapNode CreateSiteMapNodeFromSiteMapEntity(SiteMapInfo node)
        {
            string VB$LW$t_string$S0 = node.Roles;
            string roles = (VB$LW$t_string$S0 != null) ? VB$LW$t_string$S0 : null;
            string[] rolelist = null;
            if (!string.IsNullOrEmpty(roles))
            {
                rolelist = roles.Split(new char[] { ',', ';' }, 0x200);
            }
            SiteMapNode _node = new SiteMapNode(this, node.SiteMapId.ToString(), node.URL, node.Title, node.Description, rolelist, null, null, null);
            this._nodes.Add(node.SiteMapId, _node);
            return _node;
        }

        private SiteMapNode GetParentNodeFromSiteMapEntity(SiteMapInfo node)
        {
            int pid = node.Parent;
            if (!this._nodes.ContainsKey(pid))
            {
                throw new ProviderException("Invalid parent ID");
            }
            return this._nodes[pid];
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            object VB$t_ref$L0 = this._lock;
            ObjectFlowControl.CheckForSyncLockOnValueType(VB$t_ref$L0);
            lock (VB$t_ref$L0)
            {
                this.BuildSiteMap();
                return this._root;
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "TBHSiteMapProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "SQL site map provider");
            }
            base.Initialize(name, config);
            if (config["securityTrimmingEnabled"] != null)
            {
                config.Remove("securityTrimmingEnabled");
            }
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!string.IsNullOrEmpty(attr))
                {
                    throw new ProviderException("Unrecognized attribute: " + attr);
                }
            }
        }

        private void OnSiteMapChanged(string key, object item, CacheItemRemovedReason reason)
        {
            object VB$t_ref$L0 = this._lock;
            ObjectFlowControl.CheckForSyncLockOnValueType(VB$t_ref$L0);
            lock (VB$t_ref$L0)
            {
                if ((((key == "__SiteMapCacheDependency") && (reason == CacheItemRemovedReason.DependencyChanged)) ? 1 : 0) != 0)
                {
                    this.Clear();
                    this._nodes.Clear();
                    this._root = null;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__30
        {
            public int $VB$Local_SiteMapId;

            [DebuggerNonUserCode]
            public _Closure$__30()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__30(TBHSiteMapProvider._Closure$__30 other)
            {
                if (other != null)
                {
                    this.$VB$Local_SiteMapId = other.$VB$Local_SiteMapId;
                }
            }

            [DebuggerStepThrough, CompilerGenerated]
            public bool _Lambda$__17(SiteMapInfo lChildren)
            {
                return (lChildren.Parent == this.$VB$Local_SiteMapId);
            }
        }
    }
}

