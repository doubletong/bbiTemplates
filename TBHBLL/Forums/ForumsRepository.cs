using System.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBICMS;


namespace BBICMS.Forums
{

    public class ForumsRepository : BaseForumRepository
    {

        #region " BLL/DAL Methods "

        public List<Forum> GetForums()
        {
            
            string key = CacheKey + "_FullList";
            
            if (EnableCaching && (Cache[key] != null)) {
                return (List<Forum>)Cache[key];
            }
            
            List<Forum> lForums;
            Forumctx.Forums.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            
            lForums = (from lForum in Forumctx.Forums
                       select lForum).ToList();
            
            if (EnableCaching) {
                CacheData(key, lForums, CacheDuration);
            }
            
                
            return lForums;
        }

        public List<Forum> GetActiveForums()
        {
            Forumctx.Forums.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            return (from lForum in Forumctx.Forums.Include("Posts") 
                        where lForum.Active
                    select lForum).ToList();
        }

        public Forum GetForumById(int ForumId)
        {
            return (from lai in Forumctx.Forums
                where lai.ForumID == ForumId
                    select lai).FirstOrDefault();
        }

        public int GetForumCount()
        {
            return (from lai in Forumctx.Forums select lai).Count();
        }

        public Forum AddForum(int vForumID, System.DateTime vAddedDate, string vTitle, bool vModerated, int vImportance, string vDescription, string vImageUrl, System.DateTime vUpdatedDate)
        {

            Forum lForum = default(Forum);

            if (vForumID > 0)
            {

                lForum = GetForumById(vForumID);

                lForum.ForumID = vForumID;
                lForum.AddedDate = vAddedDate;
                lForum.Title = vTitle;
                lForum.Moderated = vModerated;
                lForum.Importance = vImportance;
                lForum.Description = vDescription;
                lForum.ImageUrl = vImageUrl;
                lForum.UpdatedDate = vUpdatedDate;


                lForum.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lForum = Forum.CreateForum(vForumID, vAddedDate, Helpers.CurrentUserName, vTitle, 
                    vModerated, vImportance, vUpdatedDate, true);
                lForum.Description = vDescription;

                lForum.ImageUrl = vImageUrl;
            }

            return AddForum(lForum);
        }

        public Forum AddForum(Forum vForum)
        {

            try
            {
                if (vForum.EntityState == EntityState.Detached)
                {
                    Forumctx.AddToForums(vForum);
                }
                base.PurgeCacheItems(CacheKey);

                return Forumctx.SaveChanges() > 0 ? vForum : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vForum.ForumID, ex);
                return null;

            }
        }

        public bool DeleteForum(Forum vForum)
        {
            return ChangeDeletedState(vForum, false);
        }

        public bool UnDeleteForum(Forum vForum)
        {
            return ChangeDeletedState(vForum, true);
        }


        private bool ChangeDeletedState(Forum vForum, bool vState)
        {
            vForum.Active = vState;
            vForum.UpdatedDate = DateTime.Now;
            vForum.UpdatedBy = CurrentUserName;

            try
            {
                Forumctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vForum.ForumID.ToString(), ex);

                return false;

            }
        }


        #endregion

    }
}