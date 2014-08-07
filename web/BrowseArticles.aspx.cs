using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BBICMS.Articles;
using BBICMS;
using BBICMS.BLL.Articles;
using BBICMS.UI.Articles;

public partial class BrowseArticles : ArticlePage
{
    #region " Properties "

    private bool _enableHighlighter = true;
    private bool _enablePaging = true;
    private int _localCategoryId;
    private bool _publishedOnly = true;
    private bool _showCategoryPicker = true;
    private bool _showPageSizePicker = true;
    private string _userCity = string.Empty;
    private string _userCountry = string.Empty;
    private string _userState = string.Empty;

    public bool EnableHighlighter
    {
        get { return _enableHighlighter; }
        set { _enableHighlighter = value; }
    }

    public bool PublishedOnly
    {
        get { return _publishedOnly; }
        set { _publishedOnly = value; }
    }

    public bool ShowCategoryPicker
    {
        get { return _showCategoryPicker; }
        set
        {
            _showCategoryPicker = value;
            ddlCategories.Visible = value;
            lblCategoryPicker.Visible = value;
            lblSeparator.Visible = value;
        }
    }

    public bool ShowPageSizePicker
    {
        get { return _showPageSizePicker; }
        set
        {
            _showPageSizePicker = value;
            ddlArticlesPerPage.Visible = value;
            lblPageSizePicker.Visible = value;
        }
    }

    public bool EnablePaging
    {
        get { return _enablePaging; }
        set
        {
            _enablePaging = value;
            pagerBottom.Visible = value;
        }
    }

    private bool _userCanEdit;
    public bool UserCanEdit
    {
        get { return _userCanEdit; }
        set { _userCanEdit = value; }
    }

    private string _description;
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public int LocalCategoryId
    {
        get { return _localCategoryId > 0 ? _localCategoryId : CategoryId; }
        set { _localCategoryId = value; }
    }

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        UserCanEdit = (Page.User.Identity.IsAuthenticated &&
                       (Page.User.IsInRole("Administrators") || Page.User.IsInRole("Editors")));

        try
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                ProfileBase profile = Helpers.GetUserProfile();
                _userCountry = Helpers.GetProfileSubGroupStringProperty(profile, "Address", "Country").ToLower();
                _userState = Helpers.GetProfileSubGroupStringProperty(profile, "Address", "State").ToLower();
                _userCity = Helpers.GetProfileSubGroupStringProperty(profile, "Address", "City").ToLower();
            }
        }
        catch (Exception ex)
        {
        }

        base.MoveHiddenFields = true;
        UserCanEdit = true;
        Description = "This is the Description.";

        Page.Title = "The Beer House News";
    }

    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[])savedState;
        base.LoadControlState(ctlState[0]);
        EnableHighlighter = (bool) ctlState[1];
        PublishedOnly = (bool) ctlState[2];
        ShowCategoryPicker = (bool) ctlState[3];
        ShowPageSizePicker = (bool) ctlState[4];
        EnablePaging = (bool) ctlState[5];
    }

    protected override object SaveControlState()
    {
        object[] ctlState = null;
        // ERROR: Not supported in C#: ReDimStatement 

        ctlState[0] = base.SaveControlState();
        ctlState[1] = EnableHighlighter;
        ctlState[2] = PublishedOnly;
        ctlState[3] = ShowCategoryPicker;
        ctlState[4] = ShowPageSizePicker;
        ctlState[5] = EnablePaging;

        return ctlState;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategoriesToListControl(ddlCategories, LocalCategoryId);

            int pageSize = Helpers.Settings.Articles.PageSize;
            if ((ddlArticlesPerPage.Items.FindByValue(pageSize.ToString()) == null))
            {
                ddlArticlesPerPage.Items.Add(new ListItem(pageSize.ToString(), pageSize.ToString()));
            }
            ddlArticlesPerPage.SelectedValue = pageSize.ToString();
            pagerBottom.PageSize = pageSize;
            SetPageSize();

            BindArticles();
        }
    }

    protected void BindArticles()
    {
        using (ArticleRepository Articlerpt = new ArticleRepository())
        {
            List<Article> lArticles = new List<Article>();

            if (LocalCategoryId > 0)
            {
                lArticles = Articlerpt.GetArticlesByCategory(LocalCategoryId, PublishedOnly);
            }
            else
            {
                lArticles = Articlerpt.GetArticles(PublishedOnly);
            }

            lvArticles.DataSource = lArticles;
            lvArticles.DataBind();

            if (lArticles.Count <= pagerBottom.PageSize)
            {
                pagerBottom.Visible = false;
            }
            else
            {
                pagerBottom.Visible = true;
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        HtmlMeta metaDescription = new HtmlMeta();
        metaDescription.Name = "Description";
        metaDescription.Content = "This is the Description.";

        Page.Header.Controls.Add(metaDescription);
    }

    protected void lvArticles_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;

        if (lvdi.ItemType == ListViewItemType.DataItem && Page.User.Identity.IsAuthenticated && EnableHighlighter)
        {
            Article lArticle = (Article)lvdi.DataItem;
            HtmlGenericControl dRow = (HtmlGenericControl)lvdi.FindControl("dRow");

            if ((lArticle != null))
            {
                if (string.IsNullOrEmpty(lArticle.Country) == false)
                {
                    if (lArticle.Country.ToLower() == _userCountry)
                    {
                        dRow.Attributes.Add("class", "highlightcountry");

                        if (Array.IndexOf(lArticle.State.ToLower().Split(';'), _userState) > -1)
                        {
                            dRow.Attributes.Add("class", "highlightstate");

                            if (Array.IndexOf(lArticle.City.ToLower().Split(';'), _userCity) > -1)
                            {
                                dRow.Attributes.Add("class", "highlightcity");
                            }
                        }
                    }
                }
            }
        }
    }

    protected void lvArticles_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindArticles();
    }

    protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        LocalCategoryId = int.Parse(ddlCategories.SelectedValue);
        BindArticles();
    }

    protected void SetPageSize()
    {
        pagerBottom.SetPageProperties(0, int.Parse(ddlArticlesPerPage.SelectedValue), false);
    }

    protected void ddlArticlesPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetPageSize();

        BindArticles();
    }
}