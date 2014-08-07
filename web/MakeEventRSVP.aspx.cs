using System;
using System.Collections.Generic;
using System.Web.Profile;
using BBICMS.Events;
using BBICMS;
using BBICMS.BLL;
using System.Linq;

public partial class MakeEventRSVP : EventPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            
            if (EventId == 0) {
                Response.Redirect("default.aspx");
            }
            
            mvRegistration.ActiveViewIndex = 0;
            
            if (EventRSVPId > 0) {
                BindEventRSVP();
            }
            else {
                ClearItems();
            }
            
                
            BindAuthenticatedUser();
            
        }
    }
    
    protected void BindAuthenticatedUser()
    {
        if (Context.User.Identity.IsAuthenticated) {
            
            ProfileBase lProfile = Helpers.GetUserProfile();
            
            if ((lProfile != null)) {
                
                txtFirstName.Text = lProfile.GetPropertyValue("FirstName").ToString();
                txtLastName.Text = lProfile.GetPropertyValue("LastName").ToString();


                txtPhone.Text = lProfile.GetProfileGroup("Contacts").GetPropertyValue("Phone").ToString();
                
            }
        }
    }
    
    protected void ClearItems()
    {
        
        using (EventRepository lEventrpt = new EventRepository()) {
            
            EventInfo lEventInfo = lEventrpt.GetEventInfoById(EventId);
                
            txtEventId.Text = lEventInfo.EventTitle;
        }
        
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtJobTitle.Text = string.Empty;
        txtEMail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtNoGuests.Text = string.Empty;
        txtGuestNames.Text = string.Empty;
        
            
        ltlStatus.Text = "Please Fill Out the Following Information";
    }
    
    protected void BindEventRSVP()
    {
        
        using (EventRSVPRepository lEventRSVPrpt = new EventRSVPRepository()) {
            
            EventRSVP lEventRSVP = lEventRSVPrpt.GetEventRSVPById(EventRSVPId);
            
            if ((lEventRSVP != null)) {
                
                txtEventId.Text = lEventRSVP.EventInfo.EventTitle;
                txtFirstName.Text = lEventRSVP.FirstName;
                txtLastName.Text = lEventRSVP.LastName;
                txtJobTitle.Text = lEventRSVP.JobTitle;
                txtEMail.Text = lEventRSVP.EMail;
                txtPhone.Text = lEventRSVP.Phone;
                txtNoGuests.Text = lEventRSVP.NoGuests.ToString();
                txtGuestNames.Text = lEventRSVP.GuestNames;

                rblRSVP.SelectedValue = lEventRSVP.AttendStatus.ToString();
                txtOrganization.Text = lEventRSVP.Organization;
                
                
                ltlStatus.Text = "Edit The Event Registration";
            }
            else {
                EventRSVPId = 0;
                ltlStatus.Text = "Create a New Event Registration";
                
            }
            
        }
    }
    
    protected void ManageEventRSVPs()
    {
        Response.Redirect("default.aspx");
    }
    
    protected void UpdateEventRSVP()
    {
        
        using (EventRSVPRepository lEventRSVPrpt = new EventRSVPRepository()) {
            
            EventRSVP lEventRSVP = new EventRSVP();
            
            if (EventRSVPId > 0) {
                lEventRSVP = lEventRSVPrpt.GetEventRSVPById(EventRSVPId);
            }
            else {
                lEventRSVP = new EventRSVP();
            }
            
            lEventRSVP.FirstName = txtFirstName.Text;
            lEventRSVP.LastName = txtLastName.Text;
            lEventRSVP.JobTitle = txtJobTitle.Text;
            lEventRSVP.EMail = txtEMail.Text;
            lEventRSVP.Phone = txtPhone.Text;
            lEventRSVP.NoGuests = int.Parse( txtNoGuests.Text);
            lEventRSVP.GuestNames = txtGuestNames.Text;
            
            lEventRSVP.AttendStatus = int.Parse( rblRSVP.SelectedValue);
            lEventRSVP.Organization = txtOrganization.Text;

            using (EventRepository evtr = new EventRepository())
            {
                lEventRSVP.EventInfo = evtr.GetEventInfoById(EventId);
                    //(from lai in lEventRSVPrpt.Eventctx.EventInfos
                    //                    where lai.EventId == EventId
                    //                    select lai).FirstOrDefault();
                
            }
            
            lEventRSVP.DateUpdated = DateTime.Now;
            lEventRSVP.UpdatedBy = UserName;
            
            if (lEventRSVP.EventRSVPId > 0) {
                if (lEventRSVPrpt.UpdateEventRSVP(lEventRSVP)) {
                    IndicateUpdated(lEventRSVP);
                }
                else {
                    IndicateNotUpdated(lEventRSVPrpt);
                }
            }
            else {
                
                lEventRSVP.Active = true;
                lEventRSVP.AddedBy = UserName;
                lEventRSVP.DateAdded = DateTime.Now;
                if (lEventRSVPrpt.AddEventRSVP(lEventRSVP)) {
                    IndicateUpdated(lEventRSVP);
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
            ltlStatus.Text = "Sorry, there was a problem with your registration request.";
            
        }
    }
    
    protected void IndicateUpdated(EventRSVP lEventRSVP)
    {
        ltlStatus.Text = "Your RSVP has been recieved by The Beer House. Thank You!";
        cmdDelete.Visible = false;
        cmdCancel.Visible = false;
        cmdUpdate.Visible = false;
        
        mvRegistration.ActiveViewIndex = 1;
        BindConfirmation(lEventRSVP);
    }
    
    
    protected void BindConfirmation(EventRSVP lEventRSVP)
    {
        
        using (EventRepository lEventrpt = new EventRepository()) {
            
            EventInfo lEventInfo = lEventrpt.GetEventInfoById(EventId);
            
            if ((lEventInfo != null)) {
                
                EvTitle.Text = lEventInfo.EventTitle;
                EvDate.Text = lEventInfo.EventDate.ToShortDateString() ;
                EvTime.Text = lEventInfo.EventTime;
                EvLoc.Text = lEventInfo.EventLocation;
                // lblPriority.Text = ei.Importance
                EventDesc.Text = lEventInfo.EventDesc;
                
                lblAddress.Text = lEventInfo.Address;
                lblCity.Text = lEventInfo.City;
                lblState.Text = lEventInfo.State;
                lblZipCode.Text = lEventInfo.ZipCode;
                
            }
        }
        
        
        if ((lEventRSVP != null)) {
            
            ltlFirstName.Text = lEventRSVP.FirstName;
            ltlLastname.Text = lEventRSVP.LastName;
            ltlJobTitle.Text = lEventRSVP.JobTitle;
            ltlRSVP.Text = lEventRSVP.AttendStatus > 0 ? "Yes" : "No";
            ltlOrganization.Text = lEventRSVP.Organization;
            
            ltlEmail.Text = lEventRSVP.EMail;
            ltlPhone.Text = lEventRSVP.Phone;
            ltlNoGuests.Text = lEventRSVP.NoGuests.ToString();
                
            ltlGuestsNames.Text = lEventRSVP.GuestNames.Replace("\n", "<BR/>");
            
        }
    }
    
    protected void GoToEventRSVPList()
    {
        Response.Redirect("Default.aspx");
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