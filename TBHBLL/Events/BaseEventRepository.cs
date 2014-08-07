using System;
using BBICMS;
using BBICMS.BLL;

namespace BBICMS.Events

{
    public class BaseEventRepository : BaseRepository
    {
        private CalendarofEventsEntities _Eventctx;
        private bool disposedValue;

        public BaseEventRepository()
        {
            disposedValue = false;
            ConnectionString = Globals.Settings.DefaultConnectionStringName;
            CacheKey = "Events";
        }

        public BaseEventRepository(string sConnectionString)
        {
            disposedValue = false;
            ConnectionString = sConnectionString;
            CacheKey = "Events";
        }

        public CalendarofEventsEntities Eventctx
        {
            get
            {
                if (null == _Eventctx)
                {
                    _Eventctx = new CalendarofEventsEntities(GetActualConnectionString());
                }
                return _Eventctx;
            }
            set { _Eventctx = value; }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!disposedValue && disposing) && null != _Eventctx)
            {
                _Eventctx.Dispose();
            }
            disposedValue = true;
        }
    }
}