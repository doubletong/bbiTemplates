namespace TheBeerHouse.BLL.Forums
{
    using Microsoft.VisualBasic;
    using System;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    public class BaseForumRepository : BaseRepository
    {
        private ForumModel _Forumctx;
        private bool disposedValue;

        public BaseForumRepository()
        {
            this.disposedValue = false;
            this.ConnectionString = TheBeerHouse.Globals.Settings.DefaultConnectionStringName;
            this.CacheKey = "Forums";
        }

        public BaseForumRepository(string sConnectionString)
        {
            this.disposedValue = false;
            this.ConnectionString = sConnectionString;
            this.CacheKey = "Forums";
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!this.disposedValue && disposing) && !Information.IsNothing(this._Forumctx))
            {
                this._Forumctx.Dispose();
            }
            this.disposedValue = true;
        }

        public ForumModel Forumctx
        {
            get
            {
                if (Information.IsNothing(this._Forumctx))
                {
                    this._Forumctx = new ForumModel(this.GetActualConnectionString());
                }
                return this._Forumctx;
            }
            set
            {
                this._Forumctx = value;
            }
        }
    }
}

