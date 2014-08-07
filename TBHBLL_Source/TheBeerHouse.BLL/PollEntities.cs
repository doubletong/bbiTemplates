namespace TheBeerHouse.BLL
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

    /// <summary>
    /// There are no comments for PollEntities in the schema.
    /// </summary>
    public class PollEntities : ObjectContext
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ObjectQuery<PollOption> _PollOptions;
        private ObjectQuery<Poll> _Polls;

        /// <summary>
        /// Initializes a new PollEntities object using the connection string found in the 'PollEntities' section of the application configuration file.
        /// </summary>
        public PollEntities() : base("name=PollEntities", "PollEntities")
        {
            base.SavingChanges += new EventHandler(this.PollEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new PollEntities object.
        /// </summary>
        public PollEntities(EntityConnection connection) : base(connection, "PollEntities")
        {
            base.SavingChanges += new EventHandler(this.PollEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new PollEntities object.
        /// </summary>
        public PollEntities(string connectionString) : base(connectionString, "PollEntities")
        {
            base.SavingChanges += new EventHandler(this.PollEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        [CompilerGenerated, DebuggerStepThrough]
        private static bool _Lambda$__21(ObjectStateEntry entry)
        {
            return (entry.Entity is IBaseEntity);
        }

        /// <summary>
        /// There are no comments for PollOptions in the schema.
        /// </summary>
        public void AddToPollOptions(PollOption pollOption)
        {
            base.AddObject("PollOptions", pollOption);
        }

        /// <summary>
        /// There are no comments for Polls in the schema.
        /// </summary>
        public void AddToPolls(Poll poll)
        {
            base.AddObject("Polls", poll);
        }

        /// <summary>
        /// Checks each object that is either new to the Context or has been updated 
        /// to verify each is valid. If one does not return true when the Entity's 
        /// IsValid method is called a BeerHouseDataException is thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This is an event handler that will get called when ever the SaveChanges method is called.</remarks>
        private void PollEntities_SavingChanges(object sender, EventArgs e)
        {
            List<ObjectStateEntry>.Enumerator VB$t_struct$L0;
            List<ObjectStateEntry> typeEntries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added).Where<ObjectStateEntry>(new Func<ObjectStateEntry, bool>(PollEntities._Lambda$__21)).ToList<ObjectStateEntry>();
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
        /// There are no comments for PollOptions in the schema.
        /// </summary>
        public ObjectQuery<PollOption> PollOptions
        {
            get
            {
                if (this._PollOptions == null)
                {
                    this._PollOptions = base.CreateQuery<PollOption>("[PollOptions]", new ObjectParameter[0]);
                }
                return this._PollOptions;
            }
        }

        /// <summary>
        /// There are no comments for Polls in the schema.
        /// </summary>
        public ObjectQuery<Poll> Polls
        {
            get
            {
                if (this._Polls == null)
                {
                    this._Polls = base.CreateQuery<Poll>("[Polls]", new ObjectParameter[0]);
                }
                return this._Polls;
            }
        }
    }
}

