using System.Linq;
using System.Xml.Linq;

partial class Controls_RSSReader : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        string rssURL = ResolveClientUrl("~/BBICMS.rss");
        //XDocument rssFeed = XDocument.Load(rssURL);

        //var rssItems = (from rss in rssFeed.Descendants("item")
        //               select new
        //                {
        //                    Title = rss.Element("title").Value,
        //                    Url = rss.Element("url").Value,
        //                    Description = rss.Element("description").Value
        //                }).Take(5).ToList();

        //lvRSSReader.DataSource = rssItems;
        //lvRSSReader.DataBind();

    }

}


public class BlogPost
{

    public string Title = string.Empty;
    public string Url = string.Empty;
    public string Description = string.Empty;

}

