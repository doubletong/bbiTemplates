using System.Linq;
using System.Web;
using System.Xml.Linq;
using BBICMS;
using BBICMS.Store;

public class ProductsRSS : BaseHandler
{
    public override void ProcessRequest(HttpContext context)
    {
        BaseContext = context;
        CreateRSSFeed();
    }

    private void CreateRSSFeed()
    {
        Response.ContentType = "application/xml";

        using (var lProductsrpt = new ProductsRepository())
        {
            BBICMSSection Settings = Helpers.Settings;

            int lDepartmentID = 0;
            // if a DepartmentID param is passed on the querystring, load the Products from that department,
            // and use the Department's title for the RSS's title
            if (!string.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
            {
                lDepartmentID = int.Parse(Request.QueryString["DepartmentID"]);
            }

            string sortExpr = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["SortExpr"]))
            {
                sortExpr = Request.QueryString["SortExpr"];
            }

            string lRSSTitle = "The Beer House Products";

            using (var lDepartmentrpt = new DepartmentRepository())
            {
                Department lDepartment = lDepartmentrpt.GetDepartmentById(lDepartmentID);

                if ((lDepartment != null))
                {
                    lRSSTitle = string.Format("The Beer House {0} Products", lDepartment.Title);
                }
                else
                {
                    lRSSTitle = "The Beer House Products";
                }

                var xRss = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                         new XElement("rss",
                                                      new XAttribute("version", "2.0"),
                                                      new XElement("channel",
                                                                   new XElement("title", lRSSTitle),
                                                                   new XElement("link",
                                                                                Helpers.Settings.SiteDomainName),
                                                                   new XElement("description",
                                                                                "RSS Feed containing The Beer House Products."),
                                                                   from item in
                                                                       lProductsrpt.GetProductsByDepartment(
                                                                       lDepartment.DepartmentID)
                                                                   select
                                                                       new XElement("item",
                                                                                    new XElement("title", item.Title),
                                                                                    new XElement("description",
                                                                                                 item.Description),
                                                                                    new XElement("link",Helpers.Settings.SiteDomainName +
                                                                                                     "/Store/Product_" + item.ProductID.ToString() + "/"),
                                                                                    new XElement("pubDate",
                                                                                                 item.AddedDate.
                                                                                                     ToShortDateString())
                                                                       )
                                                          )
                                             )
                    );

                Response.Write(xRss.ToString());

            }
        }

        Response.Flush();
        Response.End();

    }

}