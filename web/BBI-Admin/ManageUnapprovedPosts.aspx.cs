using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BBICMS.Forums;
using BBICMS.UI;

partial class Admin_ManageUnapprovedPosts : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUnapprovedPosts();
        }
    }

    protected void BindUnapprovedPosts()
    {
        using (PostsRepository lPostrpt = new PostsRepository())
        {
            lvPosts.DataSource = lPostrpt.GetUnapprovedPosts();

            lvPosts.DataBind();
        }
    }

    protected void lvPosts_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Approve":

                using (PostsRepository lPostrpt = new PostsRepository())
                {
                    lPostrpt.ApprovePost(Convert.ToInt32(e.CommandArgument));
                }


                BindUnapprovedPosts();

                break;
        }
    }


    protected void lvPosts_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;

        if (lvdi.ItemType == ListViewItemType.DataItem)
        {
            HtmlImage iGoDown = (HtmlImage)e.Item.FindControl("iGoDown");
            ImageButton btnApprove = (ImageButton)e.Item.FindControl("btnApprove");
            btnApprove.OnClientClick =
                "if (confirm('Are you sure you want to approve this post?') == false) return false;";
            btnApprove.ToolTip = "Approve this post";
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            btnDelete.OnClientClick =
                "if (confirm('Are you sure you want to delete this post?') == false) return false;";
            btnDelete.ToolTip = "Delete this post";
            HtmlGenericControl dTitle = (HtmlGenericControl)e.Item.FindControl("dTitle");

            Post lPost = (Post)lvdi.DataItem;

            if ((iGoDown != null))
            {
                iGoDown.Attributes.Add("OnClick", string.Format("toggleDivState('{0}');", "body" + lPost.PostID));
            }

            if ((dTitle != null))
            {
                dTitle.Attributes.Add("OnClick", string.Format("toggleDivState('{0}');", "body" + lPost.PostID));
            }
        }
    }

    protected void lvPosts_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        using (PostsRepository lPostrpt = new PostsRepository())
        {
            lPostrpt.DeletePost(int.Parse(lvPosts.DataKeys[e.ItemIndex].Value.ToString()));

            BindUnapprovedPosts();
        }
    }
}