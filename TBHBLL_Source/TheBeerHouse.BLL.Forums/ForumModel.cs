namespace TheBeerHouse.BLL.Forums
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
    /// There are no comments for ForumModel in the schema.
    /// </summary>
    public class ForumModel : ObjectContext
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ObjectQuery<Forum> _Forums;
        private ObjectQuery<Post> _Posts;

        /// <summary>
        /// Initializes a new ForumModel object using the connection string found in the 'ForumModel' section of the application configuration file.
        /// </summary>
        public ForumModel() : base("name=ForumModel", "ForumModel")
        {
            base.SavingChanges += new EventHandler(this.ForumsEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new ForumModel object.
        /// </summary>
        public ForumModel(EntityConnection connection) : base(connection, "ForumModel")
        {
            base.SavingChanges += new EventHandler(this.ForumsEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new ForumModel object.
        /// </summary>
        public ForumModel(string connectionString) : base(connectionString, "ForumModel")
        {
            base.SavingChanges += new EventHandler(this.ForumsEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        [CompilerGenerated, DebuggerStepThrough]
        private static bool _Lambda$__6(ObjectStateEntry entry)
        {
            return (entry.Entity is IBaseEntity);
        }

        /// <summary>
        /// There are no comments for Forums in the schema.
        /// </summary>
        public void AddToForums(Forum forum)
        {
            base.AddObject("Forums", forum);
        }

        /// <summary>
        /// There are no comments for Posts in the schema.
        /// </summary>
        public void AddToPosts(Post post)
        {
            base.AddObject("Posts", post);
        }

        /// <summary>
        /// Checks each object that is either new to the Context or has been updated 
        /// to verify each is valid. If one does not return true when the Entity's 
        /// IsValid method is called a BeerHouseDataException is thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This is an event handler that will get called when ever the SaveChanges method is called.</remarks>
        private void ForumsEntities_SavingChanges(object sender, EventArgs e)
        {
            List<ObjectStateEntry>.Enumerator VB$t_struct$L0;
            List<ObjectStateEntry> typeEntries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added).Where<ObjectStateEntry>(new Func<ObjectStateEntry, bool>(ForumModel._Lambda$__6)).ToList<ObjectStateEntry>();
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
        /// There are no comments for Forums in the schema.
        /// </summary>
        public ObjectQuery<Forum> Forums
        {
            get
            {
                if (this._Forums == null)
                {
                    this._Forums = base.CreateQuery<Forum>("[Forums]", new ObjectParameter[0]);
                }
                return this._Forums;
            }
        }

        /// <summary>
        /// There are no comments for Posts in the schema.
        /// </summary>
        public ObjectQuery<Post> Posts
        {
            get
            {
                if (this._Posts == null)
                {
                    this._Posts = base.CreateQuery<Post>("[Posts]", new ObjectParameter[0]);
                }
                return this._Posts;
            }
        }
    }
}

