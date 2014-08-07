namespace TheBeerHouse.BLL
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using TheBeerHouse;

    public class SiteMapRepository : BaseRepository
    {
        private SiteMapEntities _SiteMapctx;
        private bool disposedValue;
        private string key;
        private string tsnKey;

        public SiteMapRepository()
        {
            this.key = "SiteMapNodes";
            this.tsnKey = "TopSiteMapNodes";
            this.disposedValue = false;
            this.ConnectionString = TheBeerHouse.Globals.Settings.DefaultConnectionStringName;
            this.CacheKey = "SiteMap";
        }

        public SiteMapRepository(string sConnectionString)
        {
            this.key = "SiteMapNodes";
            this.tsnKey = "TopSiteMapNodes";
            this.disposedValue = false;
            this.ConnectionString = sConnectionString;
            this.CacheKey = "SiteMap";
        }

        [CompilerGenerated, DebuggerStepThrough]
        private static object _Lambda$__14(ObjectStateEntry e1)
        {
            return e1.Entity;
        }

        /// <summary>
        /// </summary>
        /// <param name="vSiteMap"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddSiteMap(SiteMapInfo vSiteMap)
        {
            bool AddSiteMap;
            try
            {
                vSiteMap.AddedBy = BaseRepository.CurrentUserName;
                vSiteMap.UpdatedBy = BaseRepository.CurrentUserName;
                this.SiteMapctx.AddToSiteMaps(vSiteMap);
                this.SiteMapctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                AddSiteMap = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vSiteMap.SiteMapId), ex);
                AddSiteMap = false;
                ProjectData.ClearProjectError();
                return AddSiteMap;
                ProjectData.ClearProjectError();
            }
            return AddSiteMap;
        }

        /// <summary>
        /// </summary>
        /// <param name="vSiteMap"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteSiteMap(SiteMapInfo vSiteMap)
        {
            bool DeleteSiteMap;
            try
            {
                this.SiteMapctx.DeleteObject(vSiteMap);
                this.SiteMapctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                DeleteSiteMap = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                DeleteSiteMap = false;
                ProjectData.ClearProjectError();
                return DeleteSiteMap;
                ProjectData.ClearProjectError();
            }
            return DeleteSiteMap;
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!this.disposedValue && disposing) && !Information.IsNothing(this._SiteMapctx))
            {
                this._SiteMapctx.Dispose();
            }
            this.disposedValue = true;
        }

        public List<SiteMapInfo> GetActiveSiteMapNodes()
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            string lActiveSiteMapKey = this.CacheKey + "_Active";
            if (((base.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[lActiveSiteMapKey]))) ? 1 : 0) != 0)
            {
                return (List<SiteMapInfo>) BaseRepository.Cache[lActiveSiteMapKey];
            }
            List<SiteMapInfo> lSiteMapNodes = this.SiteMapctx.SiteMaps.Where<SiteMapInfo>(Expression.Lambda<Func<SiteMapInfo, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(SiteMapInfo), "lSiteMapNode"), (MethodInfo) methodof(SiteMapInfo.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).OrderBy<SiteMapInfo, int>(Expression.Lambda<Func<SiteMapInfo, int>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(SiteMapInfo), "lSiteMapNode"), (MethodInfo) methodof(SiteMapInfo.get_SortOrder)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<SiteMapInfo>();
            if (base.EnableCaching)
            {
                BaseRepository.CacheData(lActiveSiteMapKey, lSiteMapNodes);
            }
            return lSiteMapNodes;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<SiteMapInfo> GetSiteMap()
        {
            ParameterExpression VB$t_ref$S0;
            return this.SiteMapctx.SiteMaps.Where<SiteMapInfo>(Expression.Lambda<Func<SiteMapInfo, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(SiteMapInfo), "lSiteMap"), (MethodInfo) methodof(SiteMapInfo.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<SiteMapInfo>();
        }

        /// <summary>
        /// </summary>
        /// <param name="SiteMapId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public SiteMapInfo GetSiteMapById(int SiteMapId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__28 $VB$Closure_ClosureVariable_84_C = new _Closure$__28();
            $VB$Closure_ClosureVariable_84_C.$VB$Local_SiteMapId = SiteMapId;
            return this.SiteMapctx.SiteMaps.Where<SiteMapInfo>(Expression.Lambda<Func<SiteMapInfo, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(SiteMapInfo), "lai"), (MethodInfo) methodof(SiteMapInfo.get_SiteMapId)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_84_C, typeof(_Closure$__28)), fieldof(_Closure$__28.$VB$Local_SiteMapId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).First<SiteMapInfo>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetSiteMapCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.SiteMapctx.SiteMaps.Select<SiteMapInfo, SiteMapInfo>(Expression.Lambda<Func<SiteMapInfo, SiteMapInfo>>(VB$t_ref$S0 = Expression.Parameter(typeof(SiteMapInfo), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<SiteMapInfo>();
        }

        /// <summary>
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public SiteMapInfo GetSiteMapInfoByURL(string URL)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__29 $VB$Closure_ClosureVariable_91_C = new _Closure$__29();
            $VB$Closure_ClosureVariable_91_C.$VB$Local_URL = URL;
            return this.SiteMapctx.SiteMaps.Where<SiteMapInfo>(Expression.Lambda<Func<SiteMapInfo, bool>>(Expression.Equal(Expression.Call(null, (MethodInfo) methodof(Microsoft.VisualBasic.CompilerServices.Operators.CompareString), new Expression[] { Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(SiteMapInfo), "lai"), (MethodInfo) methodof(SiteMapInfo.get_URL)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_91_C, typeof(_Closure$__29)), fieldof(_Closure$__29.$VB$Local_URL)), Expression.Constant(false, typeof(bool)) }), Expression.Constant(0, typeof(int))), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<SiteMapInfo>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<SiteMapInfo> GetSiteMapNodes()
        {
            List<SiteMapInfo> lSiteMapNodes;
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[this.key]))) ? 1 : 0) != 0)
            {
                lSiteMapNodes = (List<SiteMapInfo>) BaseRepository.Cache[this.key];
            }
            this.SiteMapctx.SiteMaps.MergeOption = MergeOption.NoTracking;
            lSiteMapNodes = this.SiteMapctx.SiteMaps.Where<SiteMapInfo>(Expression.Lambda<Func<SiteMapInfo, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(SiteMapInfo), "lSiteMapNode"), (MethodInfo) methodof(SiteMapInfo.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).OrderBy<SiteMapInfo, int>(Expression.Lambda<Func<SiteMapInfo, int>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(SiteMapInfo), "lSiteMapNode"), (MethodInfo) methodof(SiteMapInfo.get_SortOrder)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<SiteMapInfo>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(this.key, lSiteMapNodes);
            }
            return lSiteMapNodes;
        }

        public List<SiteMapInfo> GetTopSiteMapNodes()
        {
            List<SiteMapInfo> lSiteMapNodes;
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[this.tsnKey])))
            {
                lSiteMapNodes = (List<SiteMapInfo>) BaseRepository.Cache[this.tsnKey];
            }
            else
            {
                ParameterExpression VB$t_ref$S0;
                ParameterExpression VB$t_ref$S1;
                lSiteMapNodes = this.SiteMapctx.SiteMaps.Where<SiteMapInfo>(Expression.Lambda<Func<SiteMapInfo, bool>>(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(SiteMapInfo), "lSiteMapNode"), (MethodInfo) methodof(SiteMapInfo.get_Active)), Expression.Constant(true, typeof(bool)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(SiteMapInfo.get_Parent)), Expression.Constant(0, typeof(int)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).OrderBy<SiteMapInfo, int>(Expression.Lambda<Func<SiteMapInfo, int>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(SiteMapInfo), "lSiteMapNode"), (MethodInfo) methodof(SiteMapInfo.get_SortOrder)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<SiteMapInfo>();
            }
            BaseRepository.CacheData(this.tsnKey, lSiteMapNodes);
            return lSiteMapNodes;
        }

        /// <summary>
        /// </summary>
        /// <param name="vSiteMap"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateSiteMap(SiteMapInfo vSiteMap)
        {
            bool UpdateSiteMap;
            try
            {
                this.SiteMapctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                UpdateSiteMap = true;
            }
            catch (OptimisticConcurrencyException exception1)
            {
                ProjectData.SetProjectError(exception1);
                OptimisticConcurrencyException exOCE = exception1;
                IEnumerable<object> failedEntities = exOCE.StateEntries.Select<ObjectStateEntry, object>(new Func<ObjectStateEntry, object>(SiteMapRepository._Lambda$__14));
                this.SiteMapctx.Refresh(RefreshMode.ClientWins, (IEnumerable) failedEntities.ToList<object>());
                this.SiteMapctx.SaveChanges();
                ProjectData.ClearProjectError();
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                Exception ex = exception2;
                UpdateSiteMap = false;
                ProjectData.ClearProjectError();
                return UpdateSiteMap;
                ProjectData.ClearProjectError();
            }
            return UpdateSiteMap;
        }

        public SiteMapEntities SiteMapctx
        {
            get
            {
                if (Information.IsNothing(this._SiteMapctx))
                {
                    this._SiteMapctx = new SiteMapEntities(this.GetActualConnectionString());
                }
                return this._SiteMapctx;
            }
            set
            {
                this._SiteMapctx = value;
            }
        }

        [CompilerGenerated]
        internal class _Closure$__28
        {
            public int $VB$Local_SiteMapId;

            [DebuggerNonUserCode]
            public _Closure$__28()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__28(SiteMapRepository._Closure$__28 other)
            {
                if (other != null)
                {
                    this.$VB$Local_SiteMapId = other.$VB$Local_SiteMapId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__29
        {
            public string $VB$Local_URL;

            [DebuggerNonUserCode]
            public _Closure$__29()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__29(SiteMapRepository._Closure$__29 other)
            {
                if (other != null)
                {
                    this.$VB$Local_URL = other.$VB$Local_URL;
                }
            }
        }
    }
}

