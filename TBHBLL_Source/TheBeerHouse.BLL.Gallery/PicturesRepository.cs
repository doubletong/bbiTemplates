namespace TheBeerHouse.BLL.Gallery
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using TheBeerHouse.BLL;

    /// <summary>
    /// The Entity Repository Class. Contains methods that utilize the Entity Framework to 
    /// interact with the database.
    /// </summary>
    /// <remarks></remarks>
    public class PicturesRepository : BaseGalleryRepository
    {
        public PicturesRepository()
        {
            this.CacheKey = this.CacheKey + "_Pictures";
        }

        [DebuggerStepThrough, CompilerGenerated]
        private static string _Lambda$__10(Picture lPicture)
        {
            return lPicture.PictureTitle;
        }

        [DebuggerStepThrough, CompilerGenerated]
        private static int _Lambda$__11(Picture lPicture)
        {
            return lPicture.AlbumOrder;
        }

        [DebuggerStepThrough, CompilerGenerated]
        private static string _Lambda$__12(Picture lPicture)
        {
            return lPicture.PictureTitle;
        }

        [CompilerGenerated, DebuggerStepThrough]
        private static bool _Lambda$__8(Picture lPicture)
        {
            return lPicture.Active;
        }

        [CompilerGenerated, DebuggerStepThrough]
        private static int _Lambda$__9(Picture lPicture)
        {
            return lPicture.AlbumOrder;
        }

        /// <summary>
        /// </summary>
        /// <param name="vPicture"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddPicture(Picture vPicture)
        {
            bool AddPicture;
            try
            {
                if (vPicture.EntityState == EntityState.Detached)
                {
                    this.Galleryctx.AddToPictures(vPicture);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddPicture = this.Galleryctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vPicture.PictureID), ex);
                AddPicture = false;
                ProjectData.ClearProjectError();
                return AddPicture;
                ProjectData.ClearProjectError();
            }
            return AddPicture;
        }

        private bool ChangeDeletedState(Picture vPicture, bool vState)
        {
            bool ChangeDeletedState;
            vPicture.Active = vState;
            vPicture.DateUpdated = DateAndTime.Now;
            vPicture.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Galleryctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vPicture.PictureID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        public bool DeletePicture(int vPictureId)
        {
            return this.ChangeDeletedState(this.GetPictureById(vPictureId), false);
        }

        /// <summary>
        /// </summary>
        /// <param name="vPicture"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeletePicture(Picture vPicture)
        {
            return this.ChangeDeletedState(vPicture, false);
        }

        public List<Picture> GetActivePicturesByAlbum(int vAlbumId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__25 $VB$Closure_ClosureVariable_14_C = new _Closure$__25();
            $VB$Closure_ClosureVariable_14_C.$VB$Local_vAlbumId = vAlbumId;
            string key = this.CacheKey + "_Albumid_" + Conversions.ToString($VB$Closure_ClosureVariable_14_C.$VB$Local_vAlbumId);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Picture>) BaseRepository.Cache[key];
            }
            this.Galleryctx.Pictures.MergeOption = MergeOption.NoTracking;
            List<Picture> lPictures = this.Galleryctx.Albums.Include("Pictures").Where<Album>(Expression.Lambda<Func<Album, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lAlbum"), (MethodInfo) methodof(Album.get_AlbumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_14_C, typeof(_Closure$__25)), fieldof(_Closure$__25.$VB$Local_vAlbumId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Album>().Pictures.Where<Picture>(new Func<Picture, bool>(PicturesRepository._Lambda$__8)).OrderBy<Picture, int>(new Func<Picture, int>(PicturesRepository._Lambda$__9)).ThenBy<Picture, string>(new Func<Picture, string>(PicturesRepository._Lambda$__10)).ToList<Picture>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lPictures);
            }
            return lPictures;
        }

        public Picture GetPictureById(int PictureId)
        {
            return this.GetPictureById(PictureId, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="PictureId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Picture GetPictureById(int PictureId, bool bIncludeAlbum)
        {
            ParameterExpression VB$t_ref$S1;
            _Closure$__27 $VB$Closure_ClosureVariable_67_C = new _Closure$__27();
            $VB$Closure_ClosureVariable_67_C.$VB$Local_PictureId = PictureId;
            if (bIncludeAlbum)
            {
                ParameterExpression VB$t_ref$S0;
                return this.Galleryctx.Pictures.Include("Album").Where<Picture>(Expression.Lambda<Func<Picture, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Picture), "lai"), (MethodInfo) methodof(Picture.get_PictureID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_67_C, typeof(_Closure$__27)), fieldof(_Closure$__27.$VB$Local_PictureId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Picture>();
            }
            return this.Galleryctx.Pictures.Where<Picture>(Expression.Lambda<Func<Picture, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Picture), "lai"), (MethodInfo) methodof(Picture.get_PictureID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_67_C, typeof(_Closure$__27)), fieldof(_Closure$__27.$VB$Local_PictureId)), true, null), new ParameterExpression[] { VB$t_ref$S1 })).FirstOrDefault<Picture>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPictureCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Galleryctx.Pictures.Select<Picture, Picture>(Expression.Lambda<Func<Picture, Picture>>(VB$t_ref$S0 = Expression.Parameter(typeof(Picture), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Picture>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Picture> GetPictures()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_FullList";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Picture>) BaseRepository.Cache[key];
            }
            this.Galleryctx.Pictures.MergeOption = MergeOption.NoTracking;
            List<Picture> lPictures = this.Galleryctx.Pictures.Select<Picture, Picture>(Expression.Lambda<Func<Picture, Picture>>(VB$t_ref$S0 = Expression.Parameter(typeof(Picture), "lPicture"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Picture>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lPictures);
            }
            return lPictures;
        }

        public List<Picture> GetPicturesByAlbum(int vAlbumId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__26 $VB$Closure_ClosureVariable_2C_C = new _Closure$__26();
            $VB$Closure_ClosureVariable_2C_C.$VB$Local_vAlbumId = vAlbumId;
            string key = this.CacheKey + "_Albumid_" + Conversions.ToString($VB$Closure_ClosureVariable_2C_C.$VB$Local_vAlbumId);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Picture>) BaseRepository.Cache[key];
            }
            this.Galleryctx.Pictures.MergeOption = MergeOption.NoTracking;
            List<Picture> lPictures = this.Galleryctx.Albums.Include("Pictures").Where<Album>(Expression.Lambda<Func<Album, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lAlbum"), (MethodInfo) methodof(Album.get_AlbumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_2C_C, typeof(_Closure$__26)), fieldof(_Closure$__26.$VB$Local_vAlbumId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Album>().Pictures.OrderBy<Picture, int>(new Func<Picture, int>(PicturesRepository._Lambda$__11)).ThenBy<Picture, string>(new Func<Picture, string>(PicturesRepository._Lambda$__12)).ToList<Picture>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lPictures);
            }
            return lPictures;
        }

        public bool UnDeletePicture(int vPictureId)
        {
            return this.ChangeDeletedState(this.GetPictureById(vPictureId), true);
        }

        public bool UnDeletePicture(Picture vPicture)
        {
            return this.ChangeDeletedState(vPicture, true);
        }

        public bool UpdatePicOrder(int PictureId, int AlbumOrder)
        {
            Picture vPicture = this.GetPictureById(PictureId);
            vPicture.AlbumOrder = AlbumOrder;
            return this.AddPicture(vPicture);
        }

        /// <summary>
        /// </summary>
        /// <param name="vPicture"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdatePicture(Picture vPicture)
        {
            return this.AddPicture(vPicture);
        }

        [CompilerGenerated]
        internal class _Closure$__25
        {
            public int $VB$Local_vAlbumId;

            [DebuggerNonUserCode]
            public _Closure$__25()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__25(PicturesRepository._Closure$__25 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vAlbumId = other.$VB$Local_vAlbumId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__26
        {
            public int $VB$Local_vAlbumId;

            [DebuggerNonUserCode]
            public _Closure$__26()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__26(PicturesRepository._Closure$__26 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vAlbumId = other.$VB$Local_vAlbumId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__27
        {
            public int $VB$Local_PictureId;

            [DebuggerNonUserCode]
            public _Closure$__27()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__27(PicturesRepository._Closure$__27 other)
            {
                if (other != null)
                {
                    this.$VB$Local_PictureId = other.$VB$Local_PictureId;
                }
            }
        }
    }
}

