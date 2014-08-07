namespace TheBeerHouse.BLL.Newsletters
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Mail;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
    using System.Web.Profile;
    using System.Web.Security;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// The Entity Repository Class. Contains methods that utilize the Entity Framework to 
    /// interact with the database.
    /// </summary>
    /// <remarks></remarks>
    public class NewslettersRepository : BaseRepository
    {
        private NewsletterEntities _Newsletterctx;
        private bool disposedValue;

        public NewslettersRepository()
        {
            this.disposedValue = false;
            this.ConnectionString = TheBeerHouse.Globals.Settings.DefaultConnectionStringName;
            this.CacheKey = "NewsLetters";
        }

        public NewslettersRepository(string sConnectionString)
        {
            this.disposedValue = false;
            this.ConnectionString = sConnectionString;
            this.CacheKey = "NewsLetters";
        }

        /// <summary>
        /// </summary>
        /// <param name="vNewsletter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Newsletter AddNewsletter(Newsletter vNewsletter)
        {
            Newsletter AddNewsletter;
            try
            {
                if (vNewsletter.EntityState == EntityState.Detached)
                {
                    this.Newsletterctx.AddToNewsletters(vNewsletter);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddNewsletter = (this.Newsletterctx.SaveChanges() > 0) ? vNewsletter : null;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vNewsletter.NewsletterID), ex);
                AddNewsletter = null;
                ProjectData.ClearProjectError();
                return AddNewsletter;
                ProjectData.ClearProjectError();
            }
            return AddNewsletter;
        }

        public Newsletter AddNewsletter(int NewsletterId, string subject, string plainTextBody, string htmlBody, bool bSendNow)
        {
            Newsletter lNewsLetter;
            if (htmlBody.Trim().Length == 0)
            {
                htmlBody = HttpUtility.HtmlEncode(plainTextBody).Replace(" ", "&nbsp;").Replace(@"\t", "&nbsp;&nbsp;&nbsp;").Replace(@"\n", "<br />");
            }
            if (NewsletterId > 0)
            {
                lNewsLetter = this.GetNewsletterById(NewsletterId);
                lNewsLetter.HtmlBody = htmlBody;
                lNewsLetter.PlainTextBody = plainTextBody;
                lNewsLetter.Subject = subject;
                lNewsLetter.UpdatedDate = DateAndTime.Now;
                lNewsLetter.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lNewsLetter = Newsletter.CreateNewsletter(NewsletterId, DateTime.Now, Helpers.CurrentUserName, subject, plainTextBody, htmlBody, DateTime.Now, true);
            }
            if (bSendNow)
            {
                lNewsLetter.DateSent = DateAndTime.Now;
            }
            return this.AddNewsletter(lNewsLetter);
        }

        private bool ChangeDeletedState(Newsletter vNewsletter, bool vState)
        {
            bool ChangeDeletedState;
            vNewsletter.Active = vState;
            vNewsletter.UpdatedDate = DateAndTime.Now;
            vNewsletter.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Newsletterctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vNewsletter.NewsletterID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vNewsletter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteNewsletter(Newsletter vNewsletter)
        {
            return this.ChangeDeletedState(vNewsletter, false);
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!this.disposedValue && disposing) && !Information.IsNothing(this._Newsletterctx))
            {
                this._Newsletterctx.Dispose();
            }
            this.disposedValue = true;
        }

        /// <summary>
        /// </summary>
        /// <param name="NewsletterId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Newsletter GetNewsletterById(int NewsletterId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__32 $VB$Closure_ClosureVariable_32_C = new _Closure$__32();
            $VB$Closure_ClosureVariable_32_C.$VB$Local_NewsletterId = NewsletterId;
            return this.Newsletterctx.Newsletters.Where<Newsletter>(Expression.Lambda<Func<Newsletter, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Newsletter), "lai"), (MethodInfo) methodof(Newsletter.get_NewsletterID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_32_C, typeof(_Closure$__32)), fieldof(_Closure$__32.$VB$Local_NewsletterId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Newsletter>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetNewsletterCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Newsletterctx.Newsletters.Select<Newsletter, Newsletter>(Expression.Lambda<Func<Newsletter, Newsletter>>(VB$t_ref$S0 = Expression.Parameter(typeof(Newsletter), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Newsletter>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Newsletter> GetNewsletters()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Newsletterctx.Newsletters.OrderByDescending<Newsletter, DateTime>(Expression.Lambda<Func<Newsletter, DateTime>>(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Newsletter), "lNewsletter"), (MethodInfo) methodof(Newsletter.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Newsletter>();
        }

        /// <summary>
        /// </summary>
        /// <param name="vToDate"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Newsletter> GetNewsletters(DateTime vToDate)
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            _Closure$__31 $VB$Closure_ClosureVariable_26_C = new _Closure$__31();
            $VB$Closure_ClosureVariable_26_C.$VB$Local_vToDate = vToDate;
            return this.Newsletterctx.Newsletters.Where<Newsletter>(Expression.Lambda<Func<Newsletter, bool>>(Expression.Coalesce(Expression.LessThan(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Newsletter), "lNewsletter"), (MethodInfo) methodof(Newsletter.get_DateSent)), Expression.Convert(Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_26_C, typeof(_Closure$__31)), fieldof(_Closure$__31.$VB$Local_vToDate)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_LessThan)), Expression.Constant(false, typeof(bool))), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Newsletter, DateTime?>(Expression.Lambda<Func<Newsletter, DateTime?>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Newsletter), "lNewsletter"), (MethodInfo) methodof(Newsletter.get_DateSent)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Newsletter>();
        }

        private static bool HasPersonalizationPlaceholders(string text, bool isHtml)
        {
            if (isHtml)
            {
                if (Regex.IsMatch(text, @"&lt;%\s*username\s*%&gt;", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
                if (Regex.IsMatch(text, @"&lt;%\s*email\s*%&gt;", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
                if (Regex.IsMatch(text, @"&lt;%\s*firstname\s*%&gt;", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
                if (Regex.IsMatch(text, @"&lt;%\s*lastname\s*%&gt;", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            else
            {
                if (Regex.IsMatch(text, @"<%\s*username\s*%>", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
                if (Regex.IsMatch(text, @"<%\s*email\s*%>", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
                if (Regex.IsMatch(text, @"<%\s*firstname\s*%>", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
                if (Regex.IsMatch(text, @"<%\s*lastname\s*%>", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private static string ReplacePersonalizationPlaceholders(string text, SubscriberInfo subscriber, bool isHtml)
        {
            if (isHtml)
            {
                text = Regex.Replace(text, @"&lt;%\s*username\s*%&gt;", subscriber.UserName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                text = Regex.Replace(text, @"&lt;%\s*email\s*%&gt;", subscriber.Email, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                string firstName = "reader";
                if (subscriber.FirstName.Length > 0)
                {
                    firstName = subscriber.FirstName;
                }
                text = Regex.Replace(text, @"&lt;%\s*firstname\s*%&gt;", firstName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                text = Regex.Replace(text, @"&lt;%\s*lastname\s*%&gt;", subscriber.LastName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                return text;
            }
            text = Regex.Replace(text, @"<%\s*username\s*%>", subscriber.UserName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<%\s*email\s*%>", subscriber.Email, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string firstName = "reader";
            if (subscriber.FirstName.Length > 0)
            {
                firstName = subscriber.FirstName;
            }
            text = Regex.Replace(text, @"<%\s*firstname\s*%>", firstName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<%\s*lastname\s*%>", subscriber.LastName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return text;
        }

        public static void SendEmails(object data)
        {
            IEnumerator VB$t_ref$L0;
            object[] parameters = (object[]) data;
            string subject = Conversions.ToString(parameters[0]);
            string plainTextBody = Conversions.ToString(parameters[1]);
            string htmlBody = Conversions.ToString(parameters[2]);
            HttpContext context = (HttpContext) parameters[3];
            Newsletter.Lock.AcquireWriterLock(-1);
            Newsletter.TotalMails = 0;
            Newsletter.Lock.ReleaseWriterLock();
            bool plainTextIsPersonalized = HasPersonalizationPlaceholders(plainTextBody, false);
            bool htmlIsPersonalized = HasPersonalizationPlaceholders(htmlBody, true);
            List<SubscriberInfo> subscribers = new List<SubscriberInfo>();
            ProfileBase profile = context.Profile;
            try
            {
                VB$t_ref$L0 = Membership.GetAllUsers().GetEnumerator();
                while (VB$t_ref$L0.MoveNext())
                {
                    MembershipUser user = (MembershipUser) VB$t_ref$L0.Current;
                    ProfileBase userProfile = Helpers.GetUserProfile(user.UserName, false);
                    if (Helpers.GetSubscriptionType(userProfile) == SubscriptionType.None)
                    {
                        SubscriberInfo subscriber = new SubscriberInfo(user.UserName, user.Email, Helpers.GetProfileFirstName(userProfile), Helpers.GetProfileLastName(userProfile), Helpers.GetSubscriptionType(userProfile));
                        subscribers.Add(subscriber);
                        Newsletter.Lock.AcquireWriterLock(-1);
                        Newsletter.TotalMails++;
                        Newsletter.Lock.ReleaseWriterLock();
                    }
                }
            }
            finally
            {
                if (VB$t_ref$L0 is IDisposable)
                {
                    (VB$t_ref$L0 as IDisposable).Dispose();
                }
            }
            SmtpClient smtpClient = new SmtpClient();
            foreach (SubscriberInfo subscriber in subscribers)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Helpers.Settings.Newsletters.FromEmail, Helpers.Settings.Newsletters.FromDisplayName);
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
                    Thread.Sleep(0x1194);
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
                Newsletter.Lock.AcquireWriterLock(-1);
                Newsletter.SentMails++;
                Newsletter.Lock.ReleaseWriterLock();
            }
            Newsletter.Lock.AcquireWriterLock(-1);
            Newsletter.IsSending = false;
            Newsletter.Lock.ReleaseWriterLock();
        }

        public static int SendNewsletter(Newsletter vNewsLetter)
        {
            int SendNewsletter;
            Newsletter.Lock.AcquireWriterLock(-1);
            Newsletter.TotalMails = -1;
            Newsletter.SentMails = 0;
            Newsletter.IsSending = true;
            Newsletter.Lock.ReleaseWriterLock();
            if (!Information.IsNothing(vNewsLetter))
            {
                object[] parameters = new object[] { vNewsLetter.Subject, vNewsLetter.PlainTextBody, vNewsLetter.HtmlBody, HttpContext.Current };
                ParameterizedThreadStart pts = new ParameterizedThreadStart(NewslettersRepository.SendEmails);
                Thread thread = new Thread(pts);
                thread.Name = "SendEmails";
                thread.Priority = ThreadPriority.BelowNormal;
                thread.Start(parameters);
            }
            return SendNewsletter;
        }

        public bool UnDeleteNewsletter(Newsletter vNewsletter)
        {
            return this.ChangeDeletedState(vNewsletter, true);
        }

        public NewsletterEntities Newsletterctx
        {
            get
            {
                if (Information.IsNothing(this._Newsletterctx))
                {
                    this._Newsletterctx = new NewsletterEntities(this.GetActualConnectionString());
                }
                return this._Newsletterctx;
            }
            set
            {
                this._Newsletterctx = value;
            }
        }

        [CompilerGenerated]
        internal class _Closure$__31
        {
            public DateTime $VB$Local_vToDate;

            [DebuggerNonUserCode]
            public _Closure$__31()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__31(NewslettersRepository._Closure$__31 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vToDate = other.$VB$Local_vToDate;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__32
        {
            public int $VB$Local_NewsletterId;

            [DebuggerNonUserCode]
            public _Closure$__32()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__32(NewslettersRepository._Closure$__32 other)
            {
                if (other != null)
                {
                    this.$VB$Local_NewsletterId = other.$VB$Local_NewsletterId;
                }
            }
        }
    }
}

