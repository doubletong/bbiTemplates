using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using BBICMS;
using BBICMS.BLL;

namespace BLL
{
    public class SiteMapRepository : BaseRepository
    {
        private SiteMapEntities _SiteMapctx;
        private string key = "SiteMapNodes";
        private string tsnKey = "TopSiteMapNodes";

        public SiteMapEntities SiteMapctx
        {
            get
            {
                if ((_SiteMapctx == null))
                {
                    _SiteMapctx = new SiteMapEntities(GetActualConnectionString());
                }

                return _SiteMapctx;
            }
            set { _SiteMapctx = value; }
        }

        #region " Constructors "

        public SiteMapRepository(string sConnectionString)
        {
            ConnectionString = sConnectionString;
            CacheKey = "SiteMap";
        }

        public SiteMapRepository()
        {
            ConnectionString = Globals.Settings.DefaultConnectionStringName;
            CacheKey = "SiteMap";
        }

        #endregion

        #region " BLL/DAL Methods "

        public List<SiteMapInfo> GetSiteMapNodes()
        {
            List<SiteMapInfo> lSiteMapNodes = default(List<SiteMapInfo>);

            if (EnableCaching && (Cache[key] != null))
            {
                lSiteMapNodes = (List<SiteMapInfo>) Cache[key];
            }

            SiteMapctx.SiteMapInfos.MergeOption = MergeOption.NoTracking;
            lSiteMapNodes = (from lSiteMapNode in SiteMapctx.SiteMapInfos
                             where lSiteMapNode.Active
                             orderby lSiteMapNode.SortOrder
                             select lSiteMapNode).ToList();

            if (EnableCaching)
            {
                CacheData(key, lSiteMapNodes, CacheDuration);
            }


            return lSiteMapNodes;
        }

        public List<SiteMapInfo> GetActiveSiteMapNodes()
        {
            List<SiteMapInfo> lSiteMapNodes = default(List<SiteMapInfo>);
            string lActiveSiteMapKey = CacheKey + "_Active";

            if (base.EnableCaching && (Cache[lActiveSiteMapKey] != null))
            {
                lSiteMapNodes = (List<SiteMapInfo>) Cache[lActiveSiteMapKey];
            }
            else
            {
                lSiteMapNodes = (from lSiteMapNode in SiteMapctx.SiteMapInfos
                                 where lSiteMapNode.Active
                                 orderby lSiteMapNode.SortOrder
                                 select lSiteMapNode).ToList();

                if (base.EnableCaching)
                {
                    CacheData(lActiveSiteMapKey, lSiteMapNodes, CacheDuration);
                }
            }


            return lSiteMapNodes;
        }

        public List<SiteMapInfo> GetTopSiteMapNodes()
        {
            List<SiteMapInfo> lSiteMapNodes = default(List<SiteMapInfo>);

            if ((Cache[tsnKey] != null))
            {
                lSiteMapNodes = (List<SiteMapInfo>) Cache[tsnKey];
            }
            else
            {
                lSiteMapNodes = (from lSiteMapNode in SiteMapctx.SiteMapInfos
                                 where lSiteMapNode.Active && lSiteMapNode.Parent == 0
                                 orderby lSiteMapNode.SortOrder
                                 select lSiteMapNode).ToList();
            }

            CacheData(tsnKey, lSiteMapNodes, CacheDuration);


            return lSiteMapNodes;
        }

        public List<SiteMapInfo> GetSiteMap()
        {
            return (from lSiteMapNode in SiteMapctx.SiteMapInfos
                    where lSiteMapNode.Active
                    select lSiteMapNode).ToList();
        }

        public SiteMapInfo GetSiteMapById(int SiteMapId)
        {
            return (from lSiteMapNode in SiteMapctx.SiteMapInfos
                    where lSiteMapNode.SiteMapId == SiteMapId
                    select lSiteMapNode).FirstOrDefault();
        }

        public SiteMapInfo GetSiteMapInfoByURL(string URL)
        {
            SiteMapInfo lsmi = (from lSiteMapNode in SiteMapctx.SiteMapInfos
                                where lSiteMapNode.URL == URL
                                select lSiteMapNode).FirstOrDefault();

            return lsmi;
        }

        public SiteMapInfo GetSiteMapInfoByRealURL(string URL)
        {
            SiteMapInfo lsmi = (from lSiteMapNode in SiteMapctx.SiteMapInfos
                                where lSiteMapNode.RealURL == URL
                                select lSiteMapNode).FirstOrDefault();


            return lsmi;
        }

        public int GetSiteMapCount()
        {
            return (from lSiteMapNode in SiteMapctx.SiteMapInfos
                    select lSiteMapNode).Count();
        }

        public SiteMapInfo AddSiteMap(SiteMapInfo vSiteMap)
        {
            try
            {
                if (vSiteMap.EntityState == EntityState.Detached)
                {
                    SiteMapctx.AddToSiteMapInfos(vSiteMap);
                }
                base.PurgeCacheItems(CacheKey);

                return SiteMapctx.SaveChanges() > 0 ? vSiteMap : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vSiteMap.SiteMapId, ex);
                return null;
            }
        }

        public bool DeleteSiteMap(SiteMapInfo vSiteMap)
        {
            try
            {
                SiteMapctx.DeleteObject(vSiteMap);
                SiteMapctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region " Dispose "

        private bool disposedValue = false;
        // To detect redundant calls

        // IDisposable
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if ((_SiteMapctx == null) == false)
                    {
                        _SiteMapctx.Dispose();
                    }
                }
            }
            disposedValue = true;
        }

        #region " IDisposable Support "

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion
    }
}