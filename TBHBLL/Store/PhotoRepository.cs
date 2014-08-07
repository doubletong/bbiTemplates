using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.IO;
using System.Web;


namespace BBICMS.Store
{
    public class PhotoRepository : BaseShoppingCartRepository
    {
        #region " BLL/DAL Methods "


        public List<Photo> GetPhotos()
        {
            string key = CacheKey + "_Photos_All";

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Photo>)Cache[key];
            }

            List<Photo> lPhotos = default(List<Photo>);
            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;

            lPhotos = (from lPhoto in Shoppingctx.Photos
                         orderby lPhoto.AddedDate descending
                         select lPhoto).ToList();


            if (EnableCaching)
            {
                CacheData(key, lPhotos, CacheDuration);
            }


            return lPhotos;
        }


        public List<Photo> GetPhotosByProduct(int vProductID)
        {
            string key = CacheKey + "_Photos_List_" + vProductID.ToString();

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Photo>)Cache[key];
            }

            List<Photo> lPhotos = default(List<Photo>);
            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;

            lPhotos =(from lPhoto in 
                           (from lProduct in Shoppingctx.Products.Include("Photos")
                            where lProduct.ProductID == vProductID
                            select lProduct).FirstOrDefault().Photos
                            orderby lPhoto.AddedDate descending
                         select lPhoto).ToList();         


            if (EnableCaching)
            {
                CacheData(key, lPhotos, CacheDuration);
            }

            return lPhotos;
        }




        public Photo GetPhotoById(int vPhotoID)
        {
            return (from lPhoto in Shoppingctx.Photos
                    where lPhoto.PhotoID == vPhotoID
                    select lPhoto).FirstOrDefault();
        }

        public Photo GetPhotoByIdWithProducts(int vPhotoID)
        {
            return (from lPhoto in Shoppingctx.Photos.Include("Product")
                    where lPhoto.PhotoID == vPhotoID
                    select lPhoto).FirstOrDefault();
        }





        public Photo AddPhoto(int vPhotoID, string vThumbnail, string vOriginalPic, int vProductId, bool vActive)
        {
            Photo lPhoto = default(Photo);

            if (vPhotoID > 0)
            {
                lPhoto = GetPhotoById(vPhotoID);

                lPhoto.PhotoID = vPhotoID;
                lPhoto.Thumbnail = vThumbnail;
                lPhoto.OriginalPic = vOriginalPic;
               
                lPhoto.ProductID = vProductId;
                lPhoto.AddedBy = Helpers.CurrentUserName;
                lPhoto.AddedDate = DateTime.Now;
            }
            else
            {
                lPhoto = Photo.CreatePhoto(vPhotoID, vProductId, Helpers.CurrentUserName,
                    DateTime.Now, vThumbnail, vOriginalPic);

                            
            }


            return AddPhoto(lPhoto);
        }

        public Photo AddPhoto(Photo vPhoto)
        {
            try
            {
                if (vPhoto.EntityState == EntityState.Detached)
                {
                    Shoppingctx.AddToPhotos(vPhoto);
                }
                base.PurgeCacheItems(CacheKey);

                return Shoppingctx.SaveChanges() > 0 ? vPhoto : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vPhoto.PhotoID.ToString(), ex);
                return null;
            }
        }

        public bool DeletePhoto(int vPhotoId)
        {
            return RemovePhoto(GetPhotoById(vPhotoId));
        }

        public bool DeletePhoto(Photo vPhoto)
        {
            return RemovePhoto(vPhoto);
        }

      

        public bool RemovePhoto(Photo vPhoto)
        {      
            try
            {
                StoreHelper.DeletePhoto(vPhoto.OriginalPic);//É¾³ý´óÍ¼
                StoreHelper.DeletePhotoThumb(vPhoto.Thumbnail);//É¾³ýÐ¡Í¼                

                Shoppingctx.DeleteObject(vPhoto);
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vPhoto.PhotoID.ToString(), ex);

                return false;

            }
        }


        #endregion
    }
}