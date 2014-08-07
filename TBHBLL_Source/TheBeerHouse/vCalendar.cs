namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Text;

    /// <summary>
    /// Created by Slolife on www.CodeProject.com
    /// http://www.codeproject.com/KB/vb/vcalendar.aspx
    /// </summary>
    /// <remarks>Nice simple class to manage sharing event information via the vCal format.</remarks>
    public class vCalendar
    {
        public vEvents Events;

        public vCalendar()
        {
            this.Events = new vEvents();
        }

        public vCalendar(vEvent Value)
        {
            this.Events = new vEvents();
            this.Events.Add(Value);
        }

        public override string ToString()
        {
            IEnumerator VB$t_ref$L0;
            StringBuilder result = new StringBuilder();
            result.AppendFormat("BEGIN:VCALENDAR{0}", Environment.NewLine);
            result.AppendFormat("VERSION:2.0{0}", Environment.NewLine);
            result.AppendFormat("METHOD:PUBLISH{0}", Environment.NewLine);
            try
            {
                VB$t_ref$L0 = this.Events.GetEnumerator();
                while (VB$t_ref$L0.MoveNext())
                {
                    result.Append(((vEvent) VB$t_ref$L0.Current).ToString());
                }
            }
            finally
            {
                if (VB$t_ref$L0 is IDisposable)
                {
                    (VB$t_ref$L0 as IDisposable).Dispose();
                }
            }
            result.AppendFormat("END:VCALENDAR{0}", Environment.NewLine);
            return result.ToString();
        }

        public class vAlarm
        {
            public string Action;
            public string Description;
            public TimeSpan Trigger;

            public vAlarm()
            {
                this.Trigger = TimeSpan.FromDays(1.0);
                this.Action = "DISPLAY";
                this.Description = "Reminder";
            }

            public vAlarm(TimeSpan SetTrigger)
            {
                this.Trigger = SetTrigger;
                this.Action = "DISPLAY";
                this.Description = "Reminder";
            }

            public vAlarm(TimeSpan SetTrigger, string SetAction, string SetDescription)
            {
                this.Trigger = SetTrigger;
                this.Action = SetAction;
                this.Description = SetDescription;
            }

            public override string ToString()
            {
                StringBuilder result = new StringBuilder();
                result.AppendFormat("BEGIN:VALARM{0}", Environment.NewLine);
                result.AppendFormat("TRIGGER:P{0}DT{1}H{2}M{3}", new object[] { this.Trigger.Days, this.Trigger.Hours, this.Trigger.Minutes, Environment.NewLine });
                result.AppendFormat("ACTION:{0}{1}", this.Action, Environment.NewLine);
                result.AppendFormat("DESCRIPTION:{0}{1}", this.Description, Environment.NewLine);
                result.AppendFormat("END:VALARM{0}", Environment.NewLine);
                return result.ToString();
            }
        }

        public class vAlarms : CollectionBase
        {
            public vCalendar.vAlarm Add(vCalendar.vAlarm Value)
            {
                this.InnerList.Add(Value);
                return Value;
            }

            public vCalendar.vAlarm Item(int Index)
            {
                return (vCalendar.vAlarm) this.InnerList[Index];
            }

            public void Remove(int Index)
            {
                vCalendar.vAlarm cust = (vCalendar.vAlarm) this.InnerList[Index];
                if (cust != null)
                {
                    this.InnerList.Remove(cust);
                }
            }
        }

        public class vEvent
        {
            public vCalendar.vAlarms Alarms = new vCalendar.vAlarms();
            public string Description;
            public DateTime DTEnd;
            public DateTime DTStamp;
            public DateTime DTStart;
            public string Location;
            public string Organizer;
            public string Summary;
            public string UID;
            public string URL;

            public override string ToString()
            {
                IEnumerator VB$t_ref$L0;
                StringBuilder result = new StringBuilder();
                result.AppendFormat("BEGIN:VEVENT{0}", Environment.NewLine);
                result.AppendFormat("UID:{0}{1}", this.UID, Environment.NewLine);
                result.AppendFormat("SUMMARY:{0}{1}", this.Summary, Environment.NewLine);
                result.AppendFormat("ORGANIZER:{0}{1}", this.Organizer, Environment.NewLine);
                result.AppendFormat("LOCATION:{0}{1}", this.Location, Environment.NewLine);
                result.AppendFormat("DTSTART:{0}{1}", this.DTStart.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"), Environment.NewLine);
                result.AppendFormat("DTEND:{0}{1}", this.DTEnd.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"), Environment.NewLine);
                result.AppendFormat("DTSTAMP:{0}{1}", DateAndTime.Now.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"), Environment.NewLine);
                result.AppendFormat("DESCRIPTION:{0}{1}", this.Description, Environment.NewLine);
                if (this.URL.Length > 0)
                {
                    result.AppendFormat("URL:{0}{1}", this.URL, Environment.NewLine);
                }
                try
                {
                    VB$t_ref$L0 = this.Alarms.GetEnumerator();
                    while (VB$t_ref$L0.MoveNext())
                    {
                        result.Append(((vCalendar.vAlarm) VB$t_ref$L0.Current).ToString());
                    }
                }
                finally
                {
                    if (VB$t_ref$L0 is IDisposable)
                    {
                        (VB$t_ref$L0 as IDisposable).Dispose();
                    }
                }
                result.AppendFormat("END:VEVENT{0}", Environment.NewLine);
                return result.ToString();
            }
        }

        public class vEvents : CollectionBase
        {
            public vCalendar.vEvent Add(vCalendar.vEvent Value)
            {
                this.InnerList.Add(Value);
                return Value;
            }

            public vCalendar.vEvent Item(int Index)
            {
                return (vCalendar.vEvent) this.InnerList[Index];
            }

            public void Remove(int Index)
            {
                vCalendar.vEvent cust = (vCalendar.vEvent) this.InnerList[Index];
                if (cust != null)
                {
                    this.InnerList.Remove(cust);
                }
            }
        }
    }
}

