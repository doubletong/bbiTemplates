using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.Articles;
using BBICMS.BLL.Articles;
using System.Security;
using BBICMS.UI;


partial class Admin_ManageArticles : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            
            if (this.User.Identity.IsAuthenticated && (this.User.IsInRole("Administrators") | this.User.IsInRole("Editors") | this.User.IsInRole("Contributors") | this.User.IsInRole("Posters"))) {
                
                    
                BindArticles();
            }
            else {
                throw new SecurityException("You are not allowed to edit existing articles!");
                
            }
            
        }
    }
    
    protected void BindArticles()
    {
        
        using (ArticleRepository Articlerpt = new ArticleRepository()) {
            
            List<Article> lArticles = Articlerpt.GetActiveArticles();
            lvArticles.DataSource = lArticles;
            lvArticles.DataBind();
                
            SetupListViewPager(lArticles.Count, (DataPager)lvArticles.FindControl("pagerBottom"));
            
        }
    }
    
    protected void lvArticles_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
    {
        using (ArticleRepository lArticlesrpt = new ArticleRepository()) {
            lArticlesrpt.DeleteArticle(int.Parse(lvArticles.DataKeys[e.ItemIndex].Value.ToString()));
            BindArticles();
        }
    }
    
    
    protected void lvArticles_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindArticles();
    }
    
    protected void lvArticles_Sorted(object sender, System.EventArgs e)
    {
        BindArticles();
    }
    
}