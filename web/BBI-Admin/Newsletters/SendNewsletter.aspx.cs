using System.Threading;
using BBICMS.Newsletters;
using BBICMS.UI;

partial class Admin_SendNewsletter : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        bool isSending = false;
        Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
        isSending = Newsletter.IsSending;
        Newsletter.Lock.ReleaseReaderLock();
        
        if (!this.IsPostBack && isSending) {
            panWait.Visible = true;
            panSend.Visible = false;
            Admin_Admin mp = (Admin_Admin)Master;
                
            mp.pageBodyTag.Attributes.Add("onload", "UpdateStatus();");
        }
        txtHtmlBody.BasePath = this.BaseUrl + "FCKeditor/";
    }
    
    protected void btnSend_Click(object sender, System.EventArgs e)
    {
        bool isSending = false;
        Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
        isSending = Newsletter.IsSending;
        Newsletter.Lock.ReleaseReaderLock();
        
        // if another newsletter is currently being sent, show the panel with the wait message,
        // but don't hide the input controls so that the user doesn't loose the newsletter's text
        if (isSending) {
            panWait.Visible = true;
        }
        
    }
    
}