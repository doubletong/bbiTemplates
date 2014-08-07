namespace TheBeerHouse.UI
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Profile;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using TheBeerHouse;

    public class BasePage : Page
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private bool _cleanWhiteSpace;
        private bool _moveHiddenFields;
        public ProfileBase PageProfile;
        public static readonly TheBeerHouseSection Settings = ((TheBeerHouseSection) WebConfigurationManager.GetSection("theBeerHouse"));

        public BasePage()
        {
            base.PreInit += new EventHandler(this.Page_PreInit);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
            this._cleanWhiteSpace = true;
            this._moveHiddenFields = true;
            this.PageProfile = Helpers.GetUserProfile();
        }

        /// <summary>
        /// </summary>
        /// <param name="cssFile"></param>
        /// <remarks></remarks>
        public void AddStyleSheet(string cssFile)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("href", "~/" + cssFile);
            base.Header.Controls.Add(link);
        }

        public string ConvertToHtml(string vString)
        {
            return Helpers.ConvertToHtml(vString);
        }

        /// <summary>
        /// </summary>
        /// <param name="sTagName"></param>
        /// <param name="TagValue"></param>
        /// <remarks></remarks>
        public void CreateMetaControl(string sTagName, string TagValue)
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Name = sTagName;
            meta.Content = TagValue;
            if (((!Information.IsNothing(this.Master) && !Information.IsNothing(this.Master.Page)) ? 1 : 0) != 0)
            {
                this.Master.Page.Header.Controls.Add(meta);
            }
            else
            {
                this.Page.Header.Controls.Add(meta);
            }
        }

        public string FormatPrice(object price)
        {
            return Helpers.FormatPrice(RuntimeHelpers.GetObjectValue(price));
        }

        protected void GetDateTextBoxValue(TextBox vTextBox, ref DateTime? vDate)
        {
            if (string.IsNullOrEmpty(vTextBox.Text))
            {
                vDate = DateTime.MinValue;
            }
            else
            {
                vDate = Conversions.ToDate(vTextBox.Text);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sTagName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetMetaValue(string sTagName)
        {
            HtmlMeta meta;
            if (!Information.IsNothing(this.Master))
            {
                meta = (HtmlMeta) this.Master.Page.Header.FindControl(sTagName);
            }
            else
            {
                meta = (HtmlMeta) this.Page.Header.FindControl(sTagName);
            }
            if (!Information.IsNothing(meta))
            {
                return meta.Content;
            }
            return string.Empty;
        }

        public ProfileBase GetUserProfile(string vUserName)
        {
            return Helpers.GetUserProfile(vUserName);
        }

        protected override void InitializeCulture()
        {
            string culture = Conversions.ToString(TheBeerHouse.Globals.TBHProfile.GetProfileGroup("Preferences").GetPropertyValue("Culture"));
            this.Culture = culture;
            this.UICulture = culture;
        }

        private string MoveHiddenFieldsToBottom(string html)
        {
            IEnumerator VB$t_ref$L0;
            string sPattern = "<input type=\"hidden\".*/*>|<input type=\"hidden\".*></input>";
            MatchCollection mc = Regex.Matches(html, sPattern, (RegexOptions) Conversions.ToInteger(Conversions.ToString(1) + Conversions.ToString(0x20)));
            StringBuilder sb = new StringBuilder();
            try
            {
                VB$t_ref$L0 = mc.GetEnumerator();
                while (VB$t_ref$L0.MoveNext())
                {
                    Match m = (Match) VB$t_ref$L0.Current;
                    sb.AppendLine(m.Value);
                    html = html.Replace(m.Value, string.Empty);
                }
            }
            finally
            {
                if (VB$t_ref$L0 is IDisposable)
                {
                    (VB$t_ref$L0 as IDisposable).Dispose();
                }
            }
            return html.Replace("</form>", sb.ToString() + "</form>");
        }

        protected override void OnLoad(EventArgs e)
        {
            Helpers.SetInputControlsHighlight(this, "highlight", false);
            base.OnLoad(e);
        }

        private void Page_PreInit(object sender, EventArgs e)
        {
            string id = Helpers.ThemesSelectorID;
            if (!string.IsNullOrEmpty(id))
            {
                if ((((this.Request.Form["__EVENTTARGET"] == id) && !string.IsNullOrEmpty(this.Request.Form[id])) ? 1 : 0) != 0)
                {
                    this.Theme = this.Request.Form[id];
                    Helpers.SetProfileTheme(this.PageProfile, this.Theme);
                }
                else if (!string.IsNullOrEmpty(Helpers.GetProfileTheme(this.PageProfile)))
                {
                    this.Theme = Helpers.GetProfileTheme(this.PageProfile);
                }
            }
        }

        protected string RemoveWhiteSpace(string strText)
        {
            return Helpers.RemoveWhiteSpace(strText);
        }

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        /// <remarks></remarks>
        protected override void Render(HtmlTextWriter writer)
        {
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
            base.Render(htmlWriter);
            string html = stringWriter.ToString();
            if (this.MoveHiddenFields)
            {
                html = this.MoveHiddenFieldsToBottom(html);
            }
            writer.Write(html);
        }

        public void RequestLogin()
        {
            this.Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + this.Request.Url.PathAndQuery);
        }

        public string SEOFriendlyURL(string vURL, string vExtension)
        {
            return this.ResolveUrl(Helpers.SEOFriendlyURL(vURL, vExtension));
        }

        protected void SetDateTextBoxValue(TextBox vTextBox, DateTime vDate)
        {
            if (DateTime.Compare(vDate, DateTime.MinValue) == 0)
            {
                vTextBox.Text = string.Empty;
            }
            else
            {
                vTextBox.Text = vDate.ToShortDateString();
            }
        }

        public void SetupListViewPager(int vCount, DataPager vPager)
        {
            if (!Information.IsNothing(vPager))
            {
                vPager.Visible = Conversions.ToBoolean(Interaction.IIf(vCount <= vPager.PageSize, false, true));
            }
        }

        public string BaseUrl
        {
            get
            {
                string url = this.Request.ApplicationPath;
                if (url.EndsWith("/"))
                {
                    return url;
                }
                return (url + "/");
            }
        }

        /// <summary>
        /// A flag to indicate if the whitespace will be cleaned
        /// from the HTML.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool CleanWhiteSpace
        {
            get
            {
                return this._cleanWhiteSpace;
            }
            set
            {
                this._cleanWhiteSpace = value;
            }
        }

        public string FullBaseUrl
        {
            get
            {
                return (this.Request.Url.AbsoluteUri.Replace(this.Request.Url.PathAndQuery, "") + this.BaseUrl);
            }
        }

        public bool IsAdmin
        {
            get
            {
                return (this.User.Identity.IsAuthenticated & this.User.IsInRole("Administrators"));
            }
        }

        /// <summary>
        /// Property that can be set by the child page to indicate if 
        /// hidden fields are going to be moved to the bottom of the page.
        /// Sometimes I have found that moving the fields to the bottom can cause 
        /// some runtime issues.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>The main reason you want to do this is to move the 
        /// ViewState to the bottom of the page. This will make the 
        /// content of the page render slightly faster, but it
        /// also helps with Search Engine optimization.
        /// </remarks>
        public bool MoveHiddenFields
        {
            get
            {
                return this._moveHiddenFields;
            }
            set
            {
                this._moveHiddenFields = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        protected string PageDescription
        {
            get
            {
                return this.GetMetaValue("DESCRIPTION");
            }
            set
            {
                this.CreateMetaControl("DESCRIPTION", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        protected string PageKeyWords
        {
            get
            {
                return this.GetMetaValue("KEYWORDS");
            }
            set
            {
                this.CreateMetaControl("KEYWORDS", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        protected string PageTitle
        {
            get
            {
                if (!Information.IsNothing(this.Master))
                {
                    if (this.Master.Page.Title == string.Empty)
                    {
                        return string.Empty;
                    }
                    return this.Master.Page.Title;
                }
                if (this.Page.Title == string.Empty)
                {
                    return string.Empty;
                }
                return this.Page.Title;
            }
            set
            {
                if (!Information.IsNothing(this.Master))
                {
                    this.Master.Page.Title = value;
                }
                else
                {
                    this.Page.Title = value;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="vPrimaryKey"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int this[string vPrimaryKey]
        {
            get
            {
                if (((!Information.IsNothing(RuntimeHelpers.GetObjectValue(this.ViewState[vPrimaryKey])) && Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(this.ViewState[vPrimaryKey]))) ? 1 : 0) != 0)
                {
                    return Conversions.ToInteger(this.ViewState[vPrimaryKey]);
                }
                if (((!Information.IsNothing(this.Request.QueryString[vPrimaryKey]) && Versioned.IsNumeric(this.Request.QueryString[vPrimaryKey])) ? 1 : 0) != 0)
                {
                    this.ViewState[vPrimaryKey] = Conversions.ToInteger(this.Request.QueryString[vPrimaryKey]);
                    return Conversions.ToInteger(this.Request.QueryString[vPrimaryKey]);
                }
                return 0;
            }
            set
            {
                this.ViewState[vPrimaryKey] = value;
            }
        }

        public string this[string vPrimaryKey]
        {
            get
            {
                if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(this.ViewState[vPrimaryKey])))
                {
                    return Conversions.ToString(this.ViewState[vPrimaryKey]);
                }
                if (!Information.IsNothing(this.Request.QueryString[vPrimaryKey]))
                {
                    this.ViewState[vPrimaryKey] = this.Request.QueryString[vPrimaryKey];
                    return this.Request.QueryString[vPrimaryKey];
                }
                return string.Empty;
            }
            set
            {
                this.ViewState[vPrimaryKey] = value;
            }
        }

        public string UserName
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="vPropertyname"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string this[string vPropertyname]
        {
            get
            {
                if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(this.ViewState[vPropertyname])))
                {
                    return Conversions.ToString(Conversions.ToInteger(this.ViewState[vPropertyname]));
                }
                return string.Empty;
            }
            set
            {
                this.ViewState[vPropertyname] = value;
            }
        }
    }
}

