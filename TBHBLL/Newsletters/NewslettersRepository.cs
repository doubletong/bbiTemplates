using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.ApplicationServices;
using BBICMS;
using BBICMS.BLL;

namespace BBICMS.Newsletters
{
    public class NewslettersRepository : BaseRepository
    {
        #region " BLL/DAL Methods "

        private NewsletterEntities _Newsletterctx;

        public NewsletterEntities Newsletterctx
        {
            get
            {
                if ((_Newsletterctx == null))
                {
                    _Newsletterctx = new NewsletterEntities(GetActualConnectionString());
                }

                return _Newsletterctx;
            }
            set { _Newsletterctx = value; }
        }

        #region " Dispose "

        private bool disposedValue = false;
        // To detect redundant calls

        // IDisposable
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if ((_Newsletterctx == null) == false)
                    {
                        _Newsletterctx.Dispose();
                    }
                }
            }
            disposedValue = true;
        }

        #region " IDisposable Support "

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

        public List<Newsletter> GetNewsletters()
        {
            return (from lNewsletter in Newsletterctx.Newsletters
                    orderby lNewsletter.AddedDate descending
                    select lNewsletter).ToList();
        }

        public List<Newsletter> GetNewsletters(DateTime vToDate)
        {
            return (from lNewsletter in Newsletterctx.Newsletters
                    where lNewsletter.DateSent < vToDate
                    orderby lNewsletter.DateSent descending
                    select lNewsletter).ToList();
        }

        public Newsletter GetNewsletterById(int NewsletterId)
        {
            return (from lNewsletter in Newsletterctx.Newsletters
                    where lNewsletter.NewsletterID == NewsletterId
                    select lNewsletter).FirstOrDefault();
        }

        public int GetNewsletterCount()
        {
            return (from lNewsletter in Newsletterctx.Newsletters
                    select lNewsletter).Count();
        }

        public Newsletter AddNewsletter(int NewsletterId, string subject, string plainTextBody, string htmlBody,
                                        bool bSendNow)
        {
            Newsletter lNewsLetter = default(Newsletter);

            // if the HTML body is an empty string, use the plain-text body
            // converted to HTML
            if (htmlBody.Trim().Length == 0)
            {
                htmlBody =
                    HttpUtility.HtmlEncode(plainTextBody).Replace(" ", "&nbsp;").Replace("\\t", "&nbsp;&nbsp;&nbsp;").
                        Replace("\\n", "<br />");
            }

            //Remember this is a shared (static for those in the C# world) method, so to access the non-shared members requires a new object

            if (NewsletterId > 0)
            {
                lNewsLetter = GetNewsletterById(NewsletterId);

                lNewsLetter.HtmlBody = htmlBody;
                lNewsLetter.PlainTextBody = plainTextBody;
                lNewsLetter.Subject = subject;
                lNewsLetter.UpdatedDate = DateTime.Now;

                lNewsLetter.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lNewsLetter = Newsletter.CreateNewsletter(NewsletterId, DateTime.Now, Helpers.CurrentUserName, subject,
                                                          plainTextBody, htmlBody, DateTime.Now, true);
            }

            if (bSendNow)
            {
                lNewsLetter.DateSent = DateTime.Now;
            }

            lNewsLetter = AddNewsletter(lNewsLetter);


            return lNewsLetter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vNewsletter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Newsletter AddNewsletter(Newsletter vNewsletter)
        {
            try
            {
                if (vNewsletter.EntityState == EntityState.Detached)
                {
                    Newsletterctx.AddToNewsletters(vNewsletter);
                }
                base.PurgeCacheItems(CacheKey);

                return Newsletterctx.SaveChanges() > 0 ? vNewsletter : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vNewsletter.NewsletterID, ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vNewsletter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteNewsletter(Newsletter vNewsletter)
        {
            return ChangeDeletedState(vNewsletter, false);
        }

        public bool UnDeleteNewsletter(Newsletter vNewsletter)
        {
            return ChangeDeletedState(vNewsletter, true);
        }

        private bool ChangeDeletedState(Newsletter vNewsletter, bool vState)
        {
            vNewsletter.Active = vState;
            vNewsletter.UpdatedDate = DateTime.Now;
            vNewsletter.UpdatedBy = CurrentUserName;

            try
            {
                Newsletterctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vNewsletter.NewsletterID.ToString(), ex);

                return false;
            }
        }

        #region " Constructors "

        public NewslettersRepository(string sConnectionString)
        {
            ConnectionString = sConnectionString;
            CacheKey = "NewsLetters";
        }

        public NewslettersRepository()
        {
            ConnectionString = Globals.Settings.DefaultConnectionStringName;
            CacheKey = "NewsLetters";
        }

        #endregion

        #endregion

        // Sends a newsletter
        public static int SendNewsletter(Newsletter vNewsLetter)
        {
            Newsletter.Lock.AcquireWriterLock(Timeout.Infinite);
            Newsletter.TotalMails = -1;
            Newsletter.SentMails = 0;
            Newsletter.IsSending = true;
            Newsletter.Lock.ReleaseWriterLock();

            if (vNewsLetter != null)
            {
                // send the newsletters asyncronously
                object[] parameters = {
                                          vNewsLetter.Subject, vNewsLetter.PlainTextBody, vNewsLetter.HtmlBody,
                                          HttpContext.Current
                                      };
                var pts = new ParameterizedThreadStart(SendEmails);
                var thread = new Thread(pts);
                thread.Name = "SendEmails";
                thread.Priority = ThreadPriority.BelowNormal;

                thread.Start(parameters);
            }

            return 0;
        }

        // Sends the newsletter e-mails to all subscribers
        public static void SendEmails(object data)
        {
            var parameters = (object[]) data;
            var subject = (string) parameters[0];
            var plainTextBody = (string) parameters[1];
            var htmlBody = (string) parameters[2];
            var context = (HttpContext) parameters[3];

            Newsletter.Lock.AcquireWriterLock(Timeout.Infinite);
            Newsletter.TotalMails = 0;
            Newsletter.Lock.ReleaseWriterLock();

            // check if the plain-text and HTML bodies have personalization placeholders
            // the will need to be replaced on a per-mail basis. If not, the parsing will
            // be completely avoided later.
            bool plainTextIsPersonalized = HasPersonalizationPlaceholders(plainTextBody, false);
            bool htmlIsPersonalized = HasPersonalizationPlaceholders(htmlBody, true);

            // retreive all subscribers to the plain-text and HTML newsletter
            var subscribers = new List<SubscriberInfo>();

            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                ProfileBase userProfile = Helpers.GetUserProfile(user.UserName, false);
                if (!(Helpers.GetSubscriptionType(userProfile) != SubscriptionType.None))
                {
                    var subscriber = new SubscriberInfo(user.UserName, user.Email,
                                                        Helpers.GetProfileRealName(userProfile),                                                   
                                                        Helpers.GetSubscriptionType(userProfile));
                    subscribers.Add(subscriber);
                    Newsletter.Lock.AcquireWriterLock(Timeout.Infinite);
                    Newsletter.TotalMails += 1;

                    Newsletter.Lock.ReleaseWriterLock();
                }
            }

            // send the newsletter
            var smtpClient = new SmtpClient();

            foreach (SubscriberInfo subscriber in subscribers)
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(Helpers.Settings.Newsletters.FromEmail,
                                            Helpers.Settings.Newsletters.FromDisplayName);
                mail.To.Add(subscriber.Email);
                mail.Subject = subject;
                if (subscriber.SubscriptionType == SubscriptionType.PlainText)
                {
                    string body = plainTextBody;
                    if (plainTextIsPersonalized)
                    {
                        body = ReplacePersonalizationPlaceholders(body, subscriber, false);
                    }
                    mail.Body = body;
                    mail.IsBodyHtml = false;
                }
                else
                {
                    string body = htmlBody;
                    if (htmlIsPersonalized)
                    {
                        body = ReplacePersonalizationPlaceholders(body, subscriber, true);
                    }
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                }
                try
                {
                    //OK Lee before you ding for the code not being the same here, this is to test 
                    //the user interface stuff without sending gobs of junky e-mail. Sending
                    //e-mails is not needed to test the progress bar. :) I know the e-mail stuff
                    //works.
                    //smtpClient.Send(mail)
                    Thread.Sleep(4500);
                }
                catch
                {
                }

                Newsletter.Lock.AcquireWriterLock(Timeout.Infinite);
                Newsletter.SentMails += 1;

                Newsletter.Lock.ReleaseWriterLock();
            }

            Newsletter.Lock.AcquireWriterLock(Timeout.Infinite);
            Newsletter.IsSending = false;

            Newsletter.Lock.ReleaseWriterLock();
        }

        // Returns whether the input text contains personalization placeholders
        private static bool HasPersonalizationPlaceholders(string text, bool isHtml)
        {
            if (isHtml)
            {
                if (Regex.IsMatch(text, "&lt;%\\s*username\\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                {
                    return true;
                }
                if (Regex.IsMatch(text, "&lt;%\\s*email\\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                {
                    return true;
                }
                if (Regex.IsMatch(text, "&lt;%\\s*firstname\\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                {
                    return true;
                }
                if (Regex.IsMatch(text, "&lt;%\\s*lastname\\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                {
                    return true;
                }
            }
            else
            {
                if (Regex.IsMatch(text, "<%\\s*username\\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                {
                    return true;
                }
                if (Regex.IsMatch(text, "<%\\s*email\\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                {
                    return true;
                }
                if (Regex.IsMatch(text, "<%\\s*firstname\\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                {
                    return true;
                }
                if (Regex.IsMatch(text, "<%\\s*lastname\\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                {
                    return true;
                }
            }
            return false;
        }

        // Replaces the input text's personalization placeholders
        private static string ReplacePersonalizationPlaceholders(string text, SubscriberInfo subscriber, bool isHtml)
        {
            if (isHtml)
            {
                text = Regex.Replace(text, "&lt;%\\s*username\\s*%&gt;", subscriber.UserName,
                                     RegexOptions.IgnoreCase | RegexOptions.Compiled);
                text = Regex.Replace(text, "&lt;%\\s*email\\s*%&gt;", subscriber.Email,
                                     RegexOptions.IgnoreCase | RegexOptions.Compiled);
                string fullName = "reader";
                if (subscriber.FullName.Length > 0)
                {
                   fullName = subscriber.FullName;
                }
                text = Regex.Replace(text, "&lt;%\\s*fullname\\s*%&gt;", fullName,
                                     RegexOptions.IgnoreCase | RegexOptions.Compiled);
               
            }
            else
            {
                text = Regex.Replace(text, "<%\\s*username\\s*%>", subscriber.UserName,
                                     RegexOptions.IgnoreCase | RegexOptions.Compiled);
                text = Regex.Replace(text, "<%\\s*email\\s*%>", subscriber.Email,
                                     RegexOptions.IgnoreCase | RegexOptions.Compiled);
                string fullName = "reader";
                if (subscriber.FullName.Length > 0)
                {
                    fullName = subscriber.FullName;
                }
                text = Regex.Replace(text, "<%\\s*fullname\\s*%>", fullName,
                                     RegexOptions.IgnoreCase | RegexOptions.Compiled);
               
            }
            return text;
        }
    }
}