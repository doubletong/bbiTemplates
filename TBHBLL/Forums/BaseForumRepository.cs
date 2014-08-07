
using System;
using BBICMS;
using BBICMS.BLL;

namespace BBICMS.Forums
{

    public class BaseForumRepository : BaseRepository
    {

        #region " Constructors "

        public BaseForumRepository(string sConnectionString)
        {
            ConnectionString = sConnectionString;
            CacheKey = "Forums";
        }

        public BaseForumRepository()
        {
            ConnectionString = Globals.Settings.DefaultConnectionStringName;
            CacheKey = "Forums";
        }

        #endregion

        private ForumModel _Forumctx;
        public ForumModel Forumctx
        {
            get
            {
                if ((_Forumctx == null))
                {
                    _Forumctx = new ForumModel(GetActualConnectionString());
                }

                return _Forumctx;
            }
            set { _Forumctx = value; }
        }


        #region " Dispose "

        private bool disposedValue = false;
        // To detect redundant calls

        // IDisposable
        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                    if ((_Forumctx == null) == false)
                    {
                        _Forumctx.Dispose();

                    }

                }
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public override void Dispose()
        {

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

    }
}