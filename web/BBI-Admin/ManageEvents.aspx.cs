using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.Events;
using BBICMS.UI;

partial class Admin_ManageEvents : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindEvents();
        }
    }

    protected void BindEvents()
    {
        using (EventRepository lEventrpt = new EventRepository())
        {
            List<EventInfo> lEvents = lEventrpt.GetEvents();
            lvEvents.DataSource = lEvents;
            lvEvents.DataBind();


            SetupListViewPager(lEvents.Count, (DataPager) lvEvents.FindControl("pagerBottom"));
        }
    }

    protected void lvEvents_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindEvents();
    }

    protected void lvEvents_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        using (EventRepository lEventrpt = new EventRepository())
        {
            lEventrpt.DeleteEventInfo(int.Parse(lvEvents.DataKeys[e.ItemIndex].Value.ToString()));
        }
    }
}