using System;
using System.Web;

namespace Controls
{
    partial class FooterCopyright : System.Web.UI.UserControl
    {
        #region " Properties "

        public virtual string FormatString
        {
            get
            {
                string obj1 = (null != Application["FormatString"]) ? Application["FormatString"].ToString() : String.Empty;
                if (obj1 != null)
                {
                    return obj1;
                }
                return string.Empty;
            }
            set { Application["FormatString"] = value; }
        }

        public string StartYear
        {
            get
            {
                string text1 = (null != Application["StartYear"]) ? Application["StartYear"].ToString() : String.Empty;
                if (text1 != null)
                {
                    return text1;
                }
                return "2003";
            }
            set { Application["StartYear"] = HttpUtility.HtmlEncode(value); }
        }

        public string CompanyName
        {
            get
            {
                string text1 = BBICMS.Helpers.Settings.SiteName;
                if (text1 != null)
                {
                    return text1;
                }
                return string.Empty;
            }
            set { Application["CompanyName"] = HttpUtility.HtmlEncode(value); }
        }

        public string SiteUrl
        {
            get
            {
                string text1 = BBICMS.Helpers.Settings.SiteDomainName;
                if (text1 != null)
                {
                    return text1;
                }
                return string.Empty;
            }
            set { Application["SiteUrl"] = HttpUtility.HtmlEncode(value); }
        }

        #endregion

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            string text2 = FormatString;

            if (string.IsNullOrEmpty(StartYear) == false)
            {
                if (string.IsNullOrEmpty(text2))
                {
                    if (StartYear != DateTime.Now.Year.ToString())
                    {
                        ltlCopyright.Text = string.Format("Copyright &copy <a href=\"{0}\">{1}</a> {2}-{3}",SiteUrl, CompanyName, StartYear,
                                                          DateTime.Now.Year);
                    }
                    else
                    {
                        ltlCopyright.Text = string.Format("Copyright &copy <a href=\"{0}\">{1}</a> {2}", SiteUrl, CompanyName, DateTime.Now.Year);
                    }
                }
                else
                {
                    if (StartYear != DateTime.Now.Year.ToString())
                    {
                        ltlCopyright.Text = string.Format(text2, SiteUrl, CompanyName, StartYear, DateTime.Now.Year);
                    }
                    else
                    {
                        ltlCopyright.Text = string.Format(text2, SiteUrl, CompanyName, DateTime.Now.Year);
                    }
                }
            }
            else
            {
                ltlCopyright.Text = string.Format("Copyright &copy <a href=\"{0}\">{1}</a> {2}", SiteUrl, CompanyName, DateTime.Now.Year);
            }
        }
    }
}