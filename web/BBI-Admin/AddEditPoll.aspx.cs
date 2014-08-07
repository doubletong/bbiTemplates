using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.BLL;
using BBICMS.Polls;
using BBICMS.UI;

partial class Admin_AddEditPoll : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (PollId > 0)
            {
                BindPoll();
            }
            else
            {
                ClearPoll();
            }
        }
    }

    protected void BindPoll()
    {
        using (PollsRepository Pollrpt = new PollsRepository())
        {
            Poll vPoll = Pollrpt.GetPollById(PollId);

            if ((vPoll != null))
            {
                lblPollId.Text = vPoll.PollID.ToString();
                lblDateAdded.Text = vPoll.AddedDate.ToShortDateString();
                lblAddedBy.Text = vPoll.AddedBy;
                lblDateUpdated.Text = vPoll.UpdatedDate.ToShortDateString();
                lblUpdatedBy.Text = vPoll.UpdatedBy;
                lblVotes.Text = vPoll.Votes.ToString();
                txtQuestion.Text = vPoll.QuestionText;
                cbIsCurrent.Checked = vPoll.IsCurrent;

                BindPollOptions();

                lbtnInsertPoll.Text = "Update";


                tOptionDetail.Visible = true;
            }
        }
    }

    protected void ClearPoll()
    {
        lblPollId.Text = string.Empty;
        lblDateAdded.Text = string.Empty;
        lblAddedBy.Text = string.Empty;
        lblDateUpdated.Text = string.Empty;
        lblUpdatedBy.Text = string.Empty;
        lblVotes.Text = string.Empty;
        txtQuestion.Text = string.Empty;
        cbIsCurrent.Checked = false;
        lbtnInsertPoll.Text = "Insert";


        tOptionDetail.Visible = false;
    }

    protected void lbtnInsertPoll_Click(object sender, EventArgs e)
    {
        using (PollsRepository Pollrpt = new PollsRepository())
        {
            Poll vPoll = Pollrpt.GetPollById(PollId);

            if ((vPoll == null))
            {
                vPoll = new Poll();
            }

            vPoll.QuestionText = txtQuestion.Text;
            vPoll.IsCurrent = cbIsCurrent.Checked;


            vPoll.UpdatedBy = UserName;
            vPoll.UpdatedDate = DateTime.Now;

            if (vPoll.PollID > 0)
            {
                if ((Pollrpt.AddPoll(vPoll) != null))
                {
                    ltlStatus.Text = "The Poll Has Been Updated.";
                }
                else
                {
                    ltlStatus.Text = "The Poll Has Not Been Updated.";
                }
            }
            else
            {
                vPoll.AddedBy = UserName;
                vPoll.AddedDate = DateTime.Now;
                if ((Pollrpt.AddPoll(vPoll) != null))
                {
                    ltlStatus.Text = "The Poll Has Been Added.";
                    tOptionDetail.Visible = true;
                }
                else
                {
                    ltlStatus.Text = "The Poll Has Not Been Added.";
                }
            }
        }
    }

    protected void BindPollOptions()
    {
        using (PollOptionsRepository PollOptionRpt = new PollOptionsRepository())
        {
            lvPollOptions.DataSource = PollOptionRpt.GetActivePollOptionsByPollId(PollId);

            lvPollOptions.DataBind();
        }
    }

    protected void lbInsert_Click(object sender, EventArgs e)
    {
        UpdatePollOptions();
    }

    protected void UpdatePollOptions()
    {
        using (PollOptionsRepository PollOptionsrpt = new PollOptionsRepository())
        {
            PollOption lPollOption = new PollOption();

            if (PollOptionId > 0)
            {
                lPollOption = PollOptionsrpt.GetPollOptionById(PollOptionId);
            }
            else
            {
                lPollOption = new PollOption();
            }

            lPollOption.PollId = PollId;
            lPollOption.OptionText = txtOption.Text;

            lPollOption.UpdatedDate = DateTime.Now;
            lPollOption.UpdatedBy = UserName;


            if (lPollOption.OptionID > 0)
            {
                if ((PollOptionsrpt.AddPollOption(lPollOption) != null))
                {
                    IndicateOptionUpdated();
                }
                else
                {
                    IndicateOptionNotUpdated(PollOptionsrpt);
                }
            }
            else
            {
                lPollOption.Active = true;
                lPollOption.AddedBy = UserName;
                lPollOption.AddedDate = DateTime.Now;
                if ((PollOptionsrpt.AddPollOption(lPollOption) != null))
                {
                    IndicateOptionUpdated();
                }
                else
                {
                    IndicateOptionNotUpdated(PollOptionsrpt);
                }
            }


            lbInsert.Text = "Insert";
        }
    }

    protected void IndicateOptionNotUpdated(BaseRepository vRepository)
    {
        ltlStatus.Text = string.Empty;
        if (vRepository.ActiveExceptions.Count > 0)
        {
            foreach (KeyValuePair<String, Exception> kv in vRepository.ActiveExceptions)
            {
                ltlStatus.Text += ((Exception) kv.Value).Message + "<BR/>";
            }
        }
        else
        {
            ltlStatus.Text = "The Option Has Not Been Updated.";
        }
    }

    protected void IndicateOptionUpdated()
    {
        ltlStatus.Text = "The Option Has Been Updated.";
        // cmdDelete.Visible = True
        txtOption.Text = string.Empty;
        BindPollOptions();
    }

    protected void DeletePollOption(int OptionId)
    {
        using (PollOptionsRepository PollOptionsrpt = new PollOptionsRepository())
        {
            PollOptionsrpt.DeletePollOption(PollOptionsrpt.GetPollOptionById(OptionId));
        }
        BindPollOptions();
    }

    protected void lvPollOptions_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        DeletePollOption(int.Parse(lvPollOptions.DataKeys[e.ItemIndex].Value.ToString()));
    }

    protected void lvPollOptions_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        PollOptionId = int.Parse(lvPollOptions.DataKeys[e.NewEditIndex].Value.ToString());

        using (PollOptionsRepository lPollOptionrpt = new PollOptionsRepository())
        {
            PollOption lPollOption = lPollOptionrpt.GetPollOptionById(PollOptionId);

            txtOption.Text = lPollOption.OptionText;

            lbInsert.Text = "Update";
        }
    }
}