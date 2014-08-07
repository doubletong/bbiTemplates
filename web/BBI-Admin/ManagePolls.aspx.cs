using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.Polls;
using BBICMS.UI;

partial class Admin_ManagePolls : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPolls();
        }
    }

    protected void BindPolls()
    {
        using (PollsRepository Pollrpt = new PollsRepository())
        {
            List<Poll> lPolls = Pollrpt.GetPolls();
            lvPolls.DataSource = lPolls;
            lvPolls.DataBind();

            DataPager pagerBottom = (DataPager)lvPolls.FindControl("pagerBottom");

            if ((pagerBottom != null))
            {
                if (lPolls.Count <= pagerBottom.PageSize)
                {
                    pagerBottom.Visible = false;
                }
                else
                {
                    pagerBottom.Visible = true;
                }
            }
        }
    }

    protected void lvPolls_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Delete":
                DeletePoll(int.Parse(e.CommandArgument.ToString()));
                break;
            case "Archive":
                ArchivePoll(int.Parse(e.CommandArgument.ToString()));

                break;
        }
    }

    protected void ArchivePoll(int pollid)
    {
        using (PollsRepository Pollrpt = new PollsRepository())
        {
            Pollrpt.ArchivePoll(pollid);
        }
    }

    protected void DeletePoll(int pollId)
    {
        using (PollsRepository Pollrpt = new PollsRepository())
        {
            Pollrpt.DeletePoll(pollId);
        }

        BindPolls();
    }

    protected void lvPolls_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        DeletePoll(int.Parse(lvPolls.DataKeys[e.ItemIndex].Value.ToString()));
    }
}