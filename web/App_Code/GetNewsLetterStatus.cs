using System.Threading;
using System.Web.Script.Services;
using System.Web.Services;
using BBICMS.Newsletters;

/// <summary>
/// Summary description for GetNewsLetterStatus
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class NewsLetterStatus : System.Web.Services.WebService
{

    [WebMethod]
    [ScriptMethod]
    public NewsletterStatus GetNewsLetterStatus()
    {
        NewsletterStatus lNewsletterStatus = new NewsletterStatus();

        Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);

        lNewsletterStatus.SentMails = Newsletter.SentMails;
        lNewsletterStatus.TotalMails = Newsletter.TotalMails;
        lNewsletterStatus.IsSending = Newsletter.IsSending;

        Newsletter.Lock.ReleaseReaderLock();

        return lNewsletterStatus;

    }
}

