using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using BBICMS.Newsletters;

namespace BBICMS
{
    public sealed class Helpers
    {
        #region PhoneNumberFormat enum

        /// <summary>
        /// Phone Number Format
        /// </summary>
        /// <remarks></remarks>
        public enum PhoneNumberFormat
        {
            Dashes = 2,
            Parenthises = 1,
            Periods = 3,
            Straight = 4
        }

        #endregion

        /// <summary>
        /// I left this in for those that might use this in their appliciations. 
        /// </summary>
        /// <remarks></remarks>
        private static readonly string[] _countries = new[]
                                                          {
                                                              "Afghanistan", "Albania", "Algeria", "American Samoa",
                                                              "Andorra", "Angola", "Anguilla", "Antarctica",
                                                              "Antigua And Barbuda", "Argentina", "Armenia", "Aruba",
                                                              "Australia", "Austria", "Azerbaijan", "Bahamas",
                                                              "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium",
                                                              "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia",
                                                              "Bosnia Hercegovina", "Botswana", "Bouvet Island",
                                                              "Brazil", "Brunei Darussalam", "Bulgaria",
                                                              "Burkina Faso", "Burundi", "Byelorussian SSR", "Cambodia",
                                                              "Cameroon", "Canada", "Cape Verde", "Cayman Islands",
                                                              "Central African Republic", "Chad", "Chile", "China",
                                                              "Christmas Island", "Cocos (Keeling) Islands", "Colombia",
                                                              "Comoros",
                                                              "Congo", "Cook Islands", "Costa Rica", "Cote D'Ivoire",
                                                              "Croatia", "Cuba", "Cyprus", "Czech Republic",
                                                              "Czechoslovakia", "Denmark", "Djibouti", "Dominica",
                                                              "Dominican Republic", "East Timor", "Ecuador", "Egypt",
                                                              "El Salvador", "England", "Equatorial Guinea", "Eritrea",
                                                              "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands"
                                                              , "Fiji", "Finland", "France", "Gabon", "Gambia",
                                                              "Georgia", "Germany", "Ghana",
                                                              "Gibraltar", "Great Britain", "Greece", "Greenland",
                                                              "Grenada", "Guadeloupe", "Guam", "Guatemela", "Guernsey",
                                                              "Guiana", "Guinea", "Guinea-Bissau", "Guyana", "Haiti",
                                                              "Heard Islands", "Honduras",
                                                              "Hong Kong", "Hungary", "Iceland", "India", "Indonesia",
                                                              "Iran", "Iraq", "Ireland", "Isle Of Man", "Israel",
                                                              "Italy", "Jamaica", "Japan", "Jersey", "Jordan",
                                                              "Kazakhstan",
                                                              "Kenya", "Kiribati", "Korea, South", "Korea, North",
                                                              "Kuwait", "Kyrgyzstan", "Lao People's Dem. Rep.", "Latvia"
                                                              , "Lebanon", "Lesotho", "Liberia", "Libya",
                                                              "Liechtenstein", "Lithuania", "Luxembourg", "Macau",
                                                              "Macedonia", "Madagascar", "Malawi", "Malaysia",
                                                              "Maldives", "Mali", "Malta", "Mariana Islands",
                                                              "Marshall Islands", "Martinique", "Mauritania",
                                                              "Mauritius", "Mayotte", "Mexico", "Micronesia", "Moldova",
                                                              "Monaco", "Mongolia", "Montserrat", "Morocco",
                                                              "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal",
                                                              "Netherlands", "Netherlands Antilles", "Neutral Zone",
                                                              "New Caledonia", "New Zealand", "Nicaragua", "Niger",
                                                              "Nigeria", "Niue", "Norfolk Island", "Northern Ireland",
                                                              "Norway", "Oman", "Pakistan", "Palau", "Panama",
                                                              "Papua New Guinea", "Paraguay", "Peru", "Philippines",
                                                              "Pitcairn", "Poland", "Polynesia",
                                                              "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania",
                                                              "Russian Federation", "Rwanda", "Saint Helena",
                                                              "Saint Kitts", "Saint Lucia", "Saint Pierre",
                                                              "Saint Vincent", "Samoa", "San Marino",
                                                              "Sao Tome and Principe", "Saudi Arabia",
                                                              "Scotland", "Senegal", "Seychelles", "Sierra Leone",
                                                              "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
                                                              "Somalia", "South Africa", "South Georgia", "Spain",
                                                              "Sri Lanka", "Sudan", "Suriname", "Svalbard",
                                                              "Swaziland", "Sweden", "Switzerland",
                                                              "Syrian Arab Republic", "Taiwan", "Tajikista", "Tanzania",
                                                              "Thailand", "Togo", "Tokelau", "Tonga",
                                                              "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan"
                                                              , "Turks and Caicos Islands",
                                                              "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates",
                                                              "United Kingdom", "United States", "Uruguay", "Uzbekistan"
                                                              , "Vanuatu", "Vatican City State", "Venezuela", "Vietnam",
                                                              "Virgin Islands", "Wales", "Western Sahara", "Yemen",
                                                              "Yugoslavia", "Zaire", "Zambia", "Zimbabwe"
                                                          };

        public static readonly BBICMSSection Settings =
            ((BBICMSSection) WebConfigurationManager.GetSection("theBeerHouse"));

        public static string ThemesSelectorID = string.Empty;

        /// <summary>
        /// This is a core method to help get the physical path for the site and returns a 
        /// application variable from the web.config file.  It helps helps with the variations
        /// between local host and a delpoyed site.
        /// </summary>
        /// <value></value>
        /// <returns>Physical Path for Web Site</returns>
        /// <remarks></remarks>
        public static string ApplicationPath
        {
            get { return HttpContext.Current.Request.PhysicalApplicationPath; }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IPrincipal CurrentUser
        {
            get { return HttpContext.Current.User; }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string CurrentUserIP
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string CurrentUserName
        {
            get
            {
                string userName = string.Empty;
                if (CurrentUser.Identity.IsAuthenticated)
                {
                    userName = CurrentUser.Identity.Name;
                }
                return userName;
            }
        }

        public static string WebRoot
        {
            get
            {
                var BHSettings = (BBICMSSection) WebConfigurationManager.GetSection("theBeerHouse");
                return BHSettings.SiteDomainName;
            }
        }

        public static string ConvertToHtml(string content)
        {
            content = HttpUtility.HtmlEncode(content);
            content = content.Replace("  ", "&nbsp;&nbsp;").Replace(@"\t", "&nbsp;&nbsp;&nbsp;").Replace(@"\n", "<br>");
            return content;
        }

        /// <summary>
        /// </summary>
        /// <param name="vText"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FilterProfanity(string vText)
        {
            var ProfanityReg = new Regex(@"\b(bad phrase)\b", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return ProfanityReg.Replace(vText, "$%^&**");
        }

        /// <summary>
        /// returns a Formatted Phone Number with Parenthises
        /// </summary>
        /// <param name="sPhoneNumber"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatPhoneNumber(string sPhoneNumber)
        {
            return FormatPhoneNumber(sPhoneNumber, PhoneNumberFormat.Parenthises);
        }

        /// <summary>
        /// Returns a Formatted Phone Number based on the PhoneNumberFormat
        /// </summary>
        /// <param name="sPhoneNumber"></param>
        /// <param name="pnf"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatPhoneNumber(string sPhoneNumber, PhoneNumberFormat pnf)
        {
            var rg = new Regex("^\\(?(\\d{3})\\)?[/ - .]?(\\d{3})[- .]?(\\d{4})$");

            switch (pnf)
            {
                case PhoneNumberFormat.Dashes:
                    return rg.Replace(sPhoneNumber, "$1-$2-$3");
                case PhoneNumberFormat.Parenthises:
                    return rg.Replace(sPhoneNumber, "($1) $2-$3");
                case PhoneNumberFormat.Periods:
                    return rg.Replace(sPhoneNumber, "$1.$2.$3");
                case PhoneNumberFormat.Straight:
                    return rg.Replace(sPhoneNumber, "$1$2$3");
            }


            return sPhoneNumber;
        }


        /// <summary>
        /// </summary>
        /// <param name="vPrice"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatPrice(object vPrice)
        {
            return Convert.ToDecimal(vPrice).ToString("N2") + " " + Globals.Settings.Store.CurrencyCode;
        }

        /// <summary>
        /// '-' tend to be more Search Engine Friendly than '_', and spaces are least efficient as a word deliminator in a URL.
        /// </summary>
        /// <param name="vURL"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatSpacesForURL(string vURL)
        {
            return vURL.Replace(" ", "-");
        }

        public static StringCollection GetCountries()
        {
            var countries = new StringCollection();
            countries.AddRange(_countries);
            return countries;
        }

        public static SortedList GetCountries(bool insertEmpty)
        {
            var countries = new SortedList();
            if (insertEmpty)
            {
                countries.Add("", "Please select one...");
            }
            foreach (string country in _countries)
            {
                countries.Add(country, country);
            }
            return countries;
        }

        /// <summary>
        /// </summary>
        /// <param name="vFolder"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetPhyscialPath(string vFolder)
        {
            return Path.Combine(ApplicationPath, vFolder);
        }

  

        public static string GetProfileRealName(ProfileBase profile)
        {
            return profile.GetPropertyValue("FullName").ToString();
        }

        private static object GetProfileSubGroup(ProfileBase profile, string vSubGroup)
        {
            return profile.GetProfileGroup(vSubGroup);
        }

        public static int GetProfileSubGroupIntegerProperty(ProfileBase profile, string vSubGroup, string vProperty)
        {
            if ((profile != null))
            {
                ProfileBase p = (ProfileBase) GetProfileSubGroup(profile, vSubGroup);
                return (int)p.GetPropertyValue(vProperty);
            }
            return 0;
        }

        public static object GetProfileSubGroupObjectProperty(ProfileBase profile, string vSubGroup, string vProperty)
        {
            if ((profile != null))
            {
                var subGroup = (ProfileBase) GetProfileSubGroup(profile, vSubGroup);
                return subGroup.GetPropertyValue(vProperty);
            }
            else
            {
                return null;
            }
        }

        public static string GetProfileSubGroupStringProperty(ProfileBase profile, string vSubGroup, string vProperty)
        {
            if ((profile != null) && (profile.GetProfileGroup(vSubGroup) != null))
            {
                return profile.GetProfileGroup(vSubGroup).GetPropertyValue(vProperty).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetProfileTheme(ProfileBase profile)
        {
            return GetProfileSubGroupStringProperty(profile, "Preferences", "Theme");
        }

        public static SubscriptionType GetSubscriptionType(ProfileBase profile)
        {
            return (SubscriptionType) GetProfileSubGroupObjectProperty(profile, "Preferences", "Newsletter");
        }

        /// <summary>
        /// Returns an array with the names of all local Themes
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string[] GetThemes()
        {
            if ((HttpContext.Current.Cache["SiteThemes"] != null))
            {
                return (string[]) HttpContext.Current.Cache["SiteThemes"];
            }

            string themesDirPath = HttpContext.Current.Server.MapPath("~/App_Themes");
            // get the array of themes folders under /App_Themes
            string[] themes = Directory.GetDirectories(themesDirPath);
            for (int i = 0; i <= themes.Length - 1; i++)
            {
                themes[i] = Path.GetFileName(themes[i]);
            }
            var dep = new CacheDependency(themesDirPath);
            HttpContext.Current.Cache.Insert("SiteThemes", themes, dep);
            return themes;
        }

        public static string GetURLPath(string vURL)
        {
            var _Regex = new Regex("://[^/]+/(?<path>[^?\\s<>#\"]+)");
            if (_Regex.Matches(vURL).Count > 0)
            {
                return _Regex.Match(vURL).Groups[1].ToString();
            }
            return vURL;
        }

        public static ProfileBase GetUserProfile()
        {
            return ProfileBase.Create(CurrentUserName, CurrentUser.Identity.IsAuthenticated);
        }

        public static ProfileBase GetUserProfile(string vUserName)
        {
            return ProfileBase.Create(vUserName, CurrentUser.Identity.IsAuthenticated);
        }

        public static ProfileBase GetUserProfile(string vUserName, bool isAuthenticated)
        {
            return ProfileBase.Create(vUserName, isAuthenticated);
        }

        public static bool IsValidPhoneNumber(string sPhoneNumber)
        {
            return IsValidUSPhoneNumber(sPhoneNumber);
        }

        public static bool IsValidUSPhoneNumber(string sPhoneNumber)
        {
            RegexOptions options = RegexOptions.Multiline;

            MatchCollection matches = Regex.Matches(sPhoneNumber,
                                                    "^\\d{3}/\\d{3}-\\d{4}$^\\(\\d{3}\\)\\d{3}-\\d{4}$|^\\d{3}-\\d{3}-\\d{4}$|\\d{10}",
                                                    options);

            foreach (object found in matches)
            {
                return true;
            }


            return false;
        }

        public static bool IsValidString(string vValue, int vMax)
        {
            return IsValidString(vValue, 0, vMax);
        }

        public static bool IsValidString(string vValue, int vMin, int vMax)
        {
            if (vMin > 0)
            {
                if (string.IsNullOrEmpty(vValue) && vMin <= vValue.Length)
                {
                    return false;
                }
            }
            if (vMax < vValue.Length)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// </summary>
        /// <param name="vDirectoryPath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DirectoryInfo MakeDirectory(string vDirectoryPath)
        {
            if (Directory.Exists(vDirectoryPath))
            {
                return new DirectoryInfo(vDirectoryPath);
            }
            return Directory.CreateDirectory(vDirectoryPath);
        }

        public static string ParsePhoneNumber(string sPhoneNumber)
        {
            if (IsValidUSPhoneNumber(sPhoneNumber))
            {
                return sPhoneNumber.Replace("(", "").Replace(")", "").Replace("-", "");
            }
            return string.Empty;
        }

        public static string ProperCase(string Text)
        {
            var objCulture = new CultureInfo("zh-CN");
            return objCulture.TextInfo.ToTitleCase(Text.ToLower());
        }

        /// <summary>
        /// This is a utlity function I used to wrap the Regex.Replace
        /// method. It just provides a shorthand way to do a replace.
        /// </summary>
        /// <param name="strin"></param>
        /// <param name="strExp"></param>
        /// <param name="strReplace"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string RegexReplace(string strin, string strExp, string strReplace)
        {
            return Regex.Replace(strin, strExp, strReplace);
        }

        public static string RemoveHTML(string strText)
        {
            return RegexReplace(strText, "<[^>]*>", string.Empty);
        }

        /// <summary>
        /// Cleans the whitespace, or unneeded characters for the
        /// page to redner in the browser.
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        /// <remarks>
        /// This make the page a little thinner by not including
        /// extra characters that do not make the page render,
        /// but rather help developers manage the markup in editors.</remarks>
        public static string RemoveWhiteSpace(string strText)
        {
            return RegexReplace(strText, @"(?<=>)\s+(?=</?)", string.Empty);
        }

        public static string ResolveUrl(string originalUrl)
        {
            if (originalUrl == null)
            {
                return null;
            }
            if (originalUrl.IndexOf("://") != -1)
            {
                return originalUrl;
            }
            if (!originalUrl.StartsWith("~"))
            {
                return originalUrl;
            }
            if (HttpContext.Current == null)
            {
                throw new ArgumentException("Invalid URL: Relative URL not allowed.");
            }
            return (HttpContext.Current.Request.ApplicationPath + originalUrl.Substring(1).Replace("//", "/"));
        }

        /// <summary>
        /// </summary>
        /// <param name="vURL"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string SEOFriendlyURL(string vURL, string vExtension)
        {
            if (!string.IsNullOrEmpty(vURL))
            {
                return ResolveUrl(vURL.Replace("#", "").Replace(" ", "-") + vExtension);
            }
            return string.Empty;
        }

        /// <summary>
        /// A private method to set a value in the application, it 
        /// provides a lock around the object to prohibit simultaneous values
        /// from being written. It assumes last in wins.
        /// </summary>
        /// <param name="ItemName"></param>
        /// <param name="Value"></param>
        /// <remarks></remarks>
        private static void SetAppItem(string ItemName, string Value)
        {
            lock (HttpContext.Current.Application[ItemName])
            {
                HttpContext.Current.Application[ItemName] = Value;
            }
        }

       

        public static void SetProfileTheme(ProfileBase profile, string sTheme)
        {
            var subGroup = (ProfileBase) GetProfileSubGroup(profile, "Preferences");
            subGroup.SetPropertyValue("Theme", sTheme);
        }

        /// <summary>
        /// Stores a value in the Application for quick retrieval
        /// This differs from the VB property because C# properties cannot accept parameters.
        /// </summary>
        /// <param name="ItemName">The name used to identify the object.</param>
        /// <value></value>
        /// <returns>The value of the item or an empty string.</returns>
        /// <remarks></remarks>
        public string AppItem(string ItemName)
        {
            if ((HttpContext.Current.Application[ItemName] == null))
            {
                SetAppItem(ItemName, string.Empty);
            }
            return HttpContext.Current.Application[ItemName].ToString();
        }

        public void AppItem(string ItemName, string value)
        {
            SetAppItem(ItemName, value);
        }

        /// <summary>
        /// </summary>
        /// <param name="vAppendPath"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatUrl(string vAppendPath)
        {
            return Path.Combine(WebRoot, vAppendPath).Replace("\\", "/");
        }


        public static void ShowMsg(string title, string msg)
        {
            Page page = (Page)System.Web.HttpContext.Current.Handler;
            ClientScriptManager cs = page.ClientScript;
            string mstitle = title;
            // Check to see if the client script is already registered.
            if (!cs.IsClientScriptBlockRegistered(mstitle))
            {
                StringBuilder message = new StringBuilder();
                message.Append("<script type=\"text/javascript\">");
                message.Append("alert(\"" + msg + "\") </");
                message.Append("script>");
                cs.RegisterClientScriptBlock(page.GetType(), mstitle, message.ToString(), false);
            }
        }
        /// <summary>
        /// 显示信息便跳转
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="returnUrl"></param>

        public static void ShowMsgAndRedirect(string title, string msg, string returnUrl)
        {

            Page page = (Page)System.Web.HttpContext.Current.Handler;
            ClientScriptManager cs = page.ClientScript;
            string mstitle = title;
            // Check to see if the client script is already registered.
            if (!cs.IsClientScriptBlockRegistered(mstitle))
            {
                StringBuilder message = new StringBuilder();
                message.Append("<script type=\"text/javascript\">");
                message.Append("alert(\"" + msg + "\"); window.location.href =\"" + returnUrl + "\"; </");
                message.Append("script>");
                cs.RegisterClientScriptBlock(page.GetType(), mstitle, message.ToString(), false);
            }
        }
    }
}