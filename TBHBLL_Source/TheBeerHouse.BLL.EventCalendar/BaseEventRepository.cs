namespace TheBeerHouse.BLL.EventCalendar
{
    using Microsoft.VisualBasic;
    using System;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    public class BaseEventRepository : BaseRepository
    {
        private CalendarofEventsEntities _Eventctx;
        private bool disposedValue;

        public BaseEventRepository()
        {
            this.disposedValue = false;
            this.ConnectionString = TheBeerHouse.Globals.Settings.DefaultConnectionStringName;
            this.CacheKey = "Events";
        }

        public BaseEventRepository(string sConnectionString)
        {
            this.disposedValue = false;
            this.ConnectionString = sConnectionString;
            this.CacheKey = "Events";
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!this.disposedValue && disposing) && !Information.IsNothing(this._Eventctx))
            {
                this._Eventctx.Dispose();
            }
            this.disposedValue = true;
        }

        public CalendarofEventsEntities Eventctx
        {
            get
            {
                if (Information.IsNothing(this._Eventctx))
                {
                    this._Eventctx = new CalendarofEventsEntities(this.GetActualConnectionString());
                }
                return this._Eventctx;
            }
            set
            {
                this._Eventctx = value;
            }
        }
    }
}

