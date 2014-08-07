using System;
using System.Web.UI;
using BBICMS;

partial class Controls_ThemeSelector : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Helpers.ThemesSelectorID.Length == 0)
        {
            Helpers.ThemesSelectorID = ddlThemes.UniqueID;
        }

        ddlThemes.DataSource = Helpers.GetThemes();
        ddlThemes.DataBind();

        ddlThemes.SelectedValue = Page.Theme;
    }
}