using System;
using BBICMS.Events;
using BBICMS.UI;

partial class Admin_AddEditEvent : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            
            if (EventId > 0) {
                BindEvent();
            }
            else {
                ClearItems();
                
            }
            
        }
    }
    
    protected void ClearItems()
    {
        
        ltlEventId.Text = string.Empty;
        txtEventTitle.Text = string.Empty;
        txtEventDesc.Text = string.Empty;
        txtImportance.Text = string.Empty;
        txtEventDate.Text = string.Empty;
        txtEventEndDate.Text = string.Empty;
        txtEventExpires.Text = string.Empty;
        txtEventTime.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        txtEventLocation.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtImportance.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtState.Text = string.Empty;
        txtZipCode.Text = string.Empty;
        
        cbFeatured.Checked = false;
        cbRegistration.Checked = false;
        
        ltlActive.Text = string.Empty;
        ltlAddedBy.Text = string.Empty;
        ltlDateAdded.Text = string.Empty;
        ltlUpdatedBy.Text = string.Empty;
        ltlDateUpdated.Text = string.Empty;
        
        cbFeatured.Checked = false;
        
            
        ltlStatus.Text = "Create a New Event";
    }
    
    protected void BindEvent()
    {
        
        using (EventRepository lEventrpt = new EventRepository()) {
            
            EventInfo lEventInfo = lEventrpt.GetEventInfoById(EventId);
            
            if ((lEventInfo != null)) {
                
                ltlEventId.Text = lEventInfo.EventId.ToString();
                txtEventTitle.Text = lEventInfo.EventTitle;
                txtEventDesc.Text = lEventInfo.EventDesc;
                txtImportance.Text = lEventInfo.Importance.ToString();
                
                SetDateTextBoxValue(txtEventDate, lEventInfo.EventDate);
                SetDateTextBoxValue(txtEventEndDate, lEventInfo.EventEndDate ?? DateTime.Now);
                SetDateTextBoxValue(txtEventExpires, lEventInfo.EventExpires);
                
                txtEventTime.Text = lEventInfo.EventTime;
                txtEndTime.Text = lEventInfo.EndTime;
                txtEventLocation.Text = lEventInfo.EventLocation;
                txtAddress.Text = lEventInfo.Address;
                txtCity.Text = lEventInfo.City;
                txtState.Text = lEventInfo.State;
                txtZipCode.Text = lEventInfo.ZipCode;
                txtDuration.Text = lEventInfo.Duration;
                
                cbFeatured.Checked = lEventInfo.Featured ?? false;
                cbRegistration.Checked = lEventInfo.AllowRegistration;

                ltlActive.Text = lEventInfo.Active.ToString();
                ltlAddedBy.Text = lEventInfo.AddedBy;
                ltlDateAdded.Text = lEventInfo.DateAdded.ToShortDateString();
                ltlUpdatedBy.Text = lEventInfo.UpdatedBy;
                ltlDateUpdated.Text = lEventInfo.DateUpdated.ToShortDateString();
                
                cbFeatured.Checked = false;
                
                    
                ltlStatus.Text = "Create a New Event";
                
            }
            
        }
    }
    
    protected void UpdateEvent()
    {
        
        using (EventRepository lEventrpt = new EventRepository()) {
            EventInfo lEventInfo = new EventInfo();
            
            if (EventId > 0) {
                lEventInfo = lEventrpt.GetEventInfoById(EventId);
            }
            else {
                lEventInfo = new EventInfo();
            }
            
            lEventInfo.EventTitle = txtEventTitle.Text;
            lEventInfo.EventDesc = txtEventDesc.Text;
            lEventInfo.Importance = int.Parse( txtImportance.Text);
            
           txtEventDate.Text = lEventInfo.EventDate.ToShortDateString();
            txtEventEndDate.Text = lEventInfo.EventEndDate.ToString();
            txtEventExpires.Text = lEventInfo.EventExpires.ToShortDateString();
            
            lEventInfo.EventTime = txtEventTime.Text;
            lEventInfo.EndTime = txtEndTime.Text;
            lEventInfo.EventLocation = txtEventLocation.Text;
            lEventInfo.Address = txtAddress.Text;
            lEventInfo.City = txtCity.Text;
            lEventInfo.State = txtState.Text;
            lEventInfo.ZipCode = txtZipCode.Text;
            lEventInfo.Duration = txtDuration.Text;
            
            lEventInfo.Featured = cbFeatured.Checked;
            lEventInfo.AllowRegistration = cbRegistration.Checked;
            
            lEventInfo.DateUpdated = DateTime.Now;
            lEventInfo.UpdatedBy = UserName;
            
            if (lEventInfo.EventId > 0) {
                if ((lEventrpt.AddEventInfo(lEventInfo) != null)) {
                    IndicateUpdated(lEventrpt, "Event", ltlStatus, cmdDelete);
                }
                else {
                    IndicateNotUpdated(lEventrpt, "Event", ltlStatus);
                }
            }
            else {
                lEventInfo.Active = true;
                lEventInfo.AddedBy = UserName;
                lEventInfo.DateAdded = DateTime.Now;
                if ((lEventrpt.AddEventInfo(lEventInfo) != null)) {
                    IndicateUpdated(lEventrpt, "Event", ltlStatus, cmdDelete);
                }
                else {
                    IndicateNotUpdated(lEventrpt, "Event", ltlStatus);
                }
                
            }
        }
    }
    
    protected void DeleteEvent()
    {
        using (EventRepository lEventrpt = new EventRepository()) {
            lEventrpt.DeleteEventInfo(lEventrpt.GetEventInfoById(EventId));
        }
        GoToEventList();
    }
    
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        UpdateEvent();
    }
    
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        DeleteEvent();
    }
    
    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        GoToEventList();
    }
    
    protected void GoToEventList()
    {
        Response.Redirect("ManageEvents.aspx");
    }
    
}