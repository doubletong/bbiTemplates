
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web.Caching;
using BBICMS.Articles;
using BBICMS.BLL.Store;

namespace BBICMS.BLL.Articles
{

    public class CommentRepository : BaseArticleRepository
    {

        public List<Comment> GetActiveComments()
        {
            string key = CacheKey + "_Active";

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Comment>)Cache[key];
            }

            Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
            List<Comment> lComments = (from lComment in Articlesctx.Comments //.Include("Category")
                                       where lComment.Active
                                       //orderby lComment.ReleaseDate descending
                                       select lComment).ToList();

            if (EnableCaching)
            {
                CacheData(key, lComments, CacheDuration);
            }

            return lComments;
        }

        public List<Comment> GetComments()
        {
            string key = CacheKey;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Comment>)Cache[key];
            }

            Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
            List<Comment> lComments = (from lComment in Articlesctx.Comments.Include("Article")
                    select lComment).ToList();

            if (EnableCaching)
            {
                CacheData(key, lComments, CacheDuration);
            }


            return lComments;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Comment GetCommentById(int CommentId)
        {
            return (from lComment in Articlesctx.Comments.Include("Article")
                    where lComment.CommentID == CommentId
                    select lComment).FirstOrDefault();

//            return GetCommentById(CommentId, false, false);
        }

        public Comment AddComment(int CommentID, DateTime AddedDate
                , String AddedBy, String AddedByEmail, String AddedByIP
                , int ArticleID, String Title, String Body, String CommenterURL
                , bool Approved, DateTime UpdatedDate, String UpdatedBy, bool Active)
        {

            Comment Comment;

            if (CommentID > 0)
            {

                Comment = GetCommentById(CommentID);

                Comment.CommentID = CommentID;
                Comment.AddedDate = AddedDate;
                Comment.AddedBy = AddedBy;
                Comment.AddedByEmail = AddedByEmail;
                Comment.AddedByIP = AddedByIP;
                Comment.ArticleId = ArticleID;
                Comment.Title = Title;
                Comment.Body = Body;
                Comment.CommenterURL = CommenterURL;
                Comment.Approved = Approved;
                Comment.UpdatedDate = UpdatedDate;
                Comment.UpdatedBy = UpdatedBy;
                Comment.Active = Active;

            }
            else
            {
                Comment = Comment.CreateComment(CommentID
                , AddedDate
                , AddedBy
                , AddedByEmail
                , AddedByIP
                , Body
                , Approved
                , UpdatedDate
                , Active
                );
            }

            return AddComment(Comment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vComment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Comment AddComment(Comment vComment)
        {

            try
            {
                if (vComment.EntityState == EntityState.Detached)
                {
                    Articlesctx.AddToComments(vComment);
                }
                base.PurgeCacheItems(CacheKey);

                return Articlesctx.SaveChanges() > 0 ? vComment : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vComment.CommentID, ex);
                return null;

            }
        }

        #region " Delete Operations "

        public bool DeleteComment(int vCommentId)
        {
            return ChangeDeletedState(this.GetCommentById(vCommentId), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vComment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteComment(Comment vComment)
        {
            return ChangeDeletedState(vComment, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vComment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeleteComment(Comment vComment)
        {
            return ChangeDeletedState(vComment, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vComment"></param>
        /// <param name="vState"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ChangeDeletedState(Comment vComment, bool vState)
        {
            vComment.Active = vState;
            vComment.UpdatedDate = DateTime.Now;
            vComment.UpdatedBy = CurrentUserName;

            try
            {
                Articlesctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vComment.CommentID.ToString(), ex);
                return false;

            }
        }

        #endregion

        #region Approved Comments

        public List<Comment> GetApprovedComments(int ArticleId)
        {
            return GetApprovedComments(ArticleId, true);
        }

        public List<Comment> GetApprovedComments(int ArticleId, bool bMostRecentFirst)
{
    
    string key = CacheKey + "_Comments_Approved_ArticleId_" + ArticleId + "_MostRecent_" + bMostRecentFirst;
    
    if (EnableCaching && (Cache[key] != null)) {
        return (List<Comment>)Cache[key];
    }
    
    List<Comment> lComments = (List<Comment>)Cache[key];
    
    if ((lComments == null)) {
        
        Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
        
        if (bMostRecentFirst) {
            lComments = (from lComment in Articlesctx.Comments.Include("Article")
                             where lComment.Approved == true && lComment.Article.ArticleID == ArticleId
                              orderby lComment.AddedDate ascending
                             select lComment).ToList();
        }
        else {
            lComments = (from lComment in Articlesctx.Comments.Include("Article")
                         where lComment.Approved == true && lComment.Article.ArticleID == ArticleId
                         orderby lComment.AddedDate descending 
                         select lComment).ToList();
            
        }
    }
    
    if (EnableCaching) {
        CacheData(key, lComments, CacheDuration);
    }
    
    return lComments;
}

        public List<Comment> GetApprovedComments()
{
    
    List<Comment> lComment = (List<Comment>)Cache["ApprovedComment"];
    
    if ((lComment == null)) {
        
        Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
        lComment = (from lC in Articlesctx.Comments.Include("Article")
                         where lC.Approved
                    select lC).ToList();

        Cache.Add("ApprovedComment", lComment, null, 
            DateTime.Now.AddMinutes(10), TimeSpan.FromMinutes(10), CacheItemPriority.Normal, null);
    }
    
#endregion
        
    return lComment;
}

    }
}
