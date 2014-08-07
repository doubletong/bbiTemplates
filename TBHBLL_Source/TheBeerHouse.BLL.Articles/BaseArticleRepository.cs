namespace TheBeerHouse.BLL.Articles
{
    using Microsoft.VisualBasic;
    using System;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    public class BaseArticleRepository : BaseRepository
    {
        private ArticlesEntities _Articlesctx;
        private bool disposedValue;

        public BaseArticleRepository()
        {
            this.disposedValue = false;
            this.ConnectionString = TheBeerHouse.Globals.Settings.DefaultConnectionStringName;
            this.CacheKey = "Articles";
        }

        public BaseArticleRepository(string sConnectionString)
        {
            this.disposedValue = false;
            this.ConnectionString = sConnectionString;
            this.CacheKey = "Articles";
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!this.disposedValue && disposing) && !Information.IsNothing(this._Articlesctx))
            {
                this._Articlesctx.Dispose();
            }
            this.disposedValue = true;
        }

        public ArticlesEntities Articlesctx
        {
            get
            {
                if (Information.IsNothing(this._Articlesctx))
                {
                    this._Articlesctx = new ArticlesEntities(this.GetActualConnectionString());
                }
                return this._Articlesctx;
            }
            set
            {
                this._Articlesctx = value;
            }
        }
    }
}

