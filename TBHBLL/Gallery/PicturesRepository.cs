    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;


namespace BBICMS.Gallery
{

    public class PicturesRepository : BaseGalleryRepository
    {

        public PicturesRepository()
        {

            CacheKey = CacheKey + "_Pictures";
        }

        #region " BLL/DAL Methods "

        public List<Picture> GetActivePicturesByAlbum(int vAlbumId)
    {
        
        string key = CacheKey + "_Albumid_" + vAlbumId;
        
        if (EnableCaching && (Cache[key] != null)) {
            return (List<Picture>)Cache[key];
        }
        
        List<Picture> lPictures = default(List<Picture>);
        Galleryctx.Pictures.MergeOption = System.Data.Objects.MergeOption.NoTracking;
        
        lPictures = (from lPicture in (from lAlbum in Galleryctx.Albums.Include("Pictures")
                         where lAlbum.AlbumID == vAlbumId
                                       select lAlbum).FirstOrDefault().Pictures
                         where lPicture.Active
                         orderby lPicture.AlbumOrder ascending, lPicture.PictureTitle ascending
                         select lPicture).ToList();
        
        if (EnableCaching) {
            CacheData(key, lPictures, CacheDuration);
        }
        
            
        return lPictures;
    }

        public List<Picture> GetPicturesByAlbum(int vAlbumId)
    {
        
        string key = CacheKey + "_Albumid_" + vAlbumId;
        
        if (EnableCaching && (Cache[key] != null)) {
            return (List<Picture>)Cache[key];
        }
        
        List<Picture> lPictures = default(List<Picture>);
        Galleryctx.Pictures.MergeOption = System.Data.Objects.MergeOption.NoTracking;
        
        lPictures = (from lPicture in (from lAlbum in Galleryctx.Albums.Include("Pictures")
                         where lAlbum.AlbumID == vAlbumId
                                           select lAlbum).FirstOrDefault().Pictures
                         orderby lPicture.AlbumOrder ascending, lPicture.PictureTitle ascending
                     select lPicture).ToList();
        
        if (EnableCaching) {
            CacheData(key, lPictures, CacheDuration);
        }
        
            
        return lPictures;
    }

        public List<Picture> GetPictures()
    {
        
        string key = CacheKey + "_FullList";
        
        if (EnableCaching && (Cache[key] != null)) {
            return (List<Picture>)Cache[key];
        }
        
        List<Picture> lPictures = default(List<Picture>);
        Galleryctx.Pictures.MergeOption = System.Data.Objects.MergeOption.NoTracking;
        
        lPictures = (from lPicture in Galleryctx.Pictures
                     select lPicture).ToList();
        
        if (EnableCaching) {
            CacheData(key, lPictures, CacheDuration);
        }
        
            
        return lPictures;
    }

        public Picture GetPictureById(int PictureId)
        {
            return GetPictureById(PictureId, true);
        }

        public Picture GetPictureById(int PictureId, bool bIncludeAlbum)
    {
        
        if (bIncludeAlbum) {
            return (from lai in Galleryctx.Pictures.Include("Album")
                    where lai.PictureID == PictureId
                        select lai).FirstOrDefault();
        }
        else {
            return (from lai in Galleryctx.Pictures
                    where lai.PictureID == PictureId
                    select lai).FirstOrDefault();
        }
    }

        public int GetPictureCount()
    {
        return (from lai in Galleryctx.Pictures
                select lai).Count();
    }

        public Picture AddPicture(int vPictureID, string vPictureTitle, string vPictureCaption, 
            System.DateTime vPictureCreateDate, string vPictureFileName, bool vPictureHighlight, int vPictureAlbumID, 
            int vPictureViewCount, int vAlbumOrder, int vThumbWidth,
        int vThumbHeight)
        {

            Picture lPicture = default(Picture);

            if (vPictureID > 0)
            {

                lPicture = GetPictureById(vPictureID);

                lPicture.PictureID = vPictureID;
                lPicture.PictureTitle = vPictureTitle;
                lPicture.PictureCaption = vPictureCaption;
                lPicture.PictureCreateDate = vPictureCreateDate;
                lPicture.PictureFileName = vPictureFileName;
                lPicture.PictureHighlight = vPictureHighlight;
                lPicture.AlbumId = vPictureAlbumID;
                lPicture.PictureViewCount = vPictureViewCount;
                lPicture.AlbumOrder = vAlbumOrder;
                lPicture.ThumbWidth = vThumbWidth;
                lPicture.ThumbHeight = vThumbHeight;

                lPicture.DateUpdated = DateTime.Now ;

                lPicture.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lPicture = Picture.CreatePicture(vPictureID, vPictureCreateDate, vPictureViewCount, vAlbumOrder, vThumbWidth, vThumbHeight,
                    true, DateTime.Now, Helpers.CurrentUserName, DateTime.Now, Helpers.CurrentUserName);

                lPicture.PictureTitle = vPictureTitle;
                lPicture.PictureCaption = vPictureCaption;
                lPicture.PictureFileName = vPictureFileName;

                lPicture.PictureHighlight = vPictureHighlight;
            }


            return AddPicture(lPicture);
        }

        public Picture AddPicture(Picture vPicture)
        {

            try
            {
                if (vPicture.EntityState == EntityState.Detached)
                {
                    Galleryctx.AddToPictures(vPicture);
                }
                base.PurgeCacheItems(CacheKey);

                return Galleryctx.SaveChanges() > 0 ? vPicture : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vPicture.PictureID, ex);
                return null;

            }
        }


        public bool UpdatePicOrder(int PictureId, int AlbumOrder)
        {

            Picture vPicture = this.GetPictureById(PictureId);
            vPicture.AlbumOrder = AlbumOrder;

            return ((AddPicture(vPicture) != null) ? true : false);
        }

        #region " Delete Members "

        public bool DeletePicture(int vPictureId)
        {
            return ChangeDeletedState(this.GetPictureById(vPictureId), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vPicture"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeletePicture(Picture vPicture)
        {
            return ChangeDeletedState(vPicture, false);
        }

        public bool UnDeletePicture(int vPictureId)
        {
            return ChangeDeletedState(this.GetPictureById(vPictureId), true);
        }

        public bool UnDeletePicture(Picture vPicture)
        {
            return ChangeDeletedState(vPicture, true);
        }

        private bool ChangeDeletedState(Picture vPicture, bool vState)
        {
            vPicture.Active = vState;
            vPicture.DateUpdated = DateTime.Now;
            vPicture.UpdatedBy = CurrentUserName;

            try
            {
                Galleryctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vPicture.PictureID.ToString(), ex);

                return false;

            }
        }

        #endregion

        #endregion

    }


}

