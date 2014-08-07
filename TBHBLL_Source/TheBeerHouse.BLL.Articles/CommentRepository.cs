namespace TheBeerHouse.BLL.Articles
{
    using Joel.Net;
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
    using System.Web.Caching;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    public class CommentRepository : BaseArticleRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vComment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddComment(Comment vComment)
        {
            bool AddComment;
            try
            {
                vComment.AddedDate = DateAndTime.Now;
                vComment.UpdatedDate = DateAndTime.Now;
                if (Helpers.Settings.Articles.EnableAkismet)
                {
                    Akismet lAkismet = new Akismet(Helpers.Settings.Articles.AkismetKey, "http://beerhouse.extremewebworks.com", "TheBeerHouse | Akismet/1.11");
                    if (lAkismet.CommentCheck(vComment.GetAkismetComment()))
                    {
                        vComment.Active = false;
                    }
                }
                if (vComment.EntityState == EntityState.Detached)
                {
                    this.Articlesctx.AddToComments(vComment);
                }
                return (this.Articlesctx.SaveChanges() > 0);
                AddComment = false;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vComment.CommentID), ex);
                AddComment = false;
                ProjectData.ClearProjectError();
                return AddComment;
                ProjectData.ClearProjectError();
            }
            return AddComment;
        }

        /// <summary>
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ApproveComment(int CommentId)
        {
            bool ApproveComment;
            this.ApproveComment(this.GetCommentById(CommentId));
            return ApproveComment;
        }

        public bool ApproveComment(Comment lComment)
        {
            bool ApproveComment;
            lComment.Approved = true;
            lComment.UpdatedDate = DateAndTime.Now;
            lComment.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Articlesctx.SaveChanges();
                ApproveComment = true;
            }
            catch (OptimisticConcurrencyException exception1)
            {
                ProjectData.SetProjectError(exception1);
                OptimisticConcurrencyException ocEx = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(lComment.CommentID), ocEx);
                ApproveComment = false;
                ProjectData.ClearProjectError();
                return ApproveComment;
                ProjectData.ClearProjectError();
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                Exception Ex = exception2;
                this.ActiveExceptions.Add(Conversions.ToString(lComment.CommentID), Ex);
                ApproveComment = false;
                ProjectData.ClearProjectError();
                return ApproveComment;
                ProjectData.ClearProjectError();
            }
            return ApproveComment;
        }

        private bool ChangeDeletedState(Comment vComment, bool vState)
        {
            bool ChangeDeletedState;
            vComment.Active = vState;
            vComment.UpdatedDate = DateAndTime.Now;
            vComment.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Articlesctx.SaveChanges();
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vComment.CommentID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        public bool DeleteComment(int vCommentId)
        {
            bool DeleteComment;
            this.DeleteComment(this.GetCommentById(vCommentId));
            return DeleteComment;
        }

        public bool DeleteComment(Comment vComment)
        {
            if (Helpers.Settings.Articles.EnableAkismet & Helpers.Settings.Articles.ReportAkismet)
            {
                new Akismet(Helpers.Settings.Articles.AkismetKey, "http://beerhouse.extremewebworks.com", "TheBeerHouse | Akismet/1.11").SubmitSpam(vComment.GetAkismetComment());
            }
            return this.ChangeDeletedState(vComment, false);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Comment> GetApprovedComments()
        {
            List<Comment> lComment = (List<Comment>) BaseRepository.Cache["ApprovedComment"];
            if (Information.IsNothing(lComment))
            {
                TimeSpan VB$t_struct$N0;
                ParameterExpression VB$t_ref$S0;
                this.Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
                lComment = this.Articlesctx.Comments.Include("Article").Where<Comment>(Expression.Lambda<Func<Comment, bool>>(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Comment), "lC"), (MethodInfo) methodof(Comment.get_Approved)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Comment>();
                BaseRepository.Cache.Add("ApprovedComment", lComment, null, DateAndTime.DateAdd(DateInterval.Minute, 10.0, DateAndTime.Now), VB$t_struct$N0, CacheItemPriority.Normal, null);
            }
            return lComment;
        }

        public List<Comment> GetApprovedComments(int ArticleId)
        {
            return this.GetApprovedComments(ArticleId, true);
        }

        public List<Comment> GetApprovedComments(int ArticleId, bool bMostRecentFirst)
        {
            _Closure$__8 $VB$Closure_ClosureVariable_7D_C = new _Closure$__8();
            $VB$Closure_ClosureVariable_7D_C.$VB$Local_ArticleId = ArticleId;
            string key = this.CacheKey + "_Comments_Approved_ArticleId_" + Conversions.ToString($VB$Closure_ClosureVariable_7D_C.$VB$Local_ArticleId) + "_MostRecent_" + Conversions.ToString(bMostRecentFirst);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Comment>) BaseRepository.Cache[key];
            }
            List<Comment> lComments = (List<Comment>) BaseRepository.Cache[key];
            if (Information.IsNothing(lComments))
            {
                this.Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
                if (bMostRecentFirst)
                {
                    ParameterExpression VB$t_ref$S0;
                    ParameterExpression VB$t_ref$S1;
                    lComments = this.Articlesctx.Comments.Where<Comment>(Expression.Lambda<Func<Comment, bool>>(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Comment), "lComment"), (MethodInfo) methodof(Comment.get_Approved)), Expression.Constant(true, typeof(bool)), true, null), Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Comment.get_Article)), (MethodInfo) methodof(Article.get_ArticleID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_7D_C, typeof(_Closure$__8)), fieldof(_Closure$__8.$VB$Local_ArticleId)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).OrderBy<Comment, DateTime>(Expression.Lambda<Func<Comment, DateTime>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Comment), "lComment"), (MethodInfo) methodof(Comment.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Comment>();
                }
                else
                {
                    ParameterExpression VB$t_ref$S2;
                    ParameterExpression VB$t_ref$S3;
                    lComments = this.Articlesctx.Comments.Where<Comment>(Expression.Lambda<Func<Comment, bool>>(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(Comment), "lC"), (MethodInfo) methodof(Comment.get_Approved)), Expression.Constant(true, typeof(bool)), true, null), Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S2, (MethodInfo) methodof(Comment.get_Article)), (MethodInfo) methodof(Article.get_ArticleID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_7D_C, typeof(_Closure$__8)), fieldof(_Closure$__8.$VB$Local_ArticleId)), true, null)), new ParameterExpression[] { VB$t_ref$S2 })).OrderByDescending<Comment, DateTime>(Expression.Lambda<Func<Comment, DateTime>>(Expression.Property(VB$t_ref$S3 = Expression.Parameter(typeof(Comment), "lC"), (MethodInfo) methodof(Comment.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S3 })).ToList<Comment>();
                }
            }
            BaseRepository.CacheData(key, lComments);
            return lComments;
        }

        /// <summary>
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Comment GetCommentById(int CommentId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__9 $VB$Closure_ClosureVariable_BA_C = new _Closure$__9();
            $VB$Closure_ClosureVariable_BA_C.$VB$Local_CommentId = CommentId;
            this.Articlesctx.Comments.MergeOption = MergeOption.AppendOnly;
            return this.Articlesctx.Comments.Include("Article").Where<Comment>(Expression.Lambda<Func<Comment, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Comment), "lai"), (MethodInfo) methodof(Comment.get_CommentID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_BA_C, typeof(_Closure$__9)), fieldof(_Closure$__9.$VB$Local_CommentId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Comment>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetCommentCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Articlesctx.Comments.Select<Comment, Comment>(Expression.Lambda<Func<Comment, Comment>>(VB$t_ref$S0 = Expression.Parameter(typeof(Comment), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Comment>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Comment> GetComments()
        {
            ParameterExpression VB$t_ref$S0;
            string key = "Comments_List";
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key])))
            {
                return (List<Comment>) BaseRepository.Cache[key];
            }
            this.Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
            List<Comment> lComments = this.Articlesctx.Comments.Select<Comment, Comment>(Expression.Lambda<Func<Comment, Comment>>(VB$t_ref$S0 = Expression.Parameter(typeof(Comment), "lComment"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Comment>();
            BaseRepository.CacheData(key, lComments);
            return lComments;
        }

        public List<Comment> GetComments(bool bMostRecentFirst)
        {
            List<Comment> lComments;
            string key = "Comments_List_MostRecentFirst_" + Conversions.ToString(bMostRecentFirst);
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key])))
            {
                return (List<Comment>) BaseRepository.Cache[key];
            }
            this.Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
            if (bMostRecentFirst)
            {
                ParameterExpression VB$t_ref$S0;
                lComments = this.Articlesctx.Comments.OrderByDescending<Comment, DateTime>(Expression.Lambda<Func<Comment, DateTime>>(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Comment), "lComment"), (MethodInfo) methodof(Comment.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Comment>();
            }
            else
            {
                ParameterExpression VB$t_ref$S1;
                lComments = this.Articlesctx.Comments.OrderBy<Comment, DateTime>(Expression.Lambda<Func<Comment, DateTime>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Comment), "lComment"), (MethodInfo) methodof(Comment.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Comment>();
            }
            BaseRepository.CacheData(key, lComments);
            return lComments;
        }

        public List<Comment> GetCommentsByArticleId(int vArticleId)
        {
            return this.GetCommentsByArticleId(vArticleId, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="vArticleId"></param>
        /// <param name="bMostRecentFirst"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Comment> GetCommentsByArticleId(int vArticleId, bool bMostRecentFirst)
        {
            List<Comment> lComments;
            _Closure$__7 $VB$Closure_ClosureVariable_56_C = new _Closure$__7();
            $VB$Closure_ClosureVariable_56_C.$VB$Local_vArticleId = vArticleId;
            string key = this.CacheKey + "_Comments_By_ArticleId_" + Conversions.ToString($VB$Closure_ClosureVariable_56_C.$VB$Local_vArticleId) + "_Sort_" + Conversions.ToString(bMostRecentFirst);
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key])))
            {
                lComments = (List<Comment>) BaseRepository.Cache[key];
            }
            else if (bMostRecentFirst)
            {
                ParameterExpression VB$t_ref$S0;
                ParameterExpression VB$t_ref$S1;
                this.Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
                lComments = this.Articlesctx.Comments.Where<Comment>(Expression.Lambda<Func<Comment, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Comment), "lComment"), (MethodInfo) methodof(Comment.get_Article)), (MethodInfo) methodof(Article.get_ArticleID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_56_C, typeof(_Closure$__7)), fieldof(_Closure$__7.$VB$Local_vArticleId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Comment, DateTime>(Expression.Lambda<Func<Comment, DateTime>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Comment), "lComment"), (MethodInfo) methodof(Comment.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Comment>();
            }
            else
            {
                ParameterExpression VB$t_ref$S2;
                ParameterExpression VB$t_ref$S3;
                this.Articlesctx.Comments.MergeOption = MergeOption.NoTracking;
                lComments = this.Articlesctx.Comments.Where<Comment>(Expression.Lambda<Func<Comment, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(Comment), "lComment"), (MethodInfo) methodof(Comment.get_Article)), (MethodInfo) methodof(Article.get_ArticleID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_56_C, typeof(_Closure$__7)), fieldof(_Closure$__7.$VB$Local_vArticleId)), true, null), new ParameterExpression[] { VB$t_ref$S2 })).OrderBy<Comment, DateTime>(Expression.Lambda<Func<Comment, DateTime>>(Expression.Property(VB$t_ref$S3 = Expression.Parameter(typeof(Comment), "lComment"), (MethodInfo) methodof(Comment.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S3 })).ToList<Comment>();
            }
            BaseRepository.CacheData(key, lComments);
            return lComments;
        }

        public bool UnDeleteComment(Comment vComment)
        {
            return this.ChangeDeletedState(vComment, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vComment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateComment(Comment vComment)
        {
            return this.AddComment(vComment);
        }

        [CompilerGenerated]
        internal class _Closure$__7
        {
            public int $VB$Local_vArticleId;

            [DebuggerNonUserCode]
            public _Closure$__7()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__7(CommentRepository._Closure$__7 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vArticleId = other.$VB$Local_vArticleId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__8
        {
            public int $VB$Local_ArticleId;

            [DebuggerNonUserCode]
            public _Closure$__8()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__8(CommentRepository._Closure$__8 other)
            {
                if (other != null)
                {
                    this.$VB$Local_ArticleId = other.$VB$Local_ArticleId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__9
        {
            public int $VB$Local_CommentId;

            [DebuggerNonUserCode]
            public _Closure$__9()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__9(CommentRepository._Closure$__9 other)
            {
                if (other != null)
                {
                    this.$VB$Local_CommentId = other.$VB$Local_CommentId;
                }
            }
        }
    }
}

