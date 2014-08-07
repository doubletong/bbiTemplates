using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.UI.WebControls;
using BBICMS.Forums;
using BBICMS;
using Web.UI.Forums;

public partial class ShowThread : ForumPage
{
    private Hashtable profiles = new Hashtable();

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessInitialView();
            BindData();
        }
    }

    protected void ProcessInitialView()
    {
        using (PostsRepository lPostrpt = new PostsRepository())
        {
            Post lPost = lPostrpt.GetPostById(ThreadId);
            lPostrpt.IncrementViewCount(lPost);
            this.Title = string.Format(this.Title, lPost.Title);
            lblPageTitle.Text = string.Format(lblPageTitle.Text, lPost.ForumId, lPost.ForumTitle, lPost.Title);
            ShowCommandButtons(lPost.Closed, lPost.ForumId, ThreadId, lPost.AddedBy);
        }
    }

    protected void BindData()
    {
        using (PostsRepository lPostrpt = new PostsRepository())
        {
            List<Post> lPosts = lPostrpt.GetThread(ThreadId);
            lvPosts.DataSource = lPosts;
            lvPosts.DataBind();

            DataPager pagerBottom = (DataPager) lvPosts.FindControl("pagerBottom");
            if (lPosts.Count <= pagerBottom.PageSize)
            {
                pagerBottom.Visible = false;
            }
            else
            {
                pagerBottom.Visible = true;
            }
        }
    }

    // Retrieves and returns the profile of the specified user. The profile is cached once
    // retrieved for the first time, so that it is reused if the profile for the same user
    // will be requested more times on the same request
    protected ProfileBase GetPostUserProfile(object userName)
    {
        string name = (string) userName;
        if (!profiles.Contains(name))
        {
            ProfileBase profile = Helpers.GetUserProfile(name);
            profiles.Add(name, profile);
            return profile;
        }
        else
        {
            return (ProfileBase) profiles[userName];
        }
    }


    protected string GetPosterDescription(int posts)
    {
        if (posts >= Settings.Forums.GoldPosterPosts)
        {
            return Settings.Forums.GoldPosterDescription;
        }
        else if (posts >= Settings.Forums.SilverPosterPosts)
        {
            return Settings.Forums.SilverPosterDescription;
        }
        else if (posts >= Settings.Forums.BronzePosterPosts)
        {
            return Settings.Forums.BronzePosterDescription;
        }
        else
        {
            return string.Empty;
        }
    }

    protected void ShowCommandButtons(bool isClosed, int forumID, int threadPostID, string addedBy)
    {
        if (isClosed)
        {
            lnkNewReply1.Visible = false;
            lnkNewReply2.Visible = false;
            btnCloseThread1.Visible = false;
            btnCloseThread2.Visible = false;
            panClosed.Visible = true;
        }
        else
        {
            lnkNewReply1.NavigateUrl = string.Format(lnkNewReply1.NavigateUrl, forumID, threadPostID);
            lnkNewReply2.NavigateUrl = lnkNewReply1.NavigateUrl;
            btnCloseThread1.Visible = IsModeratorOrPoster(addedBy);
            btnCloseThread2.Visible = btnCloseThread1.Visible;
        }
    }

    protected void lvPosts_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteThread":
                using (PostsRepository lPostrpt = new PostsRepository())
                {
                    int threadPostID = Convert.ToInt32(e.CommandArgument);
                    int forumID = lPostrpt.GetPostById(threadPostID).PostID;
                    lPostrpt.DeletePost(threadPostID);
                    this.Response.Redirect("BrowseThreads.aspx?ForumID=" + forumID.ToString());
                }


                break;
            case "DeletePost":
                using (PostsRepository lPostrpt = new PostsRepository())
                {
                    int postID = Convert.ToInt32(e.CommandArgument);
                    lPostrpt.DeletePost(postID);

                    DataPager pagerBottom = (DataPager) lvPosts.FindControl("pagerBottom");
                    pagerBottom.SetPageProperties(0, Settings.Forums.PostsPageSize, false);
                    lvPosts.DataBind();
                }


                break;
        }
    }


    protected void lvPosts_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem lvdi = (ListViewDataItem) e.Item;
            Post lPost = (Post) lvdi.DataItem;
            int threadID = lPost.ParentPostID;
            if (lPost.IsFirstPost) threadID = lPost.PostID;

            if ((lPost != null))
            {
                // the link for editing the post is visible to the post's author, and to
                // administrators, editors and moderators
                HyperLink lnkEditPost = (HyperLink) e.Item.FindControl("lnkEditPost");


                lnkEditPost.NavigateUrl = string.Format(lnkEditPost.NavigateUrl, lPost.ForumId, threadID, lPost.PostID);
                lnkEditPost.Visible = IsModeratorOrPoster(lPost.AddedBy.ToLower());

                // the link for deleting the thread/post is visible only to administrators, editors and moderators
                ImageButton btnDeletePost = (ImageButton) e.Item.FindControl("btnDeletePost");
                if (lPost.IsFirstPost)
                {
                    btnDeletePost.OnClientClick = string.Format(btnDeletePost.OnClientClick, "entire thread");
                    btnDeletePost.CommandName = "DeleteThread";
                }
                else
                {
                    btnDeletePost.OnClientClick = string.Format(btnDeletePost.OnClientClick, "post");
                    btnDeletePost.CommandName = "DeletePost";
                }
                btnDeletePost.CommandArgument = lPost.PostID.ToString();
                btnDeletePost.Visible = isModerator;

                // if the thread is not closed, show the link to quote the post
                HyperLink lnkQuotePost = (HyperLink) e.Item.FindControl("lnkQuotePost");
                lnkQuotePost.NavigateUrl = string.Format(lnkQuotePost.NavigateUrl, lPost.ForumId, threadID, lPost.PostID);
                if (lPost.IsFirstPost)
                {
                    lnkQuotePost.Visible = !lPost.Closed;
                }
                else
                {
                    lnkQuotePost.Visible = !lPost.ParentPost.Closed;
                }
            }
        }
    }

    protected void btnCloseThread2_Click(object sender, EventArgs e)
    {
        using (PostsRepository lPostrpt = new PostsRepository())
        {
            lPostrpt.CloseThread(ThreadId);
            ShowCommandButtons(true, 0, 0, string.Empty);
            lvPosts.DataBind();
        }
    }

    protected void lvPosts_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
    {
        using (PostsRepository lPostrpt = new PostsRepository())
        {
            lPostrpt.DeletePost(int.Parse(lvPosts.DataKeys[e.ItemIndex].Value.ToString()));

            BindData();
        }
    }
}