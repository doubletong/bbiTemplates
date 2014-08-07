using System.Web.UI.WebControls;
using BBICMS.BLL;
using BBICMS.Polls;
using BBICMS.UI;

public partial class ArchivedPolls : BasePage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!this.User.Identity.IsAuthenticated && !Settings.Polls.ArchiveIsPublic) {
            this.RequestLogin();
        }
        
        if (!IsPostBack) {
            BindPolls();
            
        }
    }
    
    
    protected void BindPolls()
    {
        
        using (PollsRepository lPollrpty = new PollsRepository()) {
            
            lvPolls.DataSource = lPollrpty.GetArchivedPolls();
                
            lvPolls.DataBind();
            
        }
    }
    
    protected void lvPolls_ItemCreated(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem) {
            ImageButton ibtnDelete = (ImageButton)e.Item.FindControl("ibtnDelete");
            
            ibtnDelete.Visible = this.User.Identity.IsAuthenticated & (this.User.IsInRole("Administrators") | 
                this.User.IsInRole("Editors"));
            
            ibtnDelete.OnClientClick = "if (confirm('Are you sure you want to delete this poll?') == false) return false;";
            
        }
    }
}