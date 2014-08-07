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
    public class AlbumRepository : BaseGalleryRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vAlbum"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddAlbum(Album vAlbum)
        {
            bool AddAlbum;
            try
            {
                if (vAlbum.EntityState == EntityState.Detached)
                {
                    this.Galleryctx.AddToAlbums(vAlbum);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddAlbum = this.Galleryctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vAlbum.AlbumID), ex);
                AddAlbum = false;
                ProjectData.ClearProjectError();
                return AddAlbum;
                ProjectData.ClearProjectError();
            }
            return AddAlbum;
        }

        private bool ChangeDeletedState(Album vAlbum, bool vState)
        {
            bool ChangeDeletedState;
            vAlbum.Active = vState;
            vAlbum.DateUpdated = DateAndTime.Now;
            vAlbum.UpdatedBy = BaseRepository.CurrentUserName;
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
                this.ActiveExceptions.Add(Conversions.ToString(vAlbum.AlbumID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        public bool DeleteAlbum(int vAlbumId)
        {
            return this.ChangeDeletedState(this.GetAlbumById(vAlbumId), false);
        }

        /// <summary>
        /// </summary>
        /// <param name="vAlbum"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteAlbum(Album vAlbum)
        {
            return this.ChangeDeletedState(vAlbum, false);
        }

        public List<Album> GetActiveAlbums()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_Active_List";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Album>) BaseRepository.Cache[key];
            }
            this.Galleryctx.Pictures.MergeOption = MergeOption.NoTracking;
            List<Album> lAlbums = this.Galleryctx.Albums.Include("Pictures").Where<Album>(Expression.Lambda<Func<Album, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lAlbum"), (MethodInfo) methodof(Album.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Album>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lAlbums);
            }
            return lAlbums;
        }

        public List<Album> GetActiveChildAlbums(int AlbumId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__24 $VB$Closure_ClosureVariable_59_C = new _Closure$__24();
            $VB$Closure_ClosureVariable_59_C.$VB$Local_AlbumId = AlbumId;
            return this.Galleryctx.Albums.Where<Album>(Expression.Lambda<Func<Album, bool>>(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lAlbum"), (MethodInfo) methodof(Album.get_ParentAlbumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_59_C, typeof(_Closure$__24)), fieldof(_Closure$__24.$VB$Local_AlbumId)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Album.get_Active)), Expression.Constant(true, typeof(bool)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Album>();
        }

        /// <summary>
        /// </summary>
        /// <param name="AlbumId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Album GetAlbumById(int AlbumId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__22 $VB$Closure_ClosureVariable_4B_C = new _Closure$__22();
            $VB$Closure_ClosureVariable_4B_C.$VB$Local_AlbumId = AlbumId;
            return this.Galleryctx.Albums.Where<Album>(Expression.Lambda<Func<Album, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lai"), (MethodInfo) methodof(Album.get_AlbumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_4B_C, typeof(_Closure$__22)), fieldof(_Closure$__22.$VB$Local_AlbumId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Album>();
        }

        public Album GetAlbumByIdWithPictures(int AlbumId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__23 $VB$Closure_ClosureVariable_52_C = new _Closure$__23();
            $VB$Closure_ClosureVariable_52_C.$VB$Local_AlbumId = AlbumId;
            return this.Galleryctx.Albums.Include("Pictures").Where<Album>(Expression.Lambda<Func<Album, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lai"), (MethodInfo) methodof(Album.get_AlbumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_52_C, typeof(_Closure$__23)), fieldof(_Closure$__23.$VB$Local_AlbumId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Album>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetAlbumCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Galleryctx.Albums.Select<Album, Album>(Expression.Lambda<Func<Album, Album>>(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Album>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Album> GetAlbums()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_FullList";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Album>) BaseRepository.Cache[key];
            }
            this.Galleryctx.Pictures.MergeOption = MergeOption.NoTracking;
            List<Album> lAlbums = this.Galleryctx.Albums.Include("Pictures").Select<Album, Album>(Expression.Lambda<Func<Album, Album>>(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lAlbum"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Album>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lAlbums);
            }
            return lAlbums;
        }

        public List<Album> GetChildAlbums(int AlbumId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__21 $VB$Closure_ClosureVariable_3F_C = new _Closure$__21();
            $VB$Closure_ClosureVariable_3F_C.$VB$Local_AlbumId = AlbumId;
            return this.Galleryctx.Albums.Where<Album>(Expression.Lambda<Func<Album, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Album), "lAlbum"), (MethodInfo) methodof(Album.get_ParentAlbumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_3F_C, typeof(_Closure$__21)), fieldof(_Closure$__21.$VB$Local_AlbumId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Album>();
        }

        public bool UnDeleteAlbum(Album vAlbum)
        {
            return this.ChangeDeletedState(vAlbum, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vAlbum"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateAlbum(Album vAlbum)
        {
            return this.AddAlbum(vAlbum);
        }

        [CompilerGenerated]
        internal class _Closure$__21
        {
            public int $VB$Local_AlbumId;

            [DebuggerNonUserCode]
            public _Closure$__21()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__21(AlbumRepository._Closure$__21 other)
            {
                if (other != null)
                {
                    this.$VB$Local_AlbumId = other.$VB$Local_AlbumId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__22
        {
            public int $VB$Local_AlbumId;

            [DebuggerNonUserCode]
            public _Closure$__22()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__22(AlbumRepository._Closure$__22 other)
            {
                if (other != null)
                {
                    this.$VB$Local_AlbumId = other.$VB$Local_AlbumId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__23
        {
            public int $VB$Local_AlbumId;

            [DebuggerNonUserCode]
            public _Closure$__23()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__23(AlbumRepository._Closure$__23 other)
            {
                if (other != null)
                {
                    this.$VB$Local_AlbumId = other.$VB$Local_AlbumId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__24
        {
            public int $VB$Local_AlbumId;

            [DebuggerNonUserCode]
            public _Closure$__24()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__24(AlbumRepository._Closure$__24 other)
            {
                if (other != null)
                {
                    this.$VB$Local_AlbumId = other.$VB$Local_AlbumId;
                }
            }
        }
    }
}

