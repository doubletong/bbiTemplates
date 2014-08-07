namespace TheBeerHouse
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Xml.Linq;
    using TheBeerHouse.BLL.Articles;
    using TheBeerHouse.My;

    public class RSSFeed : BaseHandler
    {
        private void CreateRSSFeed()
        {
            this.Response.ContentType = "application/xml";
            using (ArticleRepository lArticlectx = new ArticleRepository())
            {
                _Closure$__37 $VB$Closure_ClosureVariable_FEEFED_0 = new _Closure$__37();
                $VB$Closure_ClosureVariable_FEEFED_0.$VB$Local_Settings = Helpers.Settings;
                XDocument VB$t_ref$S0 = new XDocument(new XDeclaration("1.0", "utf-8", null), null);
                XElement VB$t_ref$S1 = new XElement(XName.Get("rss", ""));
                VB$t_ref$S1.Add(new XAttribute(XName.Get("version", ""), "2.0"));
                VB$t_ref$S1.Add(InternalXmlHelper.CreateNamespaceAttribute(XName.Get("geo", "http://www.w3.org/2000/xmlns/"), XNamespace.Get("http://www.w3.org/2003/01/geo/wgs84_pos#")));
                XElement VB$t_ref$S2 = new XElement(XName.Get("channel", ""));
                XElement VB$t_ref$S3 = new XElement(XName.Get("title", ""));
                VB$t_ref$S3.Add("The Beer House Articles");
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S3 = new XElement(XName.Get("link", ""));
                VB$t_ref$S3.Add("http://www.TheBeerHouseBook.com/");
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S3 = new XElement(XName.Get("description", ""));
                VB$t_ref$S3.Add("RSS Feed containing The Beer House News Articles.");
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S3 = new XElement(XName.Get("docs", ""));
                VB$t_ref$S3.Add("http://www.rssboard.org/rss-specification");
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S3 = new XElement(XName.Get("image", ""));
                XElement VB$t_ref$S4 = new XElement(XName.Get("link", ""));
                VB$t_ref$S4.Add("http://www.TheBeerHouseBook.com/");
                VB$t_ref$S3.Add(VB$t_ref$S4);
                VB$t_ref$S4 = new XElement(XName.Get("title", ""));
                VB$t_ref$S4.Add("The Beer House Articles");
                VB$t_ref$S3.Add(VB$t_ref$S4);
                VB$t_ref$S4 = new XElement(XName.Get("url", ""));
                VB$t_ref$S4.Add("http://www.TheBeerHouseBook.com/Images/tbh-logo.png");
                VB$t_ref$S3.Add(VB$t_ref$S4);
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S2.Add(lArticlectx.GetRSSArticles().AsEnumerable<Article>().Select<Article, XElement>(new Func<Article, XElement>($VB$Closure_ClosureVariable_FEEFED_0._Lambda$__22)));
                VB$t_ref$S1.Add(VB$t_ref$S2);
                VB$t_ref$S0.Add(VB$t_ref$S1);
                XDocument xRss = VB$t_ref$S0;
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

        [CompilerGenerated]
        internal class _Closure$__37
        {
            public TheBeerHouseSection $VB$Local_Settings;

            [DebuggerNonUserCode]
            public _Closure$__37()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__37(RSSFeed._Closure$__37 other)
            {
                if (other != null)
                {
                    this.$VB$Local_Settings = other.$VB$Local_Settings;
                }
            }

            [CompilerGenerated, DebuggerStepThrough]
            public XElement _Lambda$__22(Article lArticle)
            {
                XElement VB$t_ref$S0 = new XElement(XName.Get("item", ""));
                XElement VB$t_ref$S1 = new XElement(XName.Get("title", ""));
                VB$t_ref$S1.Add(lArticle.Title);
                VB$t_ref$S0.Add(VB$t_ref$S1);
                VB$t_ref$S1 = new XElement(XName.Get("link", ""));
                VB$t_ref$S1.Add(Helpers.SEOFriendlyURL(this.$VB$Local_Settings.Articles.URLIndicator + "/" + lArticle.Title, ".aspx"));
                VB$t_ref$S0.Add(VB$t_ref$S1);
                VB$t_ref$S1 = new XElement(XName.Get("description", ""));
                VB$t_ref$S1.Add(lArticle.Abstract);
                VB$t_ref$S0.Add(VB$t_ref$S1);
                return VB$t_ref$S0;
            }
        }
    }
}

