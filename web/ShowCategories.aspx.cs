using System;
using BBICMS.BLL.Articles;
using BBICMS.UI;

public partial class ShowCategories : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategories();
        }
    }

    protected void BindCategories()
    {
        using (CategoryRepository lCategoriesrpt = new CategoryRepository())
        {
            lvCategories.DataSource = lCategoriesrpt.GetActiveCategories();
            lvCategories.DataBind();
        }
    }

    protected void lvCategories_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindCategories();
    }
}