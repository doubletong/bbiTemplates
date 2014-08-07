namespace TheBeerHouse.BLL.EventCalendar
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.EntityClient;
    using System.Data.Objects;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for CalendarofEventsEntities in the schema.
    /// </summary>
    public class CalendarofEventsEntities : ObjectContext
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ObjectQuery<EventInfo> _EventInfos;
        private ObjectQuery<EventRSVP> _EventRSVPs;

        /// <summary>
        /// Initializes a new CalendarofEventsEntities object using the connection string found in the 'CalendarofEventsEntities' section of the application configuration file.
        /// </summary>
        public CalendarofEventsEntities() : base("name=CalendarofEventsEntities", "CalendarofEventsEntities")
        {
            base.SavingChanges += new EventHandler(this.EventEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new CalendarofEventsEntities object.
        /// </summary>
        public CalendarofEventsEntities(EntityConnection connection) : base(connection, "CalendarofEventsEntities")
        {
            base.SavingChanges += new EventHandler(this.EventEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new CalendarofEventsEntities object.
        /// </summary>
        public CalendarofEventsEntities(string connectionString) : base(connectionString, "CalendarofEventsEntities")
        {
            base.SavingChanges += new EventHandler(this.EventEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        [CompilerGenerated, DebuggerStepThrough]
        private static bool _Lambda$__4(ObjectStateEntry entry)
        {
            return (entry.Entity is IBaseEntity);
        }

        /// <summary>
        /// There are no comments for EventInfos in the schema.
        /// </summary>
        public void AddToEventInfos(EventInfo eventInfo)
        {
            base.AddObject("EventInfos", eventInfo);
        }

        /// <summary>
        /// There are no comments for EventRSVPs in the schema.
        /// </summary>
        public void AddToEventRSVPs(EventRSVP eventRSVP)
        {
            base.AddObject("EventRSVPs", eventRSVP);
        }

        /// <summary>
        /// Checks each object that is either new to the Context or has been updated 
        /// to verify each is valid. If one does not return true when the Entity's 
        /// IsValid method is called a BeerHouseDataException is thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This is an event handler that will get called when ever the SaveChanges method is called.</remarks>
        private void EventEntities_SavingChanges(object sender, EventArgs e)
        {
            List<ObjectStateEntry>.Enumerator VB$t_struct$L0;
            List<ObjectStateEntry> typeEntries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added).Where<ObjectStateEntry>(new Func<ObjectStateEntry, bool>(CalendarofEventsEntities._Lambda$__4)).ToList<ObjectStateEntry>();
            try
            {
                VB$t_struct$L0 = typeEntries.GetEnumerator();
                while (VB$t_struct$L0.MoveNext())
                {
                    ObjectStateEntry ose = VB$t_struct$L0.Current;
                    IBaseEntity lBaseEntity = (IBaseEntity) ose.Entity;
                    if (!lBaseEntity.IsValid)
                    {
                        throw new BeerHouseDataException(string.Format("{0} is Not Valid", lBaseEntity.SetName), "", "");
                    }
                }
            }
            finally
            {
                VB$t_struct$L0.Dispose();
            }
        }

        /// <summary>
        /// There are no comments for EventInfos in the schema.
        /// </summary>
        public ObjectQuery<EventInfo> EventInfos
        {
            get
            {
                if (this._EventInfos == null)
                {
                    this._EventInfos = base.CreateQuery<EventInfo>("[EventInfos]", new ObjectParameter[0]);
                }
                return this._EventInfos;
            }
        }

        /// <summary>
        /// There are no comments for EventRSVPs in the schema.
        /// </summary>
        public ObjectQuery<EventRSVP> EventRSVPs
        {
            get
            {
                if (this._EventRSVPs == null)
                {
                    this._EventRSVPs = base.CreateQuery<EventRSVP>("[EventRSVPs]", new ObjectParameter[0]);
                }
                return this._EventRSVPs;
            }
        }
    }
}

