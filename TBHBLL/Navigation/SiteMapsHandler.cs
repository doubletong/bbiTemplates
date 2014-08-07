using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Linq;
using BLL;

namespace BBICMSBLL.Navigation
{
    public class SiteMapsHandler : IHttpHandler
    {
        // Fields
        private HttpContext _BaseContext;

        // Properties
        public HttpContext BaseContext
        {
            get { return _BaseContext; }
            set { _BaseContext = value; }
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public HttpRequest Request
        {
            get { return BaseContext.Request; }
        }

        public HttpResponse Response
        {
            get { return BaseContext.Response; }
        }

        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            BaseContext = context;
            CreateSiteMap();
            Response.Flush();
            Response.End();
        }

        bool IHttpHandler.IsReusable
        {
            get { return true; }
        }

        #endregion

        private void CreateSiteMap()
        {
            List<SiteMapInfo> lsiteMapNodes;
            Response.ContentType = "application/xml";
            using (var siteMaprpt = new SiteMapRepository())
            {
                lsiteMapNodes = siteMaprpt.GetSiteMapNodes();
            }
            if (lsiteMapNodes != null)
            {
                var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                        new XElement("rss",
                                                     new XAttribute("version", "2.0"),
                                                     new XElement("channel",
                                                                  new XElement("title", "RSS Channel Title"),
                                                                  new XElement("description", "RSS Channel Description."),
                                                                  new XElement("link",
                                                                               "http://BeerHouse.ExtremeWebWorks.com"),
                                                                  new XElement("item",
                                                                               new XElement("title",
                                                                                            "First article title"),
                                                                               new XElement("description",
                                                                                            "First Article Description"),
                                                                               new XElement("pubDate",
                                                                                            DateTime.Now.ToUniversalTime
                                                                                                ()),
                                                                               new XElement("guid", Guid.NewGuid())),
                                                                  new XElement("item",
                                                                               new XElement("title",
                                                                                            "Second article title"),
                                                                               new XElement("description",
                                                                                            "Second Article Description"),
                                                                               new XElement("pubDate",
                                                                                            DateTime.Now.ToUniversalTime
                                                                                                ()),
                                                                               new XElement("guid", Guid.NewGuid()))
                                                         )
                                            )
                    );

                Response.Write(doc.ToString());
            }
        }
    }
}