using System;

public class vCalendar
{
    #region "vCalendar Class"
    public vEvents Events;

    public override string ToString()
    {
        System.Text.StringBuilder result = new System.Text.StringBuilder();
        result.AppendFormat("BEGIN:VCALENDAR{0}", System.Environment.NewLine);
        //The following two lines seem to be required by Outlook to get the alarm settings
        result.AppendFormat("VERSION:2.0{0}", System.Environment.NewLine);
        result.AppendFormat("METHOD:PUBLISH{0}", System.Environment.NewLine);
        foreach (vEvent item in Events)
        {
            result.Append(item.ToString());
        }
        result.AppendFormat("END:VCALENDAR{0}", System.Environment.NewLine);
        return result.ToString();
    }

    public vCalendar(vEvent Value)
    {
        this.Events = new vEvents();
        this.Events.Add(Value);
    }

    public vCalendar()
    {
        this.Events = new vEvents();
    }
    #endregion

    #region "vAlarm Class"
    public class vAlarm
    {
        public TimeSpan Trigger;
        //Amount of time before event to display alarm
        public string Action;
        //Action to take to notify user of alarm
        public string Description;
        //Description of the alarm

        public vAlarm()
        {
            Trigger = TimeSpan.FromDays(1);
            Action = "DISPLAY";
            Description = "Reminder";
        }

        public vAlarm(TimeSpan SetTrigger)
        {
            Trigger = SetTrigger;
            Action = "DISPLAY";
            Description = "Reminder";
        }

        public vAlarm(TimeSpan SetTrigger, string SetAction, string SetDescription)
        {
            Trigger = SetTrigger;
            Action = SetAction;
            Description = SetDescription;
        }

        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            result.AppendFormat("BEGIN:VALARM{0}", System.Environment.NewLine);
            result.AppendFormat("TRIGGER:P{0}DT{1}H{2}M{3}", Trigger.Days, Trigger.Hours, Trigger.Minutes, System.Environment.NewLine);
            result.AppendFormat("ACTION:{0}{1}", Action, System.Environment.NewLine);
            result.AppendFormat("DESCRIPTION:{0}{1}", Description, System.Environment.NewLine);
            result.AppendFormat("END:VALARM{0}", System.Environment.NewLine);
            return result.ToString();
        }
    }
    #endregion

    #region "vEvent Class"
    public class vEvent
    {
        public string UID;
        //Unique identifier for the event
        public System.DateTime DTStart;
        //Start date of event. Will be automatically converted to GMT
        public System.DateTime DTEnd;
        //End date of event. Will be automatically converted to GMT
        public System.DateTime DTStamp;
        //Timestamp. Will be automatically converted to GMT
        public string Summary;
        //Summary/Subject of event
        public string Organizer;
        //Can be mailto: url or just a name
        public string Location;
        public string Description;
        public string URL;
        public vAlarms Alarms;
        //Alarms needed for this event

        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            result.AppendFormat("BEGIN:VEVENT{0}", System.Environment.NewLine);
            result.AppendFormat("UID:{0}{1}", UID, System.Environment.NewLine);
            result.AppendFormat("SUMMARY:{0}{1}", Summary, System.Environment.NewLine);
            result.AppendFormat("ORGANIZER:{0}{1}", Organizer, System.Environment.NewLine);
            result.AppendFormat("LOCATION:{0}{1}", Location, System.Environment.NewLine);
            result.AppendFormat("DTSTART:{0}{1}", DTStart.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z"), System.Environment.NewLine);
            result.AppendFormat("DTEND:{0}{1}", DTEnd.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z"), System.Environment.NewLine);
            result.AppendFormat("DTSTAMP:{0}{1}", DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z"), System.Environment.NewLine);
            result.AppendFormat("DESCRIPTION:{0}{1}", Description, System.Environment.NewLine);
            if (URL.Length > 0) result.AppendFormat("URL:{0}{1}", URL, System.Environment.NewLine);

            foreach (vAlarm item in Alarms)
            {
                result.Append(item.ToString());
            }
            result.AppendFormat("END:VEVENT{0}", System.Environment.NewLine);
            return result.ToString();
        }

        public vEvent()
        {
            this.Alarms = new vAlarms();
        }
    }
    #endregion

    #region "vAlarms Class"
    public class vAlarms : System.Collections.CollectionBase
    {
        // The first thing to do when building a CollectionBase class is to inherit from System.Collections.CollectionBase

        public vAlarm Add(vAlarm Value)
        {
            // After you inherit the CollectionBase class, you can access an intrinsic object
            // called InnerList that represents your collection. InnerList is of type ArrayList.
            this.InnerList.Add(Value);
            return Value;
        }

        public vAlarm Item(int Index)
        {
            // To retrieve an item from the InnerList, pass the index of that item to the .Item property.
            return (vAlarm)this.InnerList[Index];
        }

        public void Remove(int Index)
        {
            // This Remove expects an index.
            vAlarm cust = default(vAlarm);

            cust = (vAlarm)this.InnerList[Index];
            if ((cust != null))
            {
                this.InnerList.Remove(cust);
            }
        }

    }
    #endregion

    #region "vEvents Class"
    public class vEvents : System.Collections.CollectionBase
    {
        // The first thing to do when building a CollectionBase class is to inherit from System.Collections.CollectionBase

        public vEvent Add(vEvent Value)
        {
            // After you inherit the CollectionBase class, you can access an intrinsic object
            // called InnerList that represents your collection. InnerList is of type ArrayList.
            this.InnerList.Add(Value);
            return Value;
        }

        public vEvent Item(int Index)
        {
            // To retrieve an item from the InnerList, pass the index of that item to the .Item property.
            return (vEvent)this.InnerList[Index];
        }

        public void Remove(int Index)
        {
            // This Remove expects an index.
            vEvent cust = default(vEvent);

            cust = (vEvent)this.InnerList[Index];
            if ((cust != null))
            {
                this.InnerList.Remove(cust);
            }
        }
    }
    #endregion
}