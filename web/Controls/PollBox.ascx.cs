using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using BBICMS;
using BBICMS.Polls;

public partial class PollBox : UserControl
{
    #region " Property "

    public static readonly BBICMSSection Settings = Helpers.Settings;
    private int _pollID = -1;


    [Personalizable(PersonalizationScope.Shared), WebBrowsable(), WebDisplayName("Show Archive Link"),
     WebDescription("Specifies whether the link to the archive page is displayed")]
    public bool ShowArchiveLink
    {
        get { return lnkArchive.Visible; }
        set { lnkArchive.Visible = value; }
    }

    [Personalizable(PersonalizationScope.Shared), WebBrowsable(), WebDisplayName("Poll ID"),
     WebDescription("The ID of the poll to show")]
    public int PollID
    {
        get { return _pollID; }
        set { _pollID = value; }
    }

    public bool ShowQuestion
    {
        get { return lblQuestion.Visible; }
        set { lblQuestion.Visible = value; }
    }

    #endregion

    public int TotalVotes
    {
        get
        {
            if ((ViewState["TotalVotes"] != null))
            {
                return int.Parse(ViewState["TotalVotes"].ToString());
            }
            return 0;
        }
        set { ViewState["TotalVotes"] = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Page.RegisterRequiresControlState(this);
    }

    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[])savedState;
        base.LoadControlState(ctlState[0]);
        PollID = (int) ctlState[1];
    }

    protected override object SaveControlState()
    {
        object[] ctlState = new object[2];
        ctlState[0] = base.SaveControlState();
        ctlState[1] = this.PollID;
        return ctlState;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) DoBinding();
    }

    public override void DataBind()
    {
        // the call to the base DataBind makes a call to OnDataBinding,
        // which parses and evaluates the control's binding expressions, i.e. the PollID prop
        base.DataBind();
        // with the PollID set, do the actual binding
        DoBinding();
    }


    protected void DoBinding()
    {
        panResults.Visible = false;
        panVote.Visible = false;

        using (PollsRepository Pollrpty = new PollsRepository())
        {
            int lpollID = PollID == -1 ? Pollrpty.CurrentPollID : PollID;

            if (lpollID > -1)
            {
                Poll lpoll = Pollrpty.GetPollById(lpollID, false);

                if ((lpoll != null))
                {
                    lblQuestion.Text = lpoll.QuestionText;
                    TotalVotes = lpoll.Votes;
                    lblTotalVotes.Text = TotalVotes.ToString();
                    valRequireOption.ValidationGroup += lpoll.PollID.ToString();
                    btnVote.ValidationGroup = valRequireOption.ValidationGroup;

                    if (lpoll.IsArchived | GetUserVote(lpollID) > 0)
                    {
                        lvOptions.DataSource = lpoll.PollOptions;
                        lvOptions.DataBind();
                        panResults.Visible = true;
                    }
                    else
                    {
                        optlOptions.DataSource = lpoll.PollOptions;
                        optlOptions.DataBind();
                        panVote.Visible = true;
                    }
                }
            }
        }
    }

    protected void btnVote_Click(object sender, EventArgs e)
    {
        using (PollsRepository Pollrpty = new PollsRepository())
        {
            int lpollID = PollID == -1 ? Pollrpty.CurrentPollID : PollID;

            // check that the user has not already voted for this poll
            int userVote = GetUserVote(lpollID);
            if (userVote == 0)
            {
                // post the vote and tehn create a cookie to remember this user's vote
                userVote = Convert.ToInt32(optlOptions.SelectedValue);

                using (PollOptionsRepository PollOptionRptry = new PollOptionsRepository())
                {
                    PollOptionRptry.Vote(userVote);
                }

                // hide the panel with the radio buttons, and show the results
                DoBinding();
                panVote.Visible = false;
                panResults.Visible = true;

                DateTime expireDate = DateTime.Now.AddDays(Settings.Polls.VotingLockInterval);
                string key = "Vote_Poll" + lpollID;

                // save the result to the cookie
                if (Settings.Polls.VotingLockByCookie)
                {
                    HttpCookie cookie = new HttpCookie(key, userVote.ToString());
                    cookie.Expires = expireDate;
                    Response.Cookies.Add(cookie);
                }

                // save the vote also to the cache
                if (Settings.Polls.VotingLockByIP)
                {
                    Cache.Insert(Request.UserHostAddress + "_" + key, userVote);
                }
            }
        }
    }

    protected int GetUserVote(int vpollID)
    {
        string key = "Vote_Poll" + vpollID;
        string key2 = Request.UserHostAddress + "_" + key;

        // check if the vote is in the cache
        if (Settings.Polls.VotingLockByIP & (Cache[key2] != null))
        {
            return (int) Cache[key2];
        }

        // if the vote is not in cache, check if there's a client-side cookie
        if (Settings.Polls.VotingLockByCookie)
        {
            HttpCookie cookie = Request.Cookies[key];
            if ((cookie != null))
            {
                return int.Parse(cookie.Value);
            }
        }

        return 0;
    }

    protected int GetFixedPercentage(int vVotes, int vTotalVotes)
    {
        double val = (vVotes*100)/vTotalVotes > 0 ? vTotalVotes : 1;
        int percentage = (int)val;
        
        switch (percentage)
        {
            case 100:
                percentage = 98;
                break;
            case -1:
                percentage = 0;
                break;
        }
        return percentage;
    }
}