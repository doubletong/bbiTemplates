using System.Collections.Generic;
using BBICMS.Articles;
using BBICMS.BLL.Articles;
using System.Security;
using BBICMS.UI;

partial class Admin_ManageComments : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            
            if (this.User.Identity.IsAuthenticated && (this.User.IsInRole("Administrators") | this.User.IsInRole("Editors"))) {
                BindComments();
            }
            else {
                throw new SecurityException("You are not allowed to edit existing comments!");
            }
            
        }
    }
    
    protected void BindComments()
    {
        
        using (CommentRepository Commentrpt = new CommentRepository()) {

            List<Comment> lComments = Commentrpt.GetActiveComments();
            
            lvComments.DataSource = lComments;
            lvComments.DataBind();
            
            if (lComments.Count <= pagerBottom.PageSize) {
                pagerBottom.Visible = false;
            }
            else {
                pagerBottom.Visible = true;
                
            }
        }
    }
    
    protected void lvComments_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindComments();
    }
    
}