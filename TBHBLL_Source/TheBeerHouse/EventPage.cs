namespace TheBeerHouse
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TheBeerHouse.UI;

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

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int EventId
        {
            get
            {
                return this.get_PrimaryKeyId("EventId");
            }
            set
            {
                this.set_PrimaryKeyId("EventId", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int EventRSVPId
        {
            get
            {
                return this.get_PrimaryKeyId("EventRSVPId");
            }
            set
            {
                this.set_PrimaryKeyId("EventRSVPId", value);
            }
        }
    }
}

