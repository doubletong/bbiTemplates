namespace TheBeerHouse.BLL.Newsletters
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
    /// There are no comments for NewsletterEntities in the schema.
    /// </summary>
    public class NewsletterEntities : ObjectContext
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ObjectQuery<Newsletter> _Newsletters;

        /// <summary>
        /// Initializes a new NewsletterEntities object using the connection string found in the 'NewsletterEntities' section of the application configuration file.
        /// </summary>
        public NewsletterEntities() : base("name=NewsletterEntities", "NewsletterEntities")
        {
            base.SavingChanges += new EventHandler(this.NewsletterEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new NewsletterEntities object.
        /// </summary>
        public NewsletterEntities(EntityConnection connection) : base(connection, "NewsletterEntities")
        {
            base.SavingChanges += new EventHandler(this.NewsletterEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new NewsletterEntities object.
        /// </summary>
        public NewsletterEntities(string connectionString) : base(connectionString, "NewsletterEntities")
        {
            base.SavingChanges += new EventHandler(this.NewsletterEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        [DebuggerStepThrough, CompilerGenerated]
        private static bool _Lambda$__18(ObjectStateEntry entry)
        {
            return (entry.Entity is IBaseEntity);
        }

        /// <summary>
        /// There are no comments for Newsletters in the schema.
        /// </summary>
        public void AddToNewsletters(Newsletter newsletter)
        {
            base.AddObject("Newsletters", newsletter);
        }

        /// <summary>
        /// Checks each object that is either new to the Context or has been updated 
        /// to verify each is valid. If one does not return true when the Entity's 
        /// IsValid method is called a BeerHouseDataException is thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This is an event handler that will get called when ever the SaveChanges method is called.</remarks>
        private void NewsletterEntities_SavingChanges(object sender, EventArgs e)
        {
            List<ObjectStateEntry>.Enumerator VB$t_struct$L0;
            List<ObjectStateEntry> typeEntries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added).Where<ObjectStateEntry>(new Func<ObjectStateEntry, bool>(NewsletterEntities._Lambda$__18)).ToList<ObjectStateEntry>();
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
        /// There are no comments for Newsletters in the schema.
        /// </summary>
        public ObjectQuery<Newsletter> Newsletters
        {
            get
            {
                if (this._Newsletters == null)
                {
                    this._Newsletters = base.CreateQuery<Newsletter>("[Newsletters]", new ObjectParameter[0]);
                }
                return this._Newsletters;
            }
        }
    }
}

