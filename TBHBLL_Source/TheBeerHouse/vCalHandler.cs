namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using TheBeerHouse.BLL.EventCalendar;

    public class vCalHandler : IHttpHandler
    {
        private HttpContext _context;
        private HttpRequest _Request;
        private HttpResponse _Response;

        private void BuildVCal()
        {
            using (EventRepository lEventrpt = new EventRepository())
            {
                int lEventId = Conversions.ToInteger(this.Request.QueryString["EventId"]);
                if (lEventId <= 0)
                {
                    this.SendNoEventMessage();
                }
                EventInfo lEventInfo = lEventrpt.GetEventInfoById(lEventId);
                if (!Information.IsNothing(lEventInfo))
                {
                    IEnumerator VB$t_ref$L0;
                    IEnumerator VB$t_ref$L1;
                    vCalendar lvCal = new vCalendar();
                    vCalendar.vEvent lEvent = new vCalendar.vEvent();
                    lEvent.Description = lEventInfo.EventTitle;
                    lEvent.Location = lEventInfo.EventLocation;
                    lEvent.URL = string.Format("{0}?eventid={1}", Path.Combine(Helpers.WebRoot, "ShowEvent.aspx"), lEventId);
                    lEvent.Summary = lEventInfo.EventDesc;
                    Regex rgTime = new Regex(@"^(\d{2}):(\d{2}):(\d{2})\ (PM|AM)$");
                    int mHour = 0;
                    int mMinute = 0;
                    try
                    {
                        VB$t_ref$L0 = rgTime.Matches(lEventInfo.EventTime).GetEnumerator();
                        while (VB$t_ref$L0.MoveNext())
                        {
                            Match m = (Match) VB$t_ref$L0.Current;
                            if (m.Groups.Count > 3)
                            {
                                if (m.Groups[4].Value == "PM")
                                {
                                    mHour = (int) Math.Round((double) (12.0 + Conversions.ToDouble(m.Groups[1].Value)));
                                }
                                else
                                {
                                    mHour = Conversions.ToInteger(m.Groups[1].Value);
                                }
                                mMinute = Conversions.ToInteger(m.Groups[2].Value);
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
                    lEvent.DTStart = new DateTime(lEventInfo.EventDate.Year, lEventInfo.EventDate.Month, lEventInfo.EventDate.Day, mHour, mMinute, 0);
                    mHour = 0;
                    mMinute = 0;
                    try
                    {
                        VB$t_ref$L1 = rgTime.Matches(lEventInfo.EndTime).GetEnumerator();
                        while (VB$t_ref$L1.MoveNext())
                        {
                            Match m = (Match) VB$t_ref$L1.Current;
                            if (m.Groups.Count > 3)
                            {
                                if (m.Groups[4].Value == "PM")
                                {
                                    mHour = (int) Math.Round((double) (12.0 + Conversions.ToDouble(m.Groups[1].Value)));
                                }
                                else
                                {
                                    mHour = Conversions.ToInteger(m.Groups[1].Value);
                                }
                                mMinute = Conversions.ToInteger(m.Groups[2].Value);
                            }
                        }
                    }
                    finally
                    {
                        if (VB$t_ref$L1 is IDisposable)
                        {
                            (VB$t_ref$L1 as IDisposable).Dispose();
                        }
                    }
                    DateTime? VB$LW$t_struct$S0 = lEventInfo.EventEndDate;
                    DateTime dtEnd = VB$LW$t_struct$S0.HasValue ? VB$LW$t_struct$S0.GetValueOrDefault() : lEventInfo.EventDate;
                    if (mHour > 0)
                    {
                        lEvent.DTEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, mHour, mMinute, 0);
                    }
                    lvCal.Events.Add(lEvent);
                    this.CurrentContext.Response.ContentType = "text/calendar";
                    this.CurrentContext.Response.ContentEncoding = Encoding.UTF8;
                    this.CurrentContext.Response.Write(lvCal.ToString());
                    this.CurrentContext.Response.Flush();
                    this.CurrentContext.Response.End();
                }
                else
                {
                    this.SendNoEventMessage();
                }
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (Information.IsNothing(context))
            {
                throw new ArgumentNullException("There is no httpContext Associated with the Request.");
            }
            if (Information.IsNothing(context.Response))
            {
                throw new ArgumentNullException("There is no httpContext.Response Associated with the Request.");
            }
            this.CurrentContext = context;
            this.Response = context.Response;
            this.Request = context.Request;
            context.Response.ClearContent();
            if (!Information.IsNothing(this.Request.QueryString["EventId"]))
            {
                this.BuildVCal();
            }
            else
            {
                this.SendNoEventMessage();
            }
        }

        private void SendNoEventMessage()
        {
            this.CurrentContext.Response.ContentType = "text/HTML";
            this.CurrentContext.Response.Write("<P>Sorry, Please supply a valid EventId.</P>");
            this.CurrentContext.Response.Flush();
            this.CurrentContext.Response.End();
        }

        public HttpContext CurrentContext
        {
            get
            {
                return this._context;
            }
            set
            {
                this._context = value;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public HttpRequest Request
        {
            get
            {
                return this._Request;
            }
            set
            {
                this._Request = value;
            }
        }

        public HttpResponse Response
        {
            get
            {
                return this._Response;
            }
            set
            {
                this._Response = value;
            }
        }

        public bool System.Web.IHttpHandler.IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

