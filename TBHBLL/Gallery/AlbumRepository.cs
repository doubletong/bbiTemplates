using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using BBICMS;

namespace BBICMS.Gallery
{
    public class AlbumRepository : BaseGalleryRepository
    {
        #region " BLL/DAL Methods "

        public List<Album> GetActiveAlbums()
        {
            string key = CacheKey + "_Active_List";

            if (EnableCaching && (Cache[key]) != null)
            {
                return (List<Album>) Cache[key];
            }

            List<Album> lAlbums = new List<Album>();
            Galleryctx.Pictures.MergeOption = MergeOption.NoTracking;

            lAlbums = (from lAlbum in Galleryctx.Albums.Include("Pictures")
                       where lAlbum.Active
                       select lAlbum).ToList();

            if (EnableCaching)
            {
                CacheData(key, lAlbums, CacheDuration);
            }


            return lAlbums;
        }

        public List<Album> GetAlbums()
        {
            string key = CacheKey + "_FullList";

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Album>) Cache[key];
            }

            List<Album> lAlbums = default(List<Album>);
            Galleryctx.Pictures.MergeOption = MergeOption.NoTracking;

            lAlbums = (from lAlbum in Galleryctx.Albums.Include("Pictures")
                       select lAlbum).ToList();

            if (EnableCaching)
            {
                CacheData(key, lAlbums, CacheDuration);
            }


            return lAlbums;
        }

        public List<Album> GetChildAlbums(int AlbumId)
        {
            return (from lAlbum in Galleryctx.Albums
                    where lAlbum.ParentAlbumID == AlbumId
                    select lAlbum).ToList();
        }

        public Album GetAlbumById(int AlbumId)
        {
            return (from lAlbum in Galleryctx.Albums
                    where lAlbum.AlbumID == AlbumId
                    select lAlbum).FirstOrDefault();
        }

        public Album GetAlbumByIdWithPictures(int AlbumId)
        {
            return (from lAlbum in Galleryctx.Albums.Include("Pictures")
                    where lAlbum.AlbumID == AlbumId
                    select lAlbum).FirstOrDefault();
        }

        public List<Album> GetActiveChildAlbums(int AlbumId)
        {
            return (from lAlbum in Galleryctx.Albums.Include("Pictures")
                    where lAlbum.ParentAlbumID == AlbumId && lAlbum.Active
                    select lAlbum).ToList();
        }

        public int GetAlbumCount()
        {
            return (from lAlbum in Galleryctx.Albums
                    select lAlbum).Count();
        }

        public Album AddAlbum(int vAlbumID, string vAlbumName, string vAlbumDesc, DateTime vAlbumCreateDate,
                              int vParentAlbumID)
        {
            Album lAlbum = default(Album);

            if (vAlbumID > 0)
            {
                lAlbum = GetAlbumById(vAlbumID);

                lAlbum.AlbumID = vAlbumID;
                lAlbum.AlbumName = vAlbumName;
                lAlbum.AlbumDesc = vAlbumDesc;
                lAlbum.AlbumCreateDate = vAlbumCreateDate;
                lAlbum.ParentAlbumID = vParentAlbumID;

                lAlbum.DateUpdated = DateTime.Now;

                lAlbum.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lAlbum = Album.CreateAlbum(vAlbumID, vAlbumCreateDate, vParentAlbumID, true, DateTime.Now, DateTime.Now,
                                           Helpers.CurrentUserName, Helpers.CurrentUserName);

                lAlbum.AlbumName = vAlbumName;

                lAlbum.AlbumDesc = vAlbumDesc;
            }


            return AddAlbum(lAlbum);
        }

        public Album AddAlbum(Album vAlbum)
        {
            try
            {
                if (vAlbum.EntityState == EntityState.Detached)
                {
                    Galleryctx.AddToAlbums(vAlbum);
                }
                base.PurgeCacheItems(CacheKey);

                return Galleryctx.SaveChanges() > 0 ? vAlbum : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vAlbum.AlbumID.ToString(), ex);
                return null;
            }
        }

        public bool DeleteAlbum(int vAlbumId)
        {
            return ChangeDeletedState(GetAlbumById(vAlbumId), false);
        }

        public bool DeleteAlbum(Album vAlbum)
        {
            return ChangeDeletedState(vAlbum, false);
        }

        public bool UnDeleteAlbum(Album vAlbum)
        {
            return ChangeDeletedState(vAlbum, true);
        }


        private bool ChangeDeletedState(Album vAlbum, bool vState)
        {
            vAlbum.Active = vState;
            vAlbum.DateUpdated = DateTime.Now;
            vAlbum.UpdatedBy = CurrentUserName;

            try
            {
                Galleryctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vAlbum.AlbumID.ToString(), ex);

                return false;
            }
        }

        #endregion
    }
}