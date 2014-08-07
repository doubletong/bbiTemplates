using System.Linq;
using System;
using System.Web;
using System.Xml.Linq;
using BBICMS.Articles;
using System.Collections.Generic;
using BBICMS.BLL.Articles;

namespace BBICMS
{

    public class RSSFeed : BaseHandler
    {
        private void CreateRSSFeed()
        {
            BBICMSSection Settings = Helpers.Settings;
            this.Response.ContentType = "application/xml";
            using (ArticleRepository lArticlectx = new ArticleRepository())
            {

                List<Article> lArticles = lArticlectx.GetActiveArticles();

                var xRss = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                        new XElement("rss",
                                            new XAttribute("version", "2.0"),
                                            new XElement("channel",
                                                new XElement("title", "The Beer House Articles"),
                                                new XElement("link", "http://www.BBICMSBook.com/"),
                                                new XElement("description",
                                                    "RSS Feed containing The Beer House News Articles."),

                    from item in lArticles
                        select
        	                new XElement("item",
	                             new XElement("title", item.Title),
	                             new XElement("description", item.Abstract),
	                             new XElement("link", Helpers.SEOFriendlyURL(Settings.Articles.URLIndicator + "/" + item.Title, ".aspx")),
	                             new XElement("pubDate", item.ReleaseDate.ToString())

                   ) 
                  )
                )
              );

                this.Response.Write(xRss.ToString());
            }
            this.Response.Flush();
            this.Response.End();
        }

        public override void ProcessRequest(HttpContext context)
        {
            this.BaseContext = context;
            this.CreateRSSFeed();
        }

    }
}

