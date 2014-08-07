namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Xml.Linq;
    using TheBeerHouse.BLL.Store;

    public class ProductsRSS : BaseHandler
    {
        private void CreateRSSFeed()
        {
            this.Response.ContentType = "application/xml";
            using (ProductsRepository lProductsrpt = new ProductsRepository())
            {
                _Closure$__48 $VB$Closure_ClosureVariable_FEEFED_0 = new _Closure$__48();
                $VB$Closure_ClosureVariable_FEEFED_0.$VB$Local_Settings = Helpers.Settings;
                int lDepartmentID = 0;
                if (!string.IsNullOrEmpty(this.Request.QueryString["DepartmentID"]))
                {
                    lDepartmentID = int.Parse(this.Request.QueryString["DepartmentID"]);
                }
                string sortExpr = string.Empty;
                if (!string.IsNullOrEmpty(this.Request.QueryString["SortExpr"]))
                {
                    sortExpr = this.Request.QueryString["SortExpr"];
                }
                string lRSSTitle = "The Beer House Products";
                using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
                {
                    Department lDepartment = lDepartmentrpt.GetDepartmentById(lDepartmentID);
                    if (!Information.IsNothing(lDepartment))
                    {
                        lRSSTitle = string.Format("The Beer House {0} Products", lDepartment.Title);
                    }
                    else
                    {
                        lRSSTitle = "The Beer House Products";
                    }
                }
                XDocument VB$t_ref$S0 = new XDocument(new XDeclaration("1.0", "utf-8", null), null);
                XElement VB$t_ref$S1 = new XElement(XName.Get("rss", ""));
                VB$t_ref$S1.Add(new XAttribute(XName.Get("version", ""), "2.0"));
                VB$t_ref$S1.Add(new XAttribute(XName.Get("geo", "http://www.w3.org/2000/xmlns/"), "http://www.w3.org/2003/01/geo/wgs84_pos#"));
                XElement VB$t_ref$S2 = new XElement(XName.Get("channel", ""));
                XElement VB$t_ref$S3 = new XElement(XName.Get("title", ""));
                VB$t_ref$S3.Add(lRSSTitle);
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S3 = new XElement(XName.Get("link", ""));
                VB$t_ref$S3.Add("http://www.TheBeerHouseBook.com/");
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S3 = new XElement(XName.Get("description", ""));
                VB$t_ref$S3.Add("RSS Feed containing ");
                VB$t_ref$S3.Add(lRSSTitle);
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S3 = new XElement(XName.Get("docs", ""));
                VB$t_ref$S3.Add("http://www.rssboard.org/rss-specification");
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S3 = new XElement(XName.Get("image", ""));
                XElement VB$t_ref$S4 = new XElement(XName.Get("link", ""));
                VB$t_ref$S4.Add("http://www.TheBeerHouseBook.com/");
                VB$t_ref$S3.Add(VB$t_ref$S4);
                VB$t_ref$S4 = new XElement(XName.Get("title", ""));
                VB$t_ref$S4.Add(lRSSTitle);
                VB$t_ref$S3.Add(VB$t_ref$S4);
                VB$t_ref$S4 = new XElement(XName.Get("url", ""));
                VB$t_ref$S4.Add("http://www.TheBeerHouseBook.com/Images/tbh-logo.png");
                VB$t_ref$S3.Add(VB$t_ref$S4);
                VB$t_ref$S2.Add(VB$t_ref$S3);
                VB$t_ref$S2.Add(lProductsrpt.GetRSSProducts(lDepartmentID).AsEnumerable<Product>().Select<Product, XElement>(new Func<Product, XElement>($VB$Closure_ClosureVariable_FEEFED_0._Lambda$__23)));
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
        internal class _Closure$__48
        {
            public TheBeerHouseSection $VB$Local_Settings;

            [DebuggerNonUserCode]
            public _Closure$__48()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__48(ProductsRSS._Closure$__48 other)
            {
                if (other != null)
                {
                    this.$VB$Local_Settings = other.$VB$Local_Settings;
                }
            }

            [DebuggerStepThrough, CompilerGenerated]
            public XElement _Lambda$__23(Product lProduct)
            {
                XElement VB$t_ref$S0 = new XElement(XName.Get("item", ""));
                XElement VB$t_ref$S1 = new XElement(XName.Get("title", ""));
                VB$t_ref$S1.Add(lProduct.Title);
                VB$t_ref$S0.Add(VB$t_ref$S1);
                VB$t_ref$S1 = new XElement(XName.Get("link", ""));
                VB$t_ref$S1.Add(Helpers.SEOFriendlyURL(Path.Combine(this.$VB$Local_Settings.Store.ProductURLIndicator, lProduct.Title), ".aspx"));
                VB$t_ref$S0.Add(VB$t_ref$S1);
                VB$t_ref$S1 = new XElement(XName.Get("description", ""));
                VB$t_ref$S1.Add(lProduct.Description);
                VB$t_ref$S0.Add(VB$t_ref$S1);
                return VB$t_ref$S0;
            }
        }
    }
}

