namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Xml.Linq;
    using TheBeerHouse.BLL;

    public class SiteMapsHandler : IHttpHandler
    {
        private HttpContext _BaseContext;

        [DebuggerStepThrough, CompilerGenerated]
        private static XElement _Lambda$__15(SiteMapInfo lSiteMapNode)
        {
            XElement VB$t_ref$S0 = new XElement(XName.Get("url", ""));
            XElement VB$t_ref$S1 = new XElement(XName.Get("loc", ""));
            VB$t_ref$S1.Add(lSiteMapNode.URL);
            VB$t_ref$S0.Add(VB$t_ref$S1);
            VB$t_ref$S1 = new XElement(XName.Get("lastmod", ""));
            VB$t_ref$S1.Add(lSiteMapNode.DateUpdated);
            VB$t_ref$S0.Add(VB$t_ref$S1);
            VB$t_ref$S1 = new XElement(XName.Get("changefreq", ""));
            VB$t_ref$S1.Add("weekly");
            VB$t_ref$S0.Add(VB$t_ref$S1);
            VB$t_ref$S1 = new XElement(XName.Get("priority", ""));
            VB$t_ref$S1.Add("0.8");
            VB$t_ref$S0.Add(VB$t_ref$S1);
            return VB$t_ref$S0;
        }

        private void CreateSiteMap()
        {
            List<SiteMapInfo> lsiteMapNodes;
            this.Response.ContentType = "application/xml";
            using (SiteMapRepository siteMaprpt = new SiteMapRepository())
            {
                lsiteMapNodes = siteMaprpt.GetSiteMapNodes();
            }
            if (!Information.IsNothing(lsiteMapNodes))
            {
                XDocument VB$t_ref$S0 = new XDocument(new XDeclaration("1.0", "UTF-8", null), null);
                XElement VB$t_ref$S1 = new XElement(XName.Get("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9"));
                VB$t_ref$S1.Add(new XAttribute(XName.Get("xmlns", ""), "http://www.sitemaps.org/schemas/sitemap/0.9"));
                VB$t_ref$S1.Add(lsiteMapNodes.AsEnumerable<SiteMapInfo>().Select<SiteMapInfo, XElement>(new Func<SiteMapInfo, XElement>(SiteMapsHandler._Lambda$__15)));
                VB$t_ref$S0.Add(VB$t_ref$S1);
                XDocument xSiteMap = VB$t_ref$S0;
                this.Response.Write(xSiteMap.ToString());
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            this.BaseContext = context;
            this.CreateSiteMap();
            this.Response.Flush();
            this.Response.End();
        }

        public HttpContext BaseContext
        {
            get
            {
                return this._BaseContext;
            }
            set
            {
                this._BaseContext = value;
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public HttpRequest Request
        {
            get
            {
                return this.BaseContext.Request;
            }
        }

        public HttpResponse Response
        {
            get
            {
                return this.BaseContext.Response;
            }
        }

        public bool System.Web.IHttpHandler.IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}

