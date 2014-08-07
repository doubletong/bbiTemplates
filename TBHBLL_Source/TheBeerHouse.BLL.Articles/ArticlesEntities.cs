namespace TheBeerHouse.BLL.Articles
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
    /// There are no comments for ArticlesEntities in the schema.
    /// </summary>
    public class ArticlesEntities : ObjectContext
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ObjectQuery<Article> _Articles;
        private ObjectQuery<Category> _Categories;
        private ObjectQuery<Comment> _Comments;

        /// <summary>
        /// Initializes a new ArticlesEntities object using the connection string found in the 'ArticlesEntities' section of the application configuration file.
        /// </summary>
        public ArticlesEntities() : base("name=ArticlesEntities", "ArticlesEntities")
        {
            base.SavingChanges += new EventHandler(this.ArticlesEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new ArticlesEntities object.
        /// </summary>
        public ArticlesEntities(EntityConnection connection) : base(connection, "ArticlesEntities")
        {
            base.SavingChanges += new EventHandler(this.ArticlesEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new ArticlesEntities object.
        /// </summary>
        public ArticlesEntities(string connectionString) : base(connectionString, "ArticlesEntities")
        {
            base.SavingChanges += new EventHandler(this.ArticlesEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        [DebuggerStepThrough, CompilerGenerated]
        private static bool _Lambda$__2(ObjectStateEntry entry)
        {
            return (entry.Entity is IBaseEntity);
        }

        /// <summary>
        /// There are no comments for Articles in the schema.
        /// </summary>
        public void AddToArticles(Article article)
        {
            base.AddObject("Articles", article);
        }

        /// <summary>
        /// There are no comments for Categories in the schema.
        /// </summary>
        public void AddToCategories(Category category)
        {
            base.AddObject("Categories", category);
        }

        /// <summary>
        /// There are no comments for Comments in the schema.
        /// </summary>
        public void AddToComments(Comment comment)
        {
            base.AddObject("Comments", comment);
        }

        /// <summary>
        /// Checks each object that is either new to the Context or has been updated 
        /// to verify each is valid. If one does not return true when the Entity's 
        /// IsValid method is called a BeerHouseDataException is thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This is an event handler that will get called when ever the SaveChanges method is called.</remarks>
        private void ArticlesEntities_SavingChanges(object sender, EventArgs e)
        {
            List<ObjectStateEntry>.Enumerator VB$t_struct$L0;
            List<ObjectStateEntry> typeEntries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added).Where<ObjectStateEntry>(new Func<ObjectStateEntry, bool>(ArticlesEntities._Lambda$__2)).ToList<ObjectStateEntry>();
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
        /// There are no comments for Articles in the schema.
        /// </summary>
        public ObjectQuery<Article> Articles
        {
            get
            {
                if (this._Articles == null)
                {
                    this._Articles = base.CreateQuery<Article>("[Articles]", new ObjectParameter[0]);
                }
                return this._Articles;
            }
        }

        /// <summary>
        /// There are no comments for Categories in the schema.
        /// </summary>
        public ObjectQuery<Category> Categories
        {
            get
            {
                if (this._Categories == null)
                {
                    this._Categories = base.CreateQuery<Category>("[Categories]", new ObjectParameter[0]);
                }
                return this._Categories;
            }
        }

        /// <summary>
        /// There are no comments for Comments in the schema.
        /// </summary>
        public ObjectQuery<Comment> Comments
        {
            get
            {
                if (this._Comments == null)
                {
                    this._Comments = base.CreateQuery<Comment>("[Comments]", new ObjectParameter[0]);
                }
                return this._Comments;
            }
        }
    }
}

