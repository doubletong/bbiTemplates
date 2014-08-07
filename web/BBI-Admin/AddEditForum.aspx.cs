using System;
using BBICMS.Forums;
using BBICMS.UI;

partial class Admin_AddEditForum : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            
            if (ForumId > 0) {
                BindForums();
            }
            else {
                ClearItems();
                
            }
            
        }
    }
    
    protected void ClearItems()
    {
        
        ltlForumID.Text = string.Empty;
        ltlAddedDate.Text = string.Empty;
        txtTitle.Text = string.Empty;
        cbModerated.Text = string.Empty;
        txtImportance.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtImageUrl.Text = string.Empty;
        ltlUpdatedDate.Text = string.Empty;
        iLogo.Visible = false;
        
        cmdDelete.Visible = false;
            
        ltlStatus.Text = "Create a New Forum";
    }
    
    protected void BindForums()
    {
        
        using (ForumsRepository Forumrpt = new ForumsRepository()) {
            
            Forum lForum = Forumrpt.GetForumById(ForumId);
            
            if ((lForum != null)) {
                
                ltlForumID.Text = lForum.ForumID.ToString();
                txtTitle.Text = lForum.Title;
                cbModerated.Checked = lForum.Moderated;
                txtImportance.Text = lForum.Importance.ToString();
                txtDescription.Text = lForum.Description;
                
                if (string.IsNullOrEmpty(lForum.ImageUrl) == false) {
                    txtImageUrl.Text = lForum.ImageUrl;
                    iLogo.ImageUrl = lForum.ImageUrl;
                    iLogo.Visible = true;
                }
                else {
                    iLogo.Visible = false;
                }

                ltlActive.Text = lForum.Active.ToString();
                ltlAddedBy.Text = lForum.AddedBy;
                ltlAddedDate.Text = lForum.AddedDate.ToShortDateString();
                ltlUpdatedBy.Text = lForum.UpdatedBy;
                ltlUpdatedDate.Text = lForum.UpdatedDate.ToShortDateString();
                
                cmdDelete.Visible = true;
                ltlStatus.Text = "Edit The Forum";
            }
            else {
                ForumId = 0;
                ltlStatus.Text = "Create a New Forum";
                
            }
            
        }
    }
    
    protected void UpdateForums()
    {
        
        using (ForumsRepository Forumrpt = new ForumsRepository()) {
            
            Forum lForum = new Forum();
            
            if (ForumId > 0) {
                lForum = Forumrpt.GetForumById(ForumId);
            }
            else {
                lForum = new Forum();
            }
            
            lForum.Title = txtTitle.Text;
            lForum.Moderated = cbModerated.Checked;
            lForum.Importance = int.Parse( txtImportance.Text);
            lForum.Description = txtDescription.Text;
            lForum.ImageUrl = GetItemImage(fuImageURL, txtImageUrl);
            
            lForum.UpdatedDate = DateTime.Now;
            lForum.UpdatedBy = UserName;
            
            if (lForum.ForumID > 0) {
                if ((Forumrpt.AddForum(lForum) != null)) {
                    IndicateUpdated(Forumrpt, "Forum", ltlStatus, cmdDelete);
                }
                else {
                    IndicateNotUpdated(Forumrpt, "Forum", ltlStatus);
                }
            }
            else {
                
                lForum.Active = true;
                lForum.AddedBy = UserName;
                lForum.AddedDate = DateTime.Now;
                lForum.Active = true;
                if ((Forumrpt.AddForum(lForum) != null)) {
                    IndicateUpdated(Forumrpt, "Forum", ltlStatus, cmdDelete);
                }
                else {
                    IndicateNotUpdated(Forumrpt, "Forum", ltlStatus);
                }
                
            }
            
        }
    }
    
    protected void GoToForumsList()
    {
        Response.Redirect("ManageForums.aspx");
    }
    
    protected void DeleteForum()
    {
        using (ForumsRepository lForumrpt = new ForumsRepository()) {
            lForumrpt.DeleteForum(lForumrpt.GetForumById(ForumId));
        }
        GoToForumsList();
    }
    
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        DeleteForum();
    }
    
    protected void cmdUpdate_Click(object sender, System.EventArgs e)
    {
        UpdateForums();
    }
    
    protected void cmdCancel_Click(object sender, System.EventArgs e)
    {
        GoToForumsList();
    }
    
}