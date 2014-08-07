namespace TheBeerHouse.BLL
{
    using Microsoft.VisualBasic;
    using System;
    using TheBeerHouse;

    public class BasePollRepository : BaseRepository
    {
        private PollEntities _Pollsctx;
        private bool disposedValue;

        public BasePollRepository()
        {
            this.disposedValue = false;
            this.ConnectionString = TheBeerHouse.Globals.Settings.DefaultConnectionStringName;
            this.CacheKey = "Polls";
        }

        public BasePollRepository(string sConnectionString)
        {
            this.disposedValue = false;
            this.ConnectionString = sConnectionString;
            this.CacheKey = "Polls";
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!this.disposedValue && disposing) && !Information.IsNothing(this._Pollsctx))
            {
                this._Pollsctx.Dispose();
            }
            this.disposedValue = true;
        }

        public PollEntities Pollsctx
        {
            get
            {
                if (Information.IsNothing(this._Pollsctx))
                {
                    this._Pollsctx = new PollEntities(this.GetActualConnectionString());
                }
                return this._Pollsctx;
            }
            set
            {
                this._Pollsctx = value;
            }
        }
    }
}

