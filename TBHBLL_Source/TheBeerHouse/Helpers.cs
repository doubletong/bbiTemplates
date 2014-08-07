namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Principal;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Configuration;
    using System.Web.Profile;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using TheBeerHouse.BLL.Newsletters;

    public sealed class Helpers
    {
        /// <summary>
        /// I left this in for those that might use this in their appliciations. 
        /// </summary>
        /// <remarks></remarks>
        private static string[] _countries = new string[] { 
            "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua And Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", 
            "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia Hercegovina", "Botswana", "Bouvet Island", "Brazil", "Brunei Darussalam", "Bulgaria", 
            "Burkina Faso", "Burundi", "Byelorussian SSR", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", 
            "Congo", "Cook Islands", "Costa Rica", "Cote D'Ivoire", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Czechoslovakia", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt", 
            "El Salvador", "England", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", 
            "Gibraltar", "Great Britain", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemela", "Guernsey", "Guiana", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard Islands", "Honduras", 
            "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle Of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", 
            "Kenya", "Kiribati", "Korea, South", "Korea, North", "Kuwait", "Kyrgyzstan", "Lao People's Dem. Rep.", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", 
            "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Mariana Islands", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia", "Moldova", 
            "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "Neutral Zone", "New Caledonia", "New Zealand", "Nicaragua", "Niger", 
            "Nigeria", "Niue", "Norfolk Island", "Northern Ireland", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Polynesia", 
            "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "Saint Helena", "Saint Kitts", "Saint Lucia", "Saint Pierre", "Saint Vincent", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", 
            "Scotland", "Senegal", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia", "Spain", "Sri Lanka", "Sudan", "Suriname", "Svalbard", 
            "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikista", "Tanzania", "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", 
            "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City State", "Venezuela", "Vietnam", "Virgin Islands", "Wales", "Western Sahara", "Yemen", 
            "Yugoslavia", "Zaire", "Zambia", "Zimbabwe"
         };
        public static readonly TheBeerHouseSection Settings = ((TheBeerHouseSection) WebConfigurationManager.GetSection("theBeerHouse"));
        public static string ThemesSelectorID = string.Empty;

        public static string ConvertToHtml(string content)
        {
            content = HttpUtility.HtmlEncode(content);
            content = content.Replace("  ", "&nbsp;&nbsp;").Replace(@"\t", "&nbsp;&nbsp;&nbsp;").Replace(@"\n", "<br>");
            return content;
        }

        public static GroupCollection ExtractPhoneNumberPieces(string sPhoneNumber)
        {
            GroupCollection ExtractPhoneNumberPieces;
            return ExtractPhoneNumberPieces;
        }

        /// <summary>
        /// </summary>
        /// <param name="vText"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FilterProfanity(string vText)
        {
            Regex ProfanityReg = new Regex(@"\b(bad phrase)\b", RegexOptions.Multiline | RegexOptions.IgnoreCase);
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
            Regex rg = new Regex(@"^\(?(\d{3})\)?[/ - .]?(\d{3})[- .]?(\d{4})$");
            switch (((int) pnf))
            {
                case 1:
                    return rg.Replace(sPhoneNumber, "($1) $2-$3");

                case 2:
                    return rg.Replace(sPhoneNumber, "$1-$2-$3");

                case 3:
                    return rg.Replace(sPhoneNumber, "$1.$2.$3");

                case 4:
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
            return (Convert.ToDecimal(RuntimeHelpers.GetObjectValue(vPrice)).ToString("N2") + " " + TheBeerHouse.Globals.Settings.Store.CurrencyCode);
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
            StringCollection countries = new StringCollection();
            countries.AddRange(_countries);
            return countries;
        }

        public static SortedList GetCountries(bool insertEmpty)
        {
            SortedList countries = new SortedList();
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

        public static string GetProfileFirstName(ProfileBase profile)
        {
            return profile.GetPropertyValue("FirstName").ToString();
        }

        public static string GetProfileLastName(ProfileBase profile)
        {
            return profile.GetPropertyValue("LastName").ToString();
        }

        public static string GetProfileRealName(ProfileBase profile)
        {
            return string.Format("{0} {1}", profile.GetPropertyValue("FirstName").ToString(), profile.GetPropertyValue("LastName").ToString());
        }

        private static object GetProfileSubGroup(ProfileBase profile, string vSubGroup)
        {
            return profile.GetProfileGroup(vSubGroup);
        }

        public static int GetProfileSubGroupIntegerProperty(ProfileBase profile, string vSubGroup, string vProperty)
        {
            if (!Information.IsNothing(profile))
            {
                object[] VB$t_array$S1 = new object[] { vProperty };
                bool[] VB$t_array$S2 = new bool[] { true };
                if (VB$t_array$S2[0])
                {
                    vProperty = (string) Conversions.ChangeType(RuntimeHelpers.GetObjectValue(VB$t_array$S1[0]), typeof(string));
                }
                return Conversions.ToInteger(NewLateBinding.LateGet(GetProfileSubGroup(profile, vSubGroup), null, "GetPropertyValue", VB$t_array$S1, null, null, VB$t_array$S2));
            }
            return 0;
        }

        public static object GetProfileSubGroupObjectProperty(ProfileBase profile, string vSubGroup, string vProperty)
        {
            if (!Information.IsNothing(profile))
            {
                object[] VB$t_array$S1 = new object[] { vProperty };
                bool[] VB$t_array$S2 = new bool[] { true };
                if (VB$t_array$S2[0])
                {
                    vProperty = (string) Conversions.ChangeType(RuntimeHelpers.GetObjectValue(VB$t_array$S1[0]), typeof(string));
                }
                return NewLateBinding.LateGet(GetProfileSubGroup(profile, vSubGroup), null, "GetPropertyValue", VB$t_array$S1, null, null, VB$t_array$S2);
            }
            return null;
        }

        public static string GetProfileSubGroupStringProperty(ProfileBase profile, string vSubGroup, string vProperty)
        {
            if (((!Information.IsNothing(profile) && !Information.IsNothing(profile.GetProfileGroup(vSubGroup))) ? 1 : 0) != 0)
            {
                return Conversions.ToString(profile.GetProfileGroup(vSubGroup).GetPropertyValue(vProperty));
            }
            return string.Empty;
        }

        public static string GetProfileTheme(ProfileBase profile)
        {
            return GetProfileSubGroupStringProperty(profile, "Preferences", "Theme").ToString();
        }

        public static SubscriptionType GetSubscriptionType(ProfileBase profile)
        {
            return (SubscriptionType) Conversions.ToInteger(GetProfileSubGroupStringProperty(profile, "Preferences", "Newsletter"));
        }

        /// <summary>
        /// Returns an array with the names of all local Themes
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string[] GetThemes()
        {
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(HttpContext.Current.Cache["SiteThemes"])))
            {
                return (string[]) HttpContext.Current.Cache["SiteThemes"];
            }
            string themesDirPath = HttpContext.Current.Server.MapPath("~/App_Themes");
            string[] themes = Directory.GetDirectories(themesDirPath);
            int VB$t_i4$L0 = themes.Length - 1;
            for (int i = 0; i <= VB$t_i4$L0; i++)
            {
                themes[i] = Path.GetFileName(themes[i]);
            }
            CacheDependency dep = new CacheDependency(themesDirPath);
            HttpContext.Current.Cache.Insert("SiteThemes", themes, dep);
            return themes;
        }

        public static string GetURLPath(string vURL)
        {
            Regex _Regex = new Regex("://[^/]+/(?<path>[^?\\s<>#\"]+)");
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

        public static bool IsValidString(string vValue, int vMax)
        {
            return IsValidString(vValue, Conversions.ToString(0), vMax);
        }

        public static bool IsValidString(string vValue, string vMin, int vMax)
        {
            if ((Conversions.ToDouble(vMin) > 0.0) && (((string.IsNullOrEmpty(vValue) && (Conversions.ToDouble(vMin) <= vValue.Length)) ? 1 : 0) != 0))
            {
                return false;
            }
            if (vMax < vValue.Length)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidUSPhoneNumber(string sPhoneNumber)
        {
            IEnumerator VB$t_ref$L0;
            RegexOptions options = RegexOptions.Multiline;
            MatchCollection matches = Regex.Matches(sPhoneNumber, @"^\d{3}/\d{3}-\d{4}$^\(\d{3}\)\d{3}-\d{4}$|^\d{3}-\d{3}-\d{4}$|\d{10}", options);
            try
            {
                VB$t_ref$L0 = matches.GetEnumerator();
                while (VB$t_ref$L0.MoveNext())
                {
                    Match found = (Match) VB$t_ref$L0.Current;
                    return true;
                }
            }
            finally
            {
                if (VB$t_ref$L0 is IDisposable)
                {
                    (VB$t_ref$L0 as IDisposable).Dispose();
                }
            }
            return false;
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
            CultureInfo objCulture = new CultureInfo("en-US");
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

        public static string ReplaceFirstWord(string vText, string vPhrasedToBeReplaced, string vPhraseToReplaceWith)
        {
            Regex ReplaceReg = new Regex(vPhrasedToBeReplaced, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (ReplaceReg.IsMatch(vText))
            {
                IEnumerator VB$t_ref$L0;
                try
                {
                    VB$t_ref$L0 = ReplaceReg.Matches(vText).GetEnumerator();
                    while (VB$t_ref$L0.MoveNext())
                    {
                        Match lmatch = (Match) VB$t_ref$L0.Current;
                    }
                }
                finally
                {
                    if (VB$t_ref$L0 is IDisposable)
                    {
                        (VB$t_ref$L0 as IDisposable).Dispose();
                    }
                }
            }
            return vText;
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
            object VB$t_ref$L0 = HttpContext.Current.Application[ItemName];
            ObjectFlowControl.CheckForSyncLockOnValueType(VB$t_ref$L0);
            lock (VB$t_ref$L0)
            {
                HttpContext.Current.Application[ItemName] = Value;
            }
        }

        /// <summary>
        /// Adds the onfocus and onblur attributes to all input controls found in the specified parent,
        /// to change their apperance with the control has the focus.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="className"></param>
        /// <param name="onlyTextBoxes"></param>
        /// <remarks></remarks>
        public static void SetInputControlsHighlight(Control container, string className, bool onlyTextBoxes)
        {
            IEnumerator VB$t_ref$L0;
            try
            {
                VB$t_ref$L0 = container.Controls.GetEnumerator();
                while (VB$t_ref$L0.MoveNext())
                {
                    Control ctl = (Control) VB$t_ref$L0.Current;
                    if ((((!onlyTextBoxes || !(ctl is TextBox)) && !(ctl is TextBox)) && (!(ctl is DropDownList) && !(ctl is ListBox))) && ((!(ctl is CheckBox) && !(ctl is RadioButton)) && !(ctl is RadioButtonList)))
                    {
                    }
                    if (((ctl is CheckBoxList) ? 1 : 0) != 0)
                    {
                        WebControl wctl = (WebControl) ctl;
                        wctl.Attributes.Add("onfocus", string.Format("this.className = '{0}';", className));
                        wctl.Attributes.Add("onblur", "this.className = '';");
                    }
                    else if (ctl.Controls.Count > 0)
                    {
                        SetInputControlsHighlight(ctl, className, onlyTextBoxes);
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
        }

        public static void SetProfileTheme(ProfileBase profile, string sTheme)
        {
            object[] VB$t_array$S1 = new object[] { "Theme", sTheme };
            bool[] VB$t_array$S2 = new bool[] { false, true };
            NewLateBinding.LateCall(GetProfileSubGroup(profile, "Preferences"), null, "setpropertyvalue", VB$t_array$S1, null, null, VB$t_array$S2, true);
            if (VB$t_array$S2[1])
            {
                sTheme = (string) Conversions.ChangeType(RuntimeHelpers.GetObjectValue(VB$t_array$S1[1]), typeof(string));
            }
        }

        /// <summary>
        /// Stores a value in the Application for quick retrieval
        /// </summary>
        /// <param name="ItemName">The name used to identify the object.</param>
        /// <value></value>
        /// <returns>The value of the item or an empty string.</returns>
        /// <remarks></remarks>
        public string this[string ItemName]
        {
            get
            {
                if (Information.IsNothing(RuntimeHelpers.GetObjectValue(HttpContext.Current.Application[ItemName])))
                {
                    SetAppItem(ItemName, string.Empty);
                }
                return HttpContext.Current.Application[ItemName].ToString();
            }
            set
            {
                SetAppItem(ItemName, value);
            }
        }

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
            get
            {
                return HttpContext.Current.Request.PhysicalApplicationPath;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string CurrentUserIP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
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

        /// <summary>
        /// </summary>
        /// <param name="vAppendPath"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string this[string vAppendPath]
        {
            get
            {
                return Path.Combine(WebRoot, vAppendPath).Replace(@"\", "/");
            }
        }

        public static string WebRoot
        {
            get
            {
                TheBeerHouseSection BHSettings = (TheBeerHouseSection) WebConfigurationManager.GetSection("theBeerHouse");
                return BHSettings.SiteDomainName;
            }
        }

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
    }
}

