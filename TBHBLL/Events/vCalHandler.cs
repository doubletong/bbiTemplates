using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using BBICMS.Events;
using BBICMS;

public class vCalHandler : IHttpHandler
{

    #region " Properties "

    private HttpContext _context;
    public HttpContext CurrentContext
    {
        get { return _context; }
        set { _context = value; }
    }

    private HttpResponse _Response;
    public HttpResponse Response
    {
        get { return _Response; }
        set { _Response = value; }
    }

    private HttpRequest _Request;
    public HttpRequest Request
    {
        get { return _Request; }
        set { _Request = value; }
    }


    #endregion


    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(System.Web.HttpContext context)
    {

        if ((context == null))
        {
            throw new ArgumentNullException("There is no httpContext Associated with the Request.");
        }

        if ((context.Response == null))
        {
            throw new ArgumentNullException("There is no httpContext.Response Associated with the Request.");
        }

        CurrentContext = context;
        Response = context.Response;
        Request = context.Request;

        context.Response.ClearContent();

        if ((Request.QueryString["EventId"] != null))
        {


            BuildVCal();
        }
        else
        {

            SendNoEventMessage();

        }
    }

    private void SendNoEventMessage()
    {
        //Instead of throwing an error, let the user know what they did wrong in a graceful manner.
        CurrentContext.Response.ContentType = "text/HTML";
        CurrentContext.Response.Write("<P>Sorry, Please supply a valid EventId.</P>");
        CurrentContext.Response.Flush();
        CurrentContext.Response.End();
        return;
    }

    private void BuildVCal()
    {

        using (EventRepository lEventrpt = new EventRepository())
        {

            int lEventId = int.Parse(Request.QueryString["EventId"]);

            if (lEventId <= 0)
            {
                SendNoEventMessage();
            }

            EventInfo lEventInfo = lEventrpt.GetEventInfoById(lEventId);

            if ((lEventInfo != null))
            {

                vCalendar lvCal = new vCalendar();
                vCalendar.vEvent lEvent = new vCalendar.vEvent();

                lEvent.Description = lEventInfo.EventTitle;
                lEvent.Location = lEventInfo.EventLocation;
                lEvent.URL = string.Format("{0}?eventid={1}", Path.Combine(Helpers.WebRoot, "ShowEvent.aspx"), lEventId);
                lEvent.Summary = lEventInfo.EventDesc;

                Regex rgTime = new Regex("^(\\d{2}):(\\d{2}):(\\d{2})\\ (PM|AM)$");

                int mHour = 0;
                int mMinute = 0;

                foreach (Match m in rgTime.Matches(lEventInfo.EventTime))
                {

                    if (m.Groups.Count > 3)
                    {

                        if (m.Groups[4].Value == "PM")
                        {
                            mHour = 12 + int.Parse( m.Groups[1].Value);
                        }
                        else
                        {
                            mHour = int.Parse( m.Groups[1].Value);
                        }


                        mMinute = int.Parse( m.Groups[2].Value);

                    }
                }


                lEvent.DTStart = new System.DateTime(lEventInfo.EventDate.Year, lEventInfo.EventDate.Month, lEventInfo.EventDate.Day, mHour, mMinute, 0);

                mHour = 0;
                mMinute = 0;

                foreach (Match m in rgTime.Matches(lEventInfo.EndTime))
                {

                    if (m.Groups.Count > 3)
                    {

                        if (m.Groups[4].Value == "PM")
                        {
                            mHour = 12 + int.Parse( m.Groups[1].Value);
                        }
                        else
                        {
                            mHour = int.Parse( m.Groups[1].Value);
                        }


                        mMinute = int.Parse( m.Groups[2].Value);

                    }
                }

                //EventEndDate is a Nullable type, so we need to convert it to a hard date and time.
                DateTime dtEnd = lEventInfo.EventEndDate ?? lEventInfo.EventDate;

                if (mHour > 0)
                {
                    lEvent.DTEnd = new System.DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, mHour, mMinute, 0);
                }

                lvCal.Events.Add(lEvent);

                CurrentContext.Response.ContentType = "text/calendar";
                CurrentContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                CurrentContext.Response.Write(lvCal.ToString());
                CurrentContext.Response.Flush();

                CurrentContext.Response.End();
            }
            else
            {


                SendNoEventMessage();

            }

        }
    }

}