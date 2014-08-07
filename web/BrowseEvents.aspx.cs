using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.Events;
using BBICMS;

public partial class BrowseEvents : EventPage
{
    
    protected void Page_Load1(object sender, EventArgs e)
    {
        
        hlnkEdit.Visible = IsAdmin;
        
        if (!IsPostBack) {
            
                
            BindData();
            
        }
    }
    
    protected void BindData()
    {
        
        using (EventRepository lEventrpt = new EventRepository()) {
            lvEvents.DataSource = lEventrpt.GetUpcomingEvents();
            lvEvents.DataBind();
            
        }
    }
    
    protected void objCalendar_DayRender(object sender, DayRenderEventArgs e)
    {
        
        using (EventRepository lEventrpt = new EventRepository()) {

            List<EventInfo> lEventList = lEventrpt.GetDaysEvents(e.Day.Date);
            if (lEventList.Count > 0) {
                
                Style EventStyle = new Style();
                
                {
                    EventStyle.BackColor = System.Drawing.Color.DarkRed;
                    EventStyle.Font.Bold = true;
                        
                    EventStyle.ForeColor = System.Drawing.Color.White;
                }
                
                e.Day.IsSelectable = true;
                    
                e.Cell.ApplyStyle(EventStyle);
            }
            else {
                
                    
                e.Day.IsSelectable = false;
                
            }
            
        }
    }
    
    protected void objCalendar_SelectionChanged(object sender, System.EventArgs e)
    {
        BindDaysEvents(objCalendar.SelectedDate);
    }
    
    protected void objCalendar_VisibleMonthChanged(object sender, System.Web.UI.WebControls.MonthChangedEventArgs e)
    {
        BindData();
    }
    
    protected void BindDaysEvents(DateTime vDate)
    {
        
        using (EventRepository lEventrpt = new EventRepository()) {
            
            List<EventInfo> lEventList = lEventrpt.GetDaysEvents(objCalendar.SelectedDate);
            
            if (lEventList.Count > 0) {
                
                lvEvents.DataSource = lEventList;
                lvEvents.DataBind();
                
                DataPager pagerBottom = (DataPager)lvEvents.FindControl("pagerBottom");
                
                if ((pagerBottom != null)) {
                    if (lEventList.Count <= pagerBottom.PageSize) {
                        pagerBottom.Visible = false;
                    }
                    else {
                        pagerBottom.Visible = true;
                    }
                    
                }
            }
            else {
                
                    
                lvEvents.Items.Clear();
                
            }
            
        }
    }
    
}