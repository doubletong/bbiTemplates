namespace TheBeerHouse
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Xml.Linq;
    using TheBeerHouse.BLL.Forums;

    public class RSSForum : IHttpHandler
    {
        private HttpContext _BaseContext;

        private void CreateRSSFeed()
        {
            this.Response.ContentType = "application/xml";
            using (PostsRepository lPostrpt = new PostsRepository())
            {
                _Closure$__20 $VB$Closure_ClosureVariable_FEEFED_0 = new _Closure$__20();
                TheBeerHouseSection Settings = Helpers.Settings;
                $VB$Closure_ClosureVariable_FEEFED_0.$VB$Local_forumID = 0;
                if (!string.IsNullOrEmpty(this.Request.QueryString["ForumID"]))
                {
                    $VB$Closure_ClosureVariable_FEEFED_0.$VB$Local_forumID = int.Parse(this.Request.QueryString["ForumID"]);
                }
                string sortExpr = string.Empty;
                if (!string.IsNullOrEmpty(this.Request.QueryString["SortExpr"]))
                {
                    sortExpr = this.Request.QueryString["SortExpr"];
                }
                using (ForumsRepository lForumrpt = new ForumsRepository())
                {
                    Forum lForum = lForumrpt.GetForumById($VB$Closure_ClosureVariable_FEEFED_0.$VB$Local_forumID);
                    XDocument VB$t_ref$S0 = new XDocument(new XDeclaration("1.0", "utf-8", null), null);
                    XElement VB$t_ref$S1 = new XElement(XName.Get("rss", ""));
                    VB$t_ref$S1.Add(new XAttribute(XName.Get("version", ""), "2.0"));
                    VB$t_ref$S1.Add(new XAttribute(XName.Get("geo", "http://www.w3.org/2000/xmlns/"), "http://www.w3.org/2003/01/geo/wgs84_pos#"));
                    XElement VB$t_ref$S2 = new XElement(XName.Get("channel", ""));
                    XElement VB$t_ref$S3 = new XElement(XName.Get("title", ""));
                    VB$t_ref$S3.Add("The Beer House ");
                    VB$t_ref$S3.Add(lForum.Title);
                    VB$t_ref$S2.Add(VB$t_ref$S3);
                    VB$t_ref$S3 = new XElement(XName.Get("link", ""));
                    VB$t_ref$S3.Add("http://www.TheBeerHouseBook.com/");
                    VB$t_ref$S2.Add(VB$t_ref$S3);
                    VB$t_ref$S3 = new XElement(XName.Get("description", ""));
                    VB$t_ref$S3.Add("RSS Feed containing The Beer House Forum, ");
                    VB$t_ref$S3.Add(lForum.Title);
                    VB$t_ref$S3.Add(",  Posts.");
                    VB$t_ref$S2.Add(VB$t_ref$S3);
                    VB$t_ref$S3 = new XElement(XName.Get("docs", ""));
                    VB$t_ref$S3.Add("http://www.rssboard.org/rss-specification");
                    VB$t_ref$S2.Add(VB$t_ref$S3);
                    VB$t_ref$S3 = new XElement(XName.Get("image", ""));
                    XElement VB$t_ref$S4 = new XElement(XName.Get("link", ""));
                    VB$t_ref$S4.Add("http://www.TheBeerHouseBook.com/");
                    VB$t_ref$S3.Add(VB$t_ref$S4);
                    VB$t_ref$S4 = new XElement(XName.Get("title", ""));
                    VB$t_ref$S4.Add("The Beer House - ");
                    VB$t_ref$S4.Add(lForum.Title);
                    VB$t_ref$S4.Add("  Forum");
                    VB$t_ref$S3.Add(VB$t_ref$S4);
                    VB$t_ref$S4 = new XElement(XName.Get("url", ""));
                    VB$t_ref$S4.Add("http://www.TheBeerHouseBook.com/Images/tbh-logo.png");
                    VB$t_ref$S3.Add(VB$t_ref$S4);
                    VB$t_ref$S2.Add(VB$t_ref$S3);
                    VB$t_ref$S2.Add(lPostrpt.GetRSSForum($VB$Closure_ClosureVariable_FEEFED_0.$VB$Local_forumID).AsEnumerable<Post>().Select<Post, XElement>(new Func<Post, XElement>($VB$Closure_ClosureVariable_FEEFED_0._Lambda$__7)));
                    VB$t_ref$S1.Add(VB$t_ref$S2);
                    VB$t_ref$S0.Add(VB$t_ref$S1);
                    XDocument xRss = VB$t_ref$S0;
                    this.Response.Write(xRss.ToString());
                }
            }
            this.Response.Flush();
            this.Response.End();
        }

        public void ProcessRequest(HttpContext context)
        {
            this.BaseContext = context;
            this.CreateRSSFeed();
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
                return false;
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
                return false;
            }
        }

        [CompilerGenerated]
        internal class _Closure$__20
        {
            public int $VB$Local_forumID;

            [DebuggerNonUserCode]
            public _Closure$__20()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__20(RSSForum._Closure$__20 other)
            {
                if (other != null)
                {
                    this.$VB$Local_forumID = other.$VB$Local_forumID;
                }
            }

            [CompilerGenerated, DebuggerStepThrough]
            public XElement _Lambda$__7(Post lPost)
            {
                XElement VB$t_ref$S0 = new XElement(XName.Get("item", ""));
                XElement VB$t_ref$S1 = new XElement(XName.Get("title", ""));
                VB$t_ref$S1.Add(lPost.Title);
                VB$t_ref$S0.Add(VB$t_ref$S1);
                VB$t_ref$S1 = new XElement(XName.Get("link", ""));
                VB$t_ref$S1.Add(Path.Combine(Helpers.WebRoot, string.Format("ShowThread.aspx?threadid={0}", this.$VB$Local_forumID)));
                VB$t_ref$S0.Add(VB$t_ref$S1);
                VB$t_ref$S1 = new XElement(XName.Get("description", ""));
                VB$t_ref$S1.Add(lPost.Body);
                VB$t_ref$S0.Add(VB$t_ref$S1);
                return VB$t_ref$S0;
            }
        }
    }
}

