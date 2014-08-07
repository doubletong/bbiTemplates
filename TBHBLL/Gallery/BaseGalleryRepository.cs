using System;
using BBICMS.BLL;

namespace BBICMS.Gallery
{

    public class BaseGalleryRepository : BaseRepository
    {
        private GalleryEntities _Galleryctx;
        private bool disposedValue;

        public BaseGalleryRepository()
        {
            this.disposedValue = false;
            this.ConnectionString = BBICMS.Globals.Settings.DefaultConnectionStringName;
            this.CacheKey = "Gallery";
        }

        public BaseGalleryRepository(string sConnectionString)
        {
            this.disposedValue = false;
            this.ConnectionString = sConnectionString;
            this.CacheKey = "Gallery";
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!this.disposedValue && disposing) && null != this.Galleryctx)
            {
                this.Galleryctx.Dispose();
            }
            this.disposedValue = true;
        }

        public GalleryEntities Galleryctx
        {
            get
            {
                if (null == this._Galleryctx)
                {
                    this._Galleryctx = new GalleryEntities(this.GetActualConnectionString());
                }
                return this._Galleryctx;
            }
            set
            {
                this._Galleryctx = value;
            }
        }
    }
}

