using System;
using System.Web;
using BBICMS.Newsletters;
using BBICMS;
using Web.UI.Newsletter;

partial class ShowNewsletter : NewsLetterPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        // check whether this page can be accessed by anonymous users. If not, and if the
        // current user is not authenticated, redirect to the login page
        if (!this.User.Identity.IsAuthenticated && !Helpers.Settings.Newsletters.ArchiveIsPublic) {
            this.RequestLogin();
        }
        
        BindNewsletter();
    }
    
    protected void BindNewsletter()
    {
        using (NewslettersRepository lNewsLetterrpt = new NewslettersRepository()) {
            
            Newsletter lNewsLetter = lNewsLetterrpt.GetNewsletterById(NewsLetterId);
            
            // check that the newsletter can be viewed, according to the number of days
            // that must pass before it is published in the archive
            int days = DateTime.Now.Subtract( lNewsLetter.AddedDate).Days;
            if (Helpers.Settings.Newsletters.HideFromArchiveInterval > days && (!this.User.Identity.IsAuthenticated || (!this.User.IsInRole("Administrators") & !this.User.IsInRole("Editors")))) {
                
                this.RequestLogin();
            }
            
            // show the newsletter's data
            this.Title += lNewsLetter.Subject;
            lblSubject.Text = lNewsLetter.Subject;
            lblPlaintextBody.Text = HttpUtility.HtmlEncode(lNewsLetter.PlainTextBody).Replace(" ", "&nbsp; ").Replace("\\t", "&nbsp;&nbsp;&nbsp;").Replace("\\n", "<br/>");
                
            lblHtmlBody.Text = lNewsLetter.HtmlBody;
        }
    }
    
}