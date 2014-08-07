using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.Articles;
using BBICMS.BLL.Articles;
using System.Security;
using BBICMS.UI;

partial class Admin_ManageCategories : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            if (this.User.Identity.IsAuthenticated && (this.User.IsInRole("Administrators") | 
                this.User.IsInRole("Editors") | this.User.IsInRole("Contributors") | this.User.IsInRole("Posters"))) {
                BindCategories();
            }
            else {
                throw new SecurityException("You are not allowed to edit existing articles!");
                
            }
            
        }
    }
    
    protected void BindCategories()
    {
        
        using (CategoryRepository Categoryrpt = new CategoryRepository()) {
            
            List<Category> lCategories = Categoryrpt.GetActiveCategories();
            lvCategories.DataSource = lCategories;
            lvCategories.DataBind();
                
            SetupListViewPager(lCategories.Count, (DataPager)lvCategories.FindControl("pagerBottom"));
            
        }
    }
    
    protected void lvCategories_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
    {
        using (CategoryRepository lCategoryrpt = new CategoryRepository()) {
            lCategoryrpt.DeleteCategory(int.Parse(lvCategories.DataKeys[e.ItemIndex].Value.ToString()));
            BindCategories();
        }
    }
    
    protected void lvCategories_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindCategories();
    }
    
}