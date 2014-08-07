using BBICMS.Forums;
using BBICMS;
using BBICMS.UI;

partial class Admin_MoveThread : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!this.IsPostBack) {
            ForumHelper.BindForums(ddlForums, "Select Forum to Move the Thread", ForumId);
            BindPostInfo();
            
        }
    }
    
    protected void BindPostInfo()
    {
        
        using (PostsRepository lPostrpt = new PostsRepository()) {
            Post post = lPostrpt.GetPostById(ThreadId);
            lblThreadTitle.Text = post.Title;
            lblForumTitle.Text = post.ForumTitle;
            ddlForums.SelectedValue = post.ForumId.ToString();
            
        }
    }
    
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        
        using (PostsRepository lPostrpt = new PostsRepository()) {
            int lforumID = int.Parse(ddlForums.SelectedValue);
            lPostrpt.MoveThread(ThreadId, ForumId);
            this.Response.Redirect("~/BrowseThreads.aspx?ForumID=" + ForumId.ToString());
            
        }
    }
    
}