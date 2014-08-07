using System;
using System.Collections.Generic;
using BBICMS.Forums;
using BBICMS;
using Web.UI.Forums;

public partial class AddEditPost : ForumPage
{
    
    private bool isNewThread = false;
    private bool isNewReply = false;
    private bool isEditingPost = false;
    
    protected void AddEditPost_Init(object sender, System.EventArgs e)
    {
        base.MoveHiddenFields = false;
        base.CleanWhiteSpace = false;
    }
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        isNewThread = ((PostId == 0) & (ThreadId == 0));
        isEditingPost = !(PostId == 0);
        isNewReply = (!isNewThread & !isEditingPost);
        
        // show/hide controls and load data according to the parameters above
        if (!this.IsPostBack) {
            
            lnkThreadList.NavigateUrl = string.Format(lnkThreadList.NavigateUrl, ForumId);
            lnkThreadPage.NavigateUrl = string.Format(lnkThreadPage.NavigateUrl, ThreadId);
            txtBody.BasePath = this.BaseUrl + "FCKeditor/";
            chkClosed.Visible = isNewThread;
            
                
            BindPost();
        }
    }
    
    protected void BindPost()
    {
        using (PostsRepository lPost = new PostsRepository()) {
            
            if (isEditingPost) {
                // load the post to edit, and check that the current user has the
                // permission to do so
                Post post = lPost.GetPostById(PostId);
                if (!isModerator && !(this.User.Identity.IsAuthenticated & 
                    this.User.Identity.Name.Equals(
                        post.AddedBy.ToLower()))) {
                    this.RequestLogin();
                }
                
                lblEditPost.Visible = true;
                btnSubmit.Text = "Update";
                txtTitle.Text = post.Title;
                txtBody.Value = post.Body;
                panTitle.Visible = isModerator;
            }
            else if (isNewReply) {
                // chech whether the thread the user is adding a reply to is still open
                Post post = lPost.GetPostById(ThreadId);
                if (post.Closed) {
                    throw new ApplicationException("The thread you tried to reply to has been closed.");
                }
                
                lblNewReply.Visible = true;
                txtTitle.Text = "Re: " + post.Title;
                lblNewReply.Text = string.Format(lblNewReply.Text, post.Title);
                // if the ID of a post to be quoted is passed on the querystring, load
                // that post and prefill the new reply's body with that post's body
                if (QuotePostID > 0) {
                    Post quotePost = lPost.GetPostById(QuotePostID);
                    txtBody.Value = string.Format("<blockquote><hr noshade=\"\" size=\"1\" />" + "<b>Originally posted by {0}</b><br /><br />{1}" + "<hr noshade=\"\" size=\"1\" /></blockquote>", quotePost.AddedBy, quotePost.Body);
                }
            }
            else if (isNewThread) {
                lblNewThread.Visible = true;
                lnkThreadList.Visible = true;
                lnkThreadPage.Visible = false;
                
            }
        }
    }
    
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        
        using (PostsRepository lPostrpt = new PostsRepository()) {
            
            if (isEditingPost) {
                // when editing a post, a line containing the current Date/Time and the
                // name of the user making the edit is added to the post's body so that
                // the operation gets logged
                string body = Helpers.FilterProfanity(txtBody.Value);
                body += string.Format("<p>-- {0}: post edited by {1}.</p>", 
                    DateTime.Now.ToString(), this.User.Identity.Name);
                // edit an existing post
                Post lPostItem = lPostrpt.GetPostById(PostId);
                lPostItem.Title = txtTitle.Text;
                lPostItem.Body = body;
                
                lPostItem.UpdatedDate = DateTime.Now;
                lPostItem.UpdatedBy = UserName;
                
                lPostrpt.AddPost(lPostItem);
                panInput.Visible = false;
                panFeedback.Visible = true;
            }
            else {
                
                Post lPostItem = new Post();
                
                using (ForumsRepository lForumrpt = new ForumsRepository()) {
                    Forum forum = lForumrpt.GetForumById(ForumId);
                    
                    lPostItem.ForumId = ForumId;
                    lPostItem.ParentPostID = ThreadId;
                    lPostItem.Title = txtTitle.Text;
                    lPostItem.Body = txtBody.Value;
                    lPostItem.Closed = chkClosed.Checked;
                    lPostItem.LastPostDate = DateTime.Now;
                    lPostItem.LastPostBy = UserName;
                    lPostItem.UpdatedDate = DateTime.Now;
                    lPostItem.UpdatedBy = UserName;
                    
                    if ((forum.Moderated)) {
                        if (!isModerator) {
                            lPostItem.Approved = false;
                        }
                    }
                    
                    lPostItem.Active = true;
                    lPostItem.AddedDate = DateTime.Now;
                    lPostItem.AddedBy = UserName;
                    lPostItem.AddedByIP = Request.UserHostAddress;
                    
                    // insert the new post
                    if ((lPostrpt.AddPost(lPostItem) != null)) {
                        
                        panInput.Visible = false;
                        // increment the user's post counter
                        int lPosts = int.Parse( Profile.GetProfileGroup("Forum").GetPropertyValue("Posts").ToString());
                        Profile.GetProfileGroup("Forum").SetPropertyValue("Posts", lPosts + 1);
                        
                        // show the confirmation message saying that approval is
                        // required, according to the target forum's moderated property
                        
                        if (forum.Moderated) {
                            if (!isModerator) {
                                panApprovalRequired.Visible = true;
                            }
                            else {
                                panFeedback.Visible = true;
                            }
                        }
                        else {
                            panFeedback.Visible = true;
                        }
                        
                        //Just in case they corrected an error.
                        ltlStatus.Visible = false;
                    }
                    else {
                        
                        ltlStatus.Visible = true;
                        
                        foreach (KeyValuePair<string, Exception> kv in lForumrpt.ActiveExceptions) {
                            ltlStatus.Text += "<BR/>" + ((Exception)kv.Value).Message + "<BR/>";
                            
                        }
                        
                    }
                    
                }
                
            }
            
        }
    }
    
    
}