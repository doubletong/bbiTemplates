
using System;
using BBICMS.Articles;


namespace BBICMS.BLL.Articles
{

    public class BaseArticleRepository : BaseRepository
    {
        private ArticlesEntities _Articlesctx;
        private bool disposedValue;

        public BaseArticleRepository()
        {
            disposedValue = false;
            ConnectionString = Globals.Settings.DefaultConnectionStringName;
            CacheKey = "Articles";
        }

        public BaseArticleRepository(string sConnectionString)
        {
            disposedValue = false;
            ConnectionString = sConnectionString;
            CacheKey = "Articles";
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!disposedValue && disposing) && null != _Articlesctx)
            {
                _Articlesctx.Dispose();
            }
            disposedValue = true;
        }

        public ArticlesEntities Articlesctx
        {
            get
            {
                if (null == _Articlesctx)
                {
                    _Articlesctx = new ArticlesEntities(GetActualConnectionString());
                }
                return _Articlesctx;
            }
            set
            {
                _Articlesctx = value;
            }
        }
    }
}

