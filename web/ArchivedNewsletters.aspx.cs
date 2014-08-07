using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.Newsletters;
using BBICMS;
using Web.UI.Newsletter;

public partial class ArchivedNewsletters : NewsLetterPage
{
    
    protected void  Page_Load(object sender, System.EventArgs e)
    {
        
        // check whether this page can be accessed by anonymous users. If not, and if the
        // current user is not authenticated, redirect to the login page
        if (!this.User.Identity.IsAuthenticated && !Helpers.Settings.Newsletters.ArchiveIsPublic) {
            this.RequestLogin();
        }
        
        if (!IsPostBack) {
            BindArchivedNewsLetters();
            
        }
    }
    
    protected void BindArchivedNewsLetters()
    {
        
        using (NewslettersRepository lNewsLetterrpt = new NewslettersRepository()) {

            DateTime toDate = CanEdit ? DateTime.Now : DateTime.Now.Subtract(new TimeSpan(Helpers.Settings.Newsletters.HideFromArchiveInterval, 0, 0, 0));
            
            List<Newsletter> lNewsLetters = lNewsLetterrpt.GetNewsletters(toDate);
            lvNewsLetters.DataSource = lNewsLetters;
            lvNewsLetters.DataBind();
            
            DataPager pagerBottom = (DataPager)lvNewsLetters.FindControl("pagerBottom");
            
            if ((pagerBottom != null)) {
                SetupListViewPager(lNewsLetters.Count, pagerBottom);
                
            }
            
        }
    }
    
    protected void  lvNewsLetters_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindArchivedNewsLetters();
    }
    
    
}