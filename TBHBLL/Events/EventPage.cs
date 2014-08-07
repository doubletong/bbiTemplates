using System;
using System.Collections.Generic;
using System.Diagnostics;
using BBICMS.UI;

namespace BBICMS
{
    public class EventPage : BasePage
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        [DebuggerNonUserCode]
        public EventPage()
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        public int EventId
        {
            get { return PrimaryKeyId("EventId"); }
            set { ViewState["EventId"] = value; }
        }

        public int EventRSVPId
        {
            get { return PrimaryKeyId("EventRSVPId"); }
            set { ViewState["EventRSVPId"] = value; }
        }
    }
}