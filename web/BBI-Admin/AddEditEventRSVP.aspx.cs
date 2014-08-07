using System;
using System.Collections.Generic;
using BBICMS.Events;
using BBICMS;
using BBICMS.BLL;
using System.Linq;

partial class Admin_AddEditEventRSVP : EventPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            
            BindEvents();
            
            if (EventRSVPId > 0) {
                BindEventRSVP();
            }
            else {
                ClearItems();
                
            }
            
        }
    }
    
    protected void BindEvents()
    {
        
        using (EventRepository lEventrpt = new EventRepository()) {
            
            ddlEvent.DataValueField = "EventId";
            ddlEvent.DataTextField = "EventTitle";
            ddlEvent.DataSource = lEventrpt.GetActiveEvents();
                
            ddlEvent.DataBind();
            
        }
    }
    
    protected void ClearItems()
    {
        
        ltlEventRSVPId.Text = string.Empty;
        ddlEvent.ClearSelection();
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtJobTitle.Text = string.Empty;
        txtEMail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtNoGuests.Text = string.Empty;
        txtGuestNames.Text = string.Empty;
        
            
        ltlStatus.Text = "Create a New Event RSVP.";
    }
    
    protected void BindEventRSVP()
    {
        
        using (EventRSVPRepository EventRSVPrpt = new EventRSVPRepository()) {
            
            EventRSVP lEventRSVP = EventRSVPrpt.GetEventRSVPById(EventRSVPId);
            
            if ((lEventRSVP != null)) {
                
                ltlEventRSVPId.Text = lEventRSVP.EventRSVPId.ToString();
                
                if ((lEventRSVP.EventInfo != null)) {
                    ddlEvent.SelectedValue = lEventRSVP.EventInfo.EventId.ToString();
                }
                else {
                    ddlEvent.SelectedValue = lEventRSVP.EventInfoReference.EntityKey.EntityKeyValues[0].Value.ToString();
                }
                
                txtFirstName.Text = lEventRSVP.FirstName;
                txtLastName.Text = lEventRSVP.LastName;
                txtJobTitle.Text = lEventRSVP.JobTitle;
                txtEMail.Text = lEventRSVP.EMail;
                txtPhone.Text = lEventRSVP.Phone;
                txtNoGuests.Text = lEventRSVP.NoGuests.ToString();
                txtGuestNames.Text = lEventRSVP.GuestNames;
                rblRSVP.SelectedValue = lEventRSVP.AttendStatus.ToString();
                txtOrganization.Text = lEventRSVP.Organization;

                ltlActive.Text = lEventRSVP.Active.ToString();
                ltlAddedBy.Text = lEventRSVP.AddedBy;
                ltlDateAdded.Text = lEventRSVP.DateAdded.ToShortDateString();
                ltlUpdatedBy.Text = lEventRSVP.UpdatedBy;
                ltlDateUpdated.Text = lEventRSVP.DateUpdated.ToShortDateString();
                
                ltlStatus.Text = "Edit The Event RSVP";
            }
            else {
                EventRSVPId = 0;
                ltlStatus.Text = "Create a New Event RSVP";
                
            }
            
        }
    }
    
    protected void ManageEventRSVPs()
    {
        Response.Redirect("ManageEventRSVPs.aspx");
    }
    
    protected void UpdateEventRSVP()
    {
        
        using (EventRSVPRepository lEventRSVPrpt = new EventRSVPRepository()) {
            
            EventRSVP lEventRSVP = new EventRSVP( );
            
            if (EventRSVPId > 0) {
                lEventRSVP = lEventRSVPrpt.GetEventRSVPById(EventRSVPId);
            }
            else {
                lEventRSVP = new EventRSVP();
            }

            int lEventId = int.Parse(ddlEvent.SelectedValue);

            using (EventRepository levtr = new EventRepository())
            {
                lEventRSVP.EventInfo = levtr.GetEventInfoById(lEventId);
            }

            lEventRSVP.FirstName = txtFirstName.Text;
            lEventRSVP.LastName = txtLastName.Text;
            lEventRSVP.JobTitle = txtJobTitle.Text;
            lEventRSVP.EMail = txtEMail.Text;
            lEventRSVP.Phone = txtPhone.Text;
            lEventRSVP.NoGuests = int.Parse( txtNoGuests.Text);
            lEventRSVP.GuestNames = txtGuestNames.Text;
            lEventRSVP.Organization = txtOrganization.Text;
            lEventRSVP.AttendStatus = int.Parse( rblRSVP.SelectedValue);
            
            lEventRSVP.DateUpdated = DateTime.Now;
            lEventRSVP.UpdatedBy = UserName;
            
            if (lEventRSVP.EventRSVPId > 0) {
                if (lEventRSVPrpt.UpdateEventRSVP(lEventRSVP)) {
                    IndicateUpdated();
                }
                else {
                    IndicateNotUpdated((BaseRepository)lEventRSVPrpt);
                }
            }
            else {
                
                lEventRSVP.Active = true;
                lEventRSVP.AddedBy = UserName;
                lEventRSVP.DateAdded = DateTime.Now;
                if (lEventRSVPrpt.AddEventRSVP(lEventRSVP)) {
                    IndicateUpdated();
                }
                else {
                    IndicateNotUpdated(lEventRSVPrpt);
                }
                
            }
            
        }
    }
    
    protected void IndicateNotUpdated(BaseRepository vRepository)
    {
        
        ltlStatus.Text = string.Empty;
        if (vRepository.ActiveExceptions.Count > 0) {
            foreach (KeyValuePair<string, Exception> kv in vRepository.ActiveExceptions) {
                ltlStatus.Text += ((Exception)kv.Value).Message + "<BR/>";
            }
        }
        else {
            ltlStatus.Text = "The Event RSVP Has Not Been Updated.";
            
        }
    }
    
    protected void IndicateUpdated()
    {
        ltlStatus.Text = "The Event RSVP Has Been Updated.";
        cmdDelete.Visible = true;
    }
    
    protected void GoToEventRSVPList()
    {
        Response.Redirect("ManageEventRSVPs.aspx");
    }
    
    protected void DeleteEventRSVP()
    {
        using (EventRSVPRepository lEventRSVPrpt = new EventRSVPRepository()) {
            lEventRSVPrpt.DeleteEventRSVP(lEventRSVPrpt.GetEventRSVPById(EventRSVPId));
        }
        GoToEventRSVPList();
    }
    
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        DeleteEventRSVP();
    }
    
    protected void cmdUpdate_Click(object sender, System.EventArgs e)
    {
        UpdateEventRSVP();
    }
    
    protected void cmdCancel_Click(object sender, System.EventArgs e)
    {
        GoToEventRSVPList();
    }
    
}