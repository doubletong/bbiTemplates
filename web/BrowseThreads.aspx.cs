using System.Web.UI.WebControls;
using BBICMS.Forums;
using Web.UI.Forums;

public partial class BrowseThreads : ForumPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Me.Master.EnablePersonalization = True
        
        if (!this.IsPostBack) {
            
            BindForums(ddlForums, "Select a Forum to Browse");
            
            lnkNewThread1.NavigateUrl = string.Format(lnkNewThread1.NavigateUrl, ForumId);
            lnkNewThread2.NavigateUrl = lnkNewThread1.NavigateUrl;
            
            BindForumInfo();
            
                
            BindData();
        }
    }
    
    protected void BindForumInfo()
    {
        using (ForumsRepository lForumrpt = new ForumsRepository()) {
            Forum lforum = lForumrpt.GetForumById(ForumId);
            
            if ((lforum != null)) {
                this.Title = string.Format(this.Title, lforum.Title);
            }
            
                
            ddlForums.SelectedValue = ForumId.ToString();
        }
    }
    
    
    protected void BindData()
    {
        
        if (ForumId > 0) {
            
            using (PostsRepository lPostrpt = new PostsRepository()) {
                
                lvThreads.DataSource = lPostrpt.GetThreads(ForumId);
                    
                lvThreads.DataBind();
                
            }
            
        }
    }
    
    protected void ddlForums_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ForumId = int.Parse( ddlForums.SelectedValue);
        BindData();
    }
    
    protected void lvThreads_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Close") {
            int threadPostID = int.Parse( e.CommandArgument.ToString());
            
            CloseThread(threadPostID);
        }
        // MB.BBICMS.BBICMS.Forums.Post.CloseThread(threadPostID)
    }
    
    protected void CloseThread(int threadPostID)
    {
        
        using (PostsRepository lPostrpt = new PostsRepository()) {
            
                
            lPostrpt.CloseThread(threadPostID);
        }
        
            
        this.BindData();
    }
    
    protected void lvThreads_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
    {
        
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;
        
        if (lvdi.ItemType == ListViewItemType.DataItem) {
            
            HyperLink hlnkMoveThread = (HyperLink)e.Item.FindControl("hlnkMoveThread");
            Post lPost = (Post)lvdi.DataItem;
            
            if ((hlnkMoveThread != null)) {
                
                    
                hlnkMoveThread.NavigateUrl = "~/Admin/MoveThread.aspx?ThreadID=" + lPost.PostID;
                
            }
            
        }
    }
    
    protected void lvThreads_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
    {
        
        using (PostsRepository lPostrpt = new PostsRepository())
        {
            if (lvThreads != null) lPostrpt.DeletePost(int.Parse( lvThreads.DataKeys[e.ItemIndex].Value.ToString()));

            BindData();
        }
    }
    
    protected void lvThreads_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindData();
    }
    
}