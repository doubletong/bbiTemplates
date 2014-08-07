using System;
using BBICMS.Forums;
using Web.UI.Forums;

public partial class ShowForums : ForumPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        using (ForumsRepository lForumrpt = new ForumsRepository())
        {
            lvForums.DataSource = lForumrpt.GetForums();

            lvForums.DataBind();
        }
    }
}