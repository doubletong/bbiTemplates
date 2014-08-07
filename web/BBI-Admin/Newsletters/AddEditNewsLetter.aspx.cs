using System.Threading;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BBICMS.Newsletters;
using BBICMS.UI;

partial class Admin_AddEditNewsLetter : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!this.IsPostBack) {
            
            bool isSending = false;
            Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
            isSending = Newsletter.IsSending;
            Newsletter.Lock.ReleaseReaderLock();
            
            if (NewsLetterId > 0) {
                BindNewsletter();
            }
            else if (isSending) {
                ShowSending();
            }
            else {
                ClearInfo();
                
            }
        }
        txtHtmlBody.BasePath = this.BaseUrl + "FCKeditor/";
    }
    
    protected void ClearInfo()
    {
        
        ltlSubject.Visible = false;
        ltlPlainTextBody.Visible = false;
        ltlHtmlBody.Visible = false;
        
        txtSubject.Text = string.Empty;
        txtPlainTextBody.Text = string.Empty;
            
        txtHtmlBody.Value = string.Empty;

        ltlTitle.Text = ltlTitle1.Text = "写邮件";
    }
    
    protected void BindNewsletter()
    {
        ltlTitle.Text = ltlTitle1.Text = "查看邮件";
        using (NewslettersRepository lNewsletterrpt = new NewslettersRepository()) {
            
            Newsletter lNewsletter = lNewsletterrpt.GetNewsletterById(NewsLetterId);
            
            if ((lNewsletter != null)) {
                
                if ((lNewsletter.DateSent != null)) {
                    
                    ltlSubject.Text = lNewsletter.Subject;
                    ltlPlainTextBody.Text = string.Format("<div id=\"NewsletterBox\">{0}</div>", lNewsletter.PlainTextBody.Replace(@"\r\n", "<br/>"));
                    ltlHtmlBody.Text = string.Format("<div id=\"NewsletterBox\">{0}</div>", lNewsletter.HtmlBody);
                    ltlDateSent.Text = string.Format("<P>Sent on {0:d}</P>", lNewsletter.DateSent);
                    
                    txtSubject.Visible = false;
                    txtPlainTextBody.Visible = false;
                    txtHtmlBody.Visible = false;
                    btnSend.Visible = false;
                    btnSave.Visible = false;
                        
                    ltlAddInstruction.Visible = false;
                }
                else {
                    
                    ltlSubject.Visible = false;
                    ltlPlainTextBody.Visible = false;
                    ltlHtmlBody.Visible = false;
                    ltlDateSent.Visible = false;
                    
                    txtSubject.Text = lNewsletter.Subject;
                    txtPlainTextBody.Text = lNewsletter.PlainTextBody.Replace(@"\r\n", "<br/>");
                        
                    txtHtmlBody.Value = lNewsletter.HtmlBody;
                    
                }
                
            }
            
        }
    }
    
    protected void ShowSending()
    {
        panWait.Visible = true;
        panSend.Visible = false;
        Admin_Admin mp = (Admin_Admin)Master;
            
        mp.pageBodyTag.Attributes.Add("onload", "UpdateStatus();");
    }
    
    protected void btnSend_Click(object sender, System.EventArgs e)
    {
        SaveNewsletter(true);
    }
    
    protected void SaveNewsletter(bool bSendNow)
    {
        
        bool isSending = false;
        
        if (bSendNow) {
            
            Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
            isSending = Newsletter.IsSending;
            Newsletter.Lock.ReleaseReaderLock();
            
            if (isSending) {
                bSendNow = false;                
                panWait.Visible = true;                    
                panSend.Visible = false;
                
            }
        }
        
        using (NewslettersRepository lNewsletterrpt = new NewslettersRepository()) {
            
            Newsletter lNewsletter = lNewsletterrpt.AddNewsletter(NewsLetterId, txtSubject.Text, txtPlainTextBody.Text, txtHtmlBody.Value, bSendNow);
            
            if (bSendNow & !isSending) {
                panWait.Visible = true;
                panSend.Visible = false;
                
                NewslettersRepository.SendNewsletter(lNewsletter);
                ShowSending();
                
            }
            
        }
    }
    
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        SaveNewsletter(false);
    }
    
}