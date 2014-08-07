using System;
using System.Security;
using BBICMS.Articles;
using BBICMS.BLL.Articles;
using BBICMS.UI;

partial class Admin_AddEditComment : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated &&
                (User.IsInRole("Administrators") | User.IsInRole("Editors") | User.IsInRole("Contributors") |
                 User.IsInRole("Posters")))
            {
                if (CommentId > 0)
                {
                    BindComment();
                }
                else
                {
                    ClearItems();
                }
            }
            else
            {
                throw new SecurityException("You are not allowed to edit existing comments!");
            }
        }
    }

    protected void ClearItems()
    {
        ltlCommentID.Text = string.Empty;
        ltlAddedDate.Text = string.Empty;
        ltlAddedByEMail.Text = string.Empty;
        ltlAddedByIP.Text = string.Empty;
        ltlArticleTitle.Text = string.Empty;
        ltlTitle.Text = string.Empty;
        ltlBody.Text = string.Empty;
        ltlCommenterURL.Text = string.Empty;
        cbApproved.Checked = false;
        cbApproved.Enabled = false;
        ltlUpdatedDate.Text = string.Empty;
        cmdUpdate.Visible = false;


        ltlStatus.Text = "You Cannot Create a New Comments.";
    }

    protected void BindComment()
    {
        using (CommentRepository Commentrpt = new CommentRepository())
        {
            Comment lComments = Commentrpt.GetCommentById(CommentId);

            if ((lComments != null))
            {
                ltlCommentID.Text = lComments.CommentID.ToString();
                ltlAddedDate.Text = lComments.AddedDate.ToShortDateString();
                ltlAddedByEMail.Text = lComments.AddedByEmail;
                ltlAddedByIP.Text = lComments.AddedByIP;
                ltlArticleTitle.Text = lComments.Article.Title;
                ltlBody.Text = lComments.Body;
                cbApproved.Checked = lComments.Approved;

                ltlUpdatedDate.Text = lComments.UpdatedDate.ToShortDateString();
            }
            else
            {
                CommentId = 0;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UpdateComment();
    }

    protected void UpdateComment()
    {
        using (CommentRepository Commentrpt = new CommentRepository())
        {
            Comment lComments = new Comment();

            if (CommentId > 0)
            {
                lComments = Commentrpt.GetCommentById(CommentId);
            }
            else
            {
                lComments = new Comment();
            }

            lComments.Approved = cbApproved.Checked;

            if (lComments.CommentID > 0)
            {
                if ((Commentrpt.AddComment(lComments) != null))
                {
                    ltlStatus.Text = string.Format("The Comments Has Been {0}.",
                                                   lComments.Approved ? "Approved" : "Unapproved");
                }
                else
                {
                    ltlStatus.Text = "The Comment Has Not Been Updated.";
                }
            }
            else
            {
                throw new NotSupportedException("You cannot add a new comment from the admin.");
            }
        }
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        using (CommentRepository Commentrpt = new CommentRepository())
        {
            Commentrpt.DeleteComment(CommentId);
        }

        GoToManageComments();
    }

    protected void GoToManageComments()
    {
        Response.Redirect("Managecomments.aspx");
    }
}