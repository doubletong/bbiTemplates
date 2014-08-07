using System.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBICMS;

namespace BBICMS.Forums
{

    public class PostsRepository : BaseForumRepository
    {

        #region " BLL/DAL Methods "

        public List<Post> GetPosts()
        {
            return (from lPost in Forumctx.Posts
                    select lPost).ToList();
        }

        public Post GetPostById(int PostId)
        {
            return (from lPost in Forumctx.Posts.Include("Forum")
                where lPost.PostID == PostId
                    select lPost).FirstOrDefault();
        }

        public List<Post> GetThread(int vThreadId)
        {
            return (from lPost in Forumctx.Posts.Include("Forum")
                where lPost.PostID == vThreadId || lPost.ParentPostID == vThreadId
                    select lPost).ToList();
        }

        public List<Post> GetUnapprovedPosts()
        {
            return (from lPost in Forumctx.Posts.Include("Forum")
                where lPost.Approved == false
                orderby lPost.AddedDate descending
            select lPost).ToList();
        }

        public Post GetPostByIdorThreadId(int PostId)
        {
            return (from lPost in Forumctx.Posts
                where lPost.PostID == PostId || lPost.ParentPostID == PostId
                    select lPost).FirstOrDefault();
        }

        public int GetPostCount()
        {
            return (from lPost in Forumctx.Posts
                    select lPost).Count();
        }

        public Post AddPost(int vPostID, System.DateTime vAddedDate, string vAddedByIP, int vForumID, int vParentPostID, 
            string vTitle, string vBody, bool vApproved, bool vSticky, bool vClosed,
        int vViewCount, int vReplyCount, string vLastPostBy, System.DateTime vLastPostDate, System.DateTime vUpdatedDate)
        {

            Post lPost = default(Post);

            if (vPostID > 0)
            {

                lPost = GetPostById(vPostID);

                lPost.PostID = vPostID;
                lPost.AddedDate = vAddedDate;
                lPost.AddedByIP = vAddedByIP;
                lPost.ForumId = vForumID;
                lPost.ParentPostID = vParentPostID;
                lPost.Title = vTitle;
                lPost.Body = vBody;
                lPost.Approved = vApproved;
                lPost.Sticky = vSticky;
                lPost.Closed = vClosed;
                lPost.ViewCount = vViewCount;
                lPost.ReplyCount = vReplyCount;
                lPost.LastPostBy = vLastPostBy;
                lPost.LastPostDate = vLastPostDate;
                lPost.UpdatedDate = vUpdatedDate;

                lPost.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lPost = Post.CreatePost(vPostID, vAddedDate, Helpers.CurrentUserName, vAddedByIP, 
                    vParentPostID, vTitle, vBody, vApproved, vSticky, vClosed, vViewCount,
                vReplyCount, vLastPostBy, vLastPostDate, vUpdatedDate, true);


                lPost.ForumId = vForumID;
            }


            return AddPost(lPost);
        }

        public Post AddPost(Post vPost)
        {

            try
            {
                if (vPost.EntityState == EntityState.Detached)
                {
                    Forumctx.AddToPosts(vPost);
                }
                base.PurgeCacheItems(CacheKey);

                return Forumctx.SaveChanges() > 0 ? vPost : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vPost.PostID, ex);
                return null;

            }
        }

        public bool IncrementViewCount(Post vPost)
        {
            vPost.ViewCount += 1;
            return (AddPost(vPost) != null) ? true : false;
        }


        public bool ApprovePost(int vPostId)
        {
            return ApprovePost(GetPostById(vPostId));
        }

        public bool ApprovePost(Post vPost)
        {
            vPost.Approved = true;
            return (AddPost(vPost) != null) ? true : false;
        }

        // Closes a thread
        public bool CloseThread(int threadPostID)
        {

            Post lthreadPost = GetPostById(threadPostID);
            bool ret = false;
            if ((lthreadPost != null))
            {
                lthreadPost.Closed = true;
                ret = (AddPost(lthreadPost) != null) ? true : false;
            }

            return ret;
        }

        public bool MoveThread(int threadPostID, int forumID)
        {

            Post lthreadPost = GetPostByIdorThreadId(threadPostID);
            bool ret = false;

            if ((lthreadPost != null))
            {
                lthreadPost.ForumId = forumID;
                ret = (AddPost(lthreadPost) != null) ? true : false;
            }

            return ret;
        }


        public List<Post> GetThreads(int vforumId)
        {
            return GetThreads(vforumId, false);
        }

        public List<Post> GetThreads(int vforumId, bool bIncludeInactive)
        {
            
            if (bIncludeInactive) {
                return (from lPost in Forumctx.Posts.Include("Forum")
                    where lPost.Forum.ForumID == vforumId && lPost.ParentPostID == 0
                    && lPost.Approved
                            select lPost).ToList();
            }
            else {
                return (from lPost in Forumctx.Posts.Include("Forum")
                        where lPost.Forum.ForumID == vforumId && lPost.ParentPostID == 0
                        && lPost.Approved && lPost.Active 
                        select lPost).ToList();
                
            }
        }


        public IEnumerable<Post> GetRSSForum(int vForumId)
        {
            
            string key = CacheKey + "_IEnumerable";
            
            //If EnableCaching AndAlso Not IsNothing(Cache(key)) Then
            // Return CType(Cache(key), IEnumerable(Of Article))
            //End If
            Forumctx.Posts.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            IEnumerable<Post> lPosts = default(IEnumerable<Post>);
            
            if (vForumId > 0) {
                
                    
                lPosts = (from lPost in Forumctx.Posts
                          orderby lPost.LastPostDate descending
                              select lPost).Take(10).AsEnumerable();
            }
            else {


                lPosts = (from lPost in Forumctx.Posts
                          where lPost.ForumId == vForumId
                          orderby lPost.LastPostDate descending
                          select lPost).Take(10).AsEnumerable();
            }
            
            //If EnableCaching Then
            // CacheData(key, lArticles)
            //End If
                
            return lPosts;
        }


        public int GetPostCountByThread(int threadPostID)
        {
            return GetThread(threadPostID).Count;
        }

        #region " Delete "

        public bool DeletePost(int PostId)
        {
            return DeletePost(GetPostById(PostId));
        }

        public bool DeletePost(Post vPost)
        {

            foreach (Post lpost in this.GetThread(vPost.PostID))
            {


                lpost.Active = false;
            }

            return ChangeDeletedState(vPost, false);
        }

        public bool UnDeletePost(Post vPost)
        {
            return ChangeDeletedState(vPost, true);
        }


        private bool ChangeDeletedState(Post vPost, bool vState)
        {
            vPost.Active = vState;
            vPost.UpdatedDate = DateTime.Now;
            vPost.UpdatedBy = CurrentUserName;

            try
            {
                Forumctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vPost.PostID.ToString(), ex);

                return false;

            }
        }

        #endregion

        #endregion

    }
}