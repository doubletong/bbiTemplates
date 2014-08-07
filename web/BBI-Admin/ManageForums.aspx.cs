using System;
using System.Web.UI.WebControls;
using BBICMS.Forums;
using BBICMS.UI;

partial class Admin_ManageForums : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindForums();
        }
    }

    protected void BindForums()
    {
        using (ForumsRepository lForumsrpt = new ForumsRepository())
        {
            lvForums.DataSource = lForumsrpt.GetActiveForums();
            lvForums.DataBind();
        }
    }

    protected void lvForums_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Delete":
                using (ForumsRepository lForumrpt = new ForumsRepository())
                {
                    lForumrpt.DeleteForum(lForumrpt.GetForumById(int.Parse(e.CommandArgument.ToString())));
                }

                BindForums();

                break;
        }
    }

    protected void lvForums_ItemDeleted(object sender, ListViewDeletedEventArgs e)
    {
    }

    protected void lvForums_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindForums();
    }
}