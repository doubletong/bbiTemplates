using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using BLL;
using BBICMS.BLL;

namespace BBICMS
{
    public class URLRewrite : IHttpModule
    {
        private static Regex wwwRegex = new Regex(@"https?://www\.", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(BeginRequest);
        }

        #endregion

        private void BeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication) sender;
            HttpRequest Request = app.Request;
            HttpResponse Response = app.Response;
            string sRequestedURL = Request.Url.ToString().ToLower();
            bool bWWW = wwwRegex.IsMatch(sRequestedURL);
            string redirectURL = string.Empty;
            if (bWWW)
            {
                redirectURL = wwwRegex.Replace(sRequestedURL, string.Format("{0}://", Request.Url.Scheme));
            }
            Rewrite(app);
        }

        private void Do301Redirect(HttpResponse Response, string redirectURL)
        {
            Response.RedirectLocation = redirectURL;
            Response.StatusCode = 0x12d;
            Response.End();
        }

        private void Rewrite(HttpApplication app)
        {
            if (app.Context.Request.Path.ToLower().EndsWith(".aspx"))
            {
                using (var lSiteMapRst = new SiteMapRepository(Globals.Settings.DefaultConnectionStringName))
                {
                    string lURLFile = Helpers.GetURLPath(app.Context.Request.Url.ToString());
                    SiteMapInfo lSiteMap = lSiteMapRst.GetSiteMapInfoByURL(lURLFile.Replace("BeerHouse35/", ""));
                    if (null != lSiteMap)
                    {
                        if (lSiteMap.RealURL != lURLFile)
                        {
                            HttpContext.Current.RewritePath("~/" + lSiteMap.RealURL, false);
                        }
                        else
                        {
                            Do301Redirect(app.Response, Path.Combine(Globals.Settings.SiteDomainName, lSiteMap.URL));
                        }
                    }
                }
            }
        }
    }
}