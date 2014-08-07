using System;
using System.Web;
using System.Xml.Linq;
using BBICMS.Forums;
using BBICMS;

public class RSSForum : IHttpHandler
{

    #region " Properties "

    private HttpContext _BaseContext;
    public HttpContext BaseContext
    {
        get { return _BaseContext; }
        set { _BaseContext = value; }
    }


    public HttpResponse Response
    {
        get { return BaseContext.Response; }
    }

    public HttpRequest Request
    {
        get { return BaseContext.Request; }
    }


    public bool IsReusable
    {
        get { return false; }
    }

    #endregion

    public void ProcessRequest(HttpContext context)
    {

        BaseContext = context;

        CreateRSSFeed();
    }

    private void CreateRSSFeed()
    {

        Response.ContentType = "application/xml";

        using (PostsRepository lPostrpt = new PostsRepository())
        {

            BBICMSSection Settings = Helpers.Settings;

            int forumID = 0;
            // if a ForumID param is passed on the querystring, load the Forum with that ID,
            // and use its title for the RSS's title
            if (!string.IsNullOrEmpty(Request.QueryString["ForumID"]))
            {
                forumID = int.Parse(Request.QueryString["ForumID"]);
            }

            string sortExpr = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["SortExpr"]))
            {
                sortExpr = Request.QueryString["SortExpr"];
            }

            using (ForumsRepository lForumrpt = new ForumsRepository())
            {

                Forum lForum = lForumrpt.GetForumById(forumID);

                XDocument xRss = default(XDocument);

                throw new NotImplementedException("You need to come back and create the RSS Feed XML.");

            }
        }
    }
}