namespace TheBeerHouse.BLL.Gallery
{
    using System;
    using System.Collections.Generic;
    using System.Data.EntityClient;
    using System.Data.Objects;

    /// <summary>
    /// There are no comments for GalleryEntities in the schema.
    /// </summary>
    public class GalleryEntities : ObjectContext
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ObjectQuery<Album> _Albums;
        private ObjectQuery<Picture> _Pictures;

        /// <summary>
        /// Initializes a new GalleryEntities object using the connection string found in the 'GalleryEntities' section of the application configuration file.
        /// </summary>
        public GalleryEntities() : base("name=GalleryEntities", "GalleryEntities")
        {
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new GalleryEntities object.
        /// </summary>
        public GalleryEntities(EntityConnection connection) : base(connection, "GalleryEntities")
        {
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new GalleryEntities object.
        /// </summary>
        public GalleryEntities(string connectionString) : base(connectionString, "GalleryEntities")
        {
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// There are no comments for Albums in the schema.
        /// </summary>
        public void AddToAlbums(Album album)
        {
            base.AddObject("Albums", album);
        }

        /// <summary>
        /// There are no comments for Pictures in the schema.
        /// </summary>
        public void AddToPictures(Picture picture)
        {
            base.AddObject("Pictures", picture);
        }

        /// <summary>
        /// There are no comments for Albums in the schema.
        /// </summary>
        public ObjectQuery<Album> Albums
        {
            get
            {
                if (this._Albums == null)
                {
                    this._Albums = base.CreateQuery<Album>("[Albums]", new ObjectParameter[0]);
                }
                return this._Albums;
            }
        }

        /// <summary>
        /// There are no comments for Pictures in the schema.
        /// </summary>
        public ObjectQuery<Picture> Pictures
        {
            get
            {
                if (this._Pictures == null)
                {
                    this._Pictures = base.CreateQuery<Picture>("[Pictures]", new ObjectParameter[0]);
                }
                return this._Pictures;
            }
        }
    }
}

