namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;
    using TheBeerHouse.BLL;

    public class URLRewrite : IHttpModule
    {
        private static Regex wwwRegex = new Regex(@"https?://www\.", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private void BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication) sender;
            HttpRequest Request = app.Request;
            HttpResponse Response = app.Response;
            string sRequestedURL = Request.Url.ToString().ToLower();
            bool bWWW = wwwRegex.IsMatch(sRequestedURL);
            string redirectURL = string.Empty;
            if (bWWW)
            {
                redirectURL = wwwRegex.Replace(sRequestedURL, string.Format("{0}://", Request.Url.Scheme));
            }
            this.Rewrite(app);
        }

        public void Dispose()
        {
        }

        private void Do301Redirect(HttpResponse Response, string redirectURL)
        {
            Response.RedirectLocation = redirectURL;
            Response.StatusCode = 0x12d;
            Response.End();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.BeginRequest);
        }

        private void Rewrite(HttpApplication app)
        {
            if (app.Context.Request.Path.ToLower().EndsWith(".aspx"))
            {
                using (SiteMapRepository lSiteMapRst = new SiteMapRepository(TheBeerHouse.Globals.Settings.DefaultConnectionStringName))
                {
                    string lURLFile = Helpers.GetURLPath(app.Context.Request.Url.ToString());
                    SiteMapInfo lSiteMap = lSiteMapRst.GetSiteMapInfoByURL(lURLFile.Replace("BeerHouse35/", ""));
                    if (!Information.IsNothing(lSiteMap))
                    {
                        if (lSiteMap.RealURL != lURLFile)
                        {
                            HttpContext.Current.RewritePath("~/" + lSiteMap.RealURL, false);
                        }
                        else
                        {
                            this.Do301Redirect(app.Response, Path.Combine(TheBeerHouse.Globals.Settings.SiteDomainName, lSiteMap.URL));
                        }
                    }
                }
            }
        }
    }
}

