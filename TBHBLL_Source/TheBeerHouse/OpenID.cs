namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary> 
    /// Summary description for OpenID 
    /// </summary>
    public sealed class OpenID
    {
        private static readonly Regex REGEX_HREF = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex REGEX_LINK = new Regex("<link[^>]*/?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private OpenID()
        {
        }

        private static void AssignValue(Match linkMatch, StringDictionary col, string name)
        {
            if (linkMatch.Value.IndexOf(name) > 0)
            {
                Match hrefMatch = REGEX_HREF.Match(linkMatch.Value);
                if (hrefMatch.Success && !col.ContainsKey(name))
                {
                    col.Add(name, hrefMatch.Groups[1].Value);
                }
            }
        }

        /// <summary> 
        /// Authenticates the request from the OpenID provider. 
        /// </summary>
        public static OpenIdData Authenticate()
        {
            OpenIdData data = (OpenIdData) HttpContext.Current.Session["openid"];
            if (data == null)
            {
                return new OpenIdData(string.Empty);
            }
            NameValueCollection query = HttpContext.Current.Request.QueryString;
            if (query["openid.claimed_id"] == data.Identity)
            {
                IEnumerator VB$t_ref$L0;
                data.IsSuccess = query["openid.mode"] == "id_res";
                try
                {
                    VB$t_ref$L0 = query.Keys.GetEnumerator();
                    while (VB$t_ref$L0.MoveNext())
                    {
                        string name = Conversions.ToString(VB$t_ref$L0.Current);
                        if (name.StartsWith("openid.sreg."))
                        {
                            data.Parameters.Add(name.Replace("openid.sreg.", string.Empty), query[name]);
                        }
                    }
                }
                finally
                {
                    if (VB$t_ref$L0 is IDisposable)
                    {
                        (VB$t_ref$L0 as IDisposable).Dispose();
                    }
                }
                HttpContext.Current.Session.Remove("openid");
            }
            return data;
        }

        /// <summary> 
        /// Creates the URL to the OpenID provider with all parameters. 
        /// </summary>
        private static string CreateRedirectUrl(string requiredParameters, string optionalParameters, string delgate, string identity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("?openid.ns=" + HttpUtility.UrlEncode("http://specs.openid.net/auth/2.0"));
            sb.Append("&openid.mode=checkid_setup");
            sb.Append("&openid.identity=" + HttpUtility.UrlEncode(delgate));
            sb.Append("&openid.claimed_id=" + HttpUtility.UrlEncode(identity));
            sb.Append("&openid.return_to=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
            if (((!string.IsNullOrEmpty(requiredParameters) || !string.IsNullOrEmpty(optionalParameters)) ? 1 : 0) != 0)
            {
                sb.Append("&openid.ns.sreg=" + HttpUtility.UrlEncode("http://openid.net/extensions/sreg/1.1"));
                if (!string.IsNullOrEmpty(requiredParameters))
                {
                    sb.Append("&openid.sreg.required=" + HttpUtility.UrlEncode(requiredParameters));
                }
                if (!string.IsNullOrEmpty(optionalParameters))
                {
                    sb.Append("&openid.sreg.optional=" + HttpUtility.UrlEncode(optionalParameters));
                }
            }
            return sb.ToString();
        }

        /// <summary> 
        /// Crawls the identity URL to find the auto-discovery link headers. 
        /// </summary>
        public static StringDictionary GetIdentityServer(string identity)
        {
            using (WebClient client = new WebClient())
            {
                IEnumerator VB$t_ref$L0;
                string html = client.DownloadString(identity);
                StringDictionary col = new StringDictionary();
                try
                {
                    VB$t_ref$L0 = REGEX_LINK.Matches(html).GetEnumerator();
                    while (VB$t_ref$L0.MoveNext())
                    {
                        Match match = (Match) VB$t_ref$L0.Current;
                        AssignValue(match, col, "openid.server");
                        AssignValue(match, col, "openid.delegate");
                    }
                }
                finally
                {
                    if (VB$t_ref$L0 is IDisposable)
                    {
                        (VB$t_ref$L0 as IDisposable).Dispose();
                    }
                }
                return col;
            }
        }

        /// <summary> 
        /// Perform redirection to the OpenID provider based on the specified identity. 
        /// </summary>
        /// <param name="identity">The identity or OpenID URL.</param>
        /// <param name="requiredParameters">The required parameters. Can be null or string.empty.</param>
        /// <param name="OptionalParameters">The optional parameters. Can be null or string.empty.</param>
        public static bool Login(string identity, string requiredParameters, string optionalParameters)
        {
            try
            {
                StringDictionary dic = GetIdentityServer(identity);
                string server = dic["openid.server"];
                string VB$LW$t_string$S0 = dic["openid.delegate"];
                string delgate = (VB$LW$t_string$S0 != null) ? VB$LW$t_string$S0 : identity;
                if (!string.IsNullOrEmpty(server))
                {
                    string redirectUrl = CreateRedirectUrl(requiredParameters, optionalParameters, delgate, identity);
                    OpenIdData data = new OpenIdData(identity);
                    HttpContext.Current.Session["openid"] = data;
                    HttpContext.Current.Response.Redirect(server + redirectUrl, true);
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception generatedExceptionName = exception1;
                ProjectData.ClearProjectError();
            }
            return false;
        }

        /// <summary> 
        /// Gets a value indicating whether the request comes from an OpenID provider. 
        /// </summary>
        /// <value>
        /// <c>true</c> if this is an OpenID request; otherwise, <c>false</c>. 
        /// </value>
        public static bool IsOpenIdRequest
        {
            get
            {
                if (!HttpContext.Current.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                return (HttpContext.Current.Request.QueryString["openid.mode"] != null);
            }
        }
    }
}

