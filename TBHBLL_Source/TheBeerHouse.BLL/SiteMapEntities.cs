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
    /// There are no comments for SiteMapEntities in the schema.
    /// </summary>
    public class SiteMapEntities : ObjectContext
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ObjectQuery<SiteMapInfo> _SiteMaps;

        /// <summary>
        /// Initializes a new SiteMapEntities object using the connection string found in the 'SiteMapEntities' section of the application configuration file.
        /// </summary>
        public SiteMapEntities() : base("name=SiteMapEntities", "SiteMapEntities")
        {
            base.SavingChanges += new EventHandler(this.SiteMapEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new SiteMapEntities object.
        /// </summary>
        public SiteMapEntities(EntityConnection connection) : base(connection, "SiteMapEntities")
        {
            base.SavingChanges += new EventHandler(this.SiteMapEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new SiteMapEntities object.
        /// </summary>
        public SiteMapEntities(string connectionString) : base(connectionString, "SiteMapEntities")
        {
            base.SavingChanges += new EventHandler(this.SiteMapEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        [DebuggerStepThrough, CompilerGenerated]
        private static bool _Lambda$__13(ObjectStateEntry entry)
        {
            return (entry.Entity is IBaseEntity);
        }

        /// <summary>
        /// There are no comments for SiteMaps in the schema.
        /// </summary>
        public void AddToSiteMaps(SiteMapInfo siteMapInfo)
        {
            base.AddObject("SiteMaps", siteMapInfo);
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void SiteMapEntities_SavingChanges(object sender, EventArgs e)
        {
            List<ObjectStateEntry>.Enumerator VB$t_struct$L0;
            List<ObjectStateEntry> typeEntries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added).Where<ObjectStateEntry>(new Func<ObjectStateEntry, bool>(SiteMapEntities._Lambda$__13)).ToList<ObjectStateEntry>();
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
        /// There are no comments for SiteMaps in the schema.
        /// </summary>
        public ObjectQuery<SiteMapInfo> SiteMaps
        {
            get
            {
                if (this._SiteMaps == null)
                {
                    this._SiteMaps = base.CreateQuery<SiteMapInfo>("[SiteMaps]", new ObjectParameter[0]);
                }
                return this._SiteMaps;
            }
        }
    }
}

