using System;
using BBICMS.Events;
using BBICMS;

public partial class ShowEvent : EventPage
{
    protected void Page_Load1(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        using (EventRepository lEventrpt = new EventRepository())
        {
            EventInfo lEventInfo = lEventrpt.GetEventInfoById(EventId);

            if ((lEventInfo != null))
            {
                EvTitle.Text = lEventInfo.EventTitle;
                EvDate.Text = lEventInfo.EventDate.ToShortDateString();
                EvTime.Text = lEventInfo.EventTime;
                EvLoc.Text = lEventInfo.EventLocation;
                // lblPriority.Text = ei.Importance
                EventDesc.Text = lEventInfo.EventDesc;

                lblAddress.Text = lEventInfo.Address;
                lblCity.Text = lEventInfo.City;
                lblState.Text = lEventInfo.State;
                lblZipCode.Text = lEventInfo.ZipCode;

                hlnkEdit.Visible = IsAdmin;
                hlnkEdit.NavigateUrl = "~/Admin/AddEditEvent.aspx?eventid=" + lEventInfo.EventId;

                hlnkAddToCalendar.NavigateUrl = "~/event.ics?eventid=" + lEventInfo.EventId;

                PageTitle = lEventInfo.EventTitle;

                hlnkRegister.Visible = lEventInfo.AllowRegistration;

                if (lEventInfo.AllowRegistration)
                {
                    hlnkRegister.NavigateUrl = string.Format("~/MakeEventRSVP.aspx?eventid={0}", EventId);
                }
            }
        }
    }
}