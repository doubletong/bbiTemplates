using System;
using BBICMS.BLL;

namespace BBICMS.Polls
{

    public class BasePollRepository : BaseRepository
    {


        #region " Constructors "

        public BasePollRepository(string sConnectionString)
        {
            ConnectionString = sConnectionString;
            CacheKey = "Polls";
        }

        public BasePollRepository()
        {
            ConnectionString = Globals.Settings.DefaultConnectionStringName;
            CacheKey = "Polls";
        }

        #endregion

        private PollEntities _Pollsctx;
        public PollEntities Pollsctx
        {
            get
            {
                if ((_Pollsctx == null))
                {
                    _Pollsctx = new PollEntities(GetActualConnectionString());
                }

                return _Pollsctx;
            }
            set { _Pollsctx = value; }
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

                    if ((_Pollsctx == null) == false)
                    {
                        _Pollsctx.Dispose();

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