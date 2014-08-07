namespace TheBeerHouse.BLL.Forums
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
    public class PostsRepository : BaseForumRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vPost"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddPost(Post vPost)
        {
            bool AddPost;
            try
            {
                if (vPost.EntityState == EntityState.Detached)
                {
                    this.Forumctx.AddToPosts(vPost);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddPost = this.Forumctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vPost.PostID), ex);
                AddPost = false;
                ProjectData.ClearProjectError();
                return AddPost;
                ProjectData.ClearProjectError();
            }
            return AddPost;
        }

        public bool ApprovePost(int vPostId)
        {
            return this.ApprovePost(this.GetPostById(vPostId));
        }

        public bool ApprovePost(Post vPost)
        {
            vPost.Approved = true;
            return this.AddPost(vPost);
        }

        private bool ChangeDeletedState(Post vPost, bool vState)
        {
            bool ChangeDeletedState;
            vPost.Active = vState;
            vPost.UpdatedDate = DateAndTime.Now;
            vPost.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Forumctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vPost.PostID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        public bool CloseThread(int threadPostID)
        {
            Post lthreadPost = this.GetPostById(threadPostID);
            bool ret = false;
            if (!Information.IsNothing(lthreadPost))
            {
                lthreadPost.Closed = true;
                ret = this.UpdatePost(lthreadPost);
            }
            return ret;
        }

        public bool DeletePost(int PostId)
        {
            return this.DeletePost(this.GetPostById(PostId));
        }

        /// <summary>
        /// </summary>
        /// <param name="vPost"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeletePost(Post vPost)
        {
            foreach (Post lpost in this.GetThread(vPost.PostID))
            {
                lpost.Active = false;
            }
            return this.ChangeDeletedState(vPost, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Post GetPostById(int PostId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__15 $VB$Closure_ClosureVariable_1F_C = new _Closure$__15();
            $VB$Closure_ClosureVariable_1F_C.$VB$Local_PostId = PostId;
            return base.Forumctx.Posts.Include("Forum").Where<Post>(Expression.Lambda<Func<Post, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_PostID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_1F_C, typeof(_Closure$__15)), fieldof(_Closure$__15.$VB$Local_PostId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Post>();
        }

        /// <summary>
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Post GetPostByIdorThreadId(int PostId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__17 $VB$Closure_ClosureVariable_43_C = new _Closure$__17();
            $VB$Closure_ClosureVariable_43_C.$VB$Local_PostId = PostId;
            return base.Forumctx.Posts.Where<Post>(Expression.Lambda<Func<Post, bool>>(Expression.Or(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_PostID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_43_C, typeof(_Closure$__17)), fieldof(_Closure$__17.$VB$Local_PostId)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Post.get_ParentPostID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_43_C, typeof(_Closure$__17)), fieldof(_Closure$__17.$VB$Local_PostId)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Post>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPostCount()
        {
            ParameterExpression VB$t_ref$S0;
            return base.Forumctx.Posts.Select<Post, Post>(Expression.Lambda<Func<Post, Post>>(VB$t_ref$S0 = Expression.Parameter(typeof(Post), "lPost"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Post>();
        }

        public int GetPostCountByThread(int threadPostID)
        {
            return this.GetThread(threadPostID).Count;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Post> GetPosts()
        {
            ParameterExpression VB$t_ref$S0;
            return base.Forumctx.Posts.Select<Post, Post>(Expression.Lambda<Func<Post, Post>>(VB$t_ref$S0 = Expression.Parameter(typeof(Post), "lPost"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Post>();
        }

        public IEnumerable<Post> GetRSSForum(int vForumId)
        {
            ParameterExpression VB$t_ref$S1;
            ParameterExpression VB$t_ref$S2;
            _Closure$__19 $VB$Closure_ClosureVariable_B9_C = new _Closure$__19();
            $VB$Closure_ClosureVariable_B9_C.$VB$Local_vForumId = vForumId;
            string key = this.CacheKey + "_IEnumerable";
            this.Forumctx.Posts.MergeOption = MergeOption.NoTracking;
            if ($VB$Closure_ClosureVariable_B9_C.$VB$Local_vForumId > 0)
            {
                ParameterExpression VB$t_ref$S0;
                return this.Forumctx.Posts.OrderByDescending<Post, DateTime>(Expression.Lambda<Func<Post, DateTime>>(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_LastPostDate)), new ParameterExpression[] { VB$t_ref$S0 })).Take<Post>(10).AsEnumerable<Post>();
            }
            return this.Forumctx.Posts.Where<Post>(Expression.Lambda<Func<Post, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_Forum)), (MethodInfo) methodof(Forum.get_ForumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_B9_C, typeof(_Closure$__19)), fieldof(_Closure$__19.$VB$Local_vForumId)), true, null), new ParameterExpression[] { VB$t_ref$S1 })).OrderByDescending<Post, DateTime>(Expression.Lambda<Func<Post, DateTime>>(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_LastPostDate)), new ParameterExpression[] { VB$t_ref$S2 })).Take<Post>(10).AsEnumerable<Post>();
        }

        /// <summary>
        /// </summary>
        /// <param name="vThreadId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Post> GetThread(int vThreadId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__16 $VB$Closure_ClosureVariable_2C_C = new _Closure$__16();
            $VB$Closure_ClosureVariable_2C_C.$VB$Local_vThreadId = vThreadId;
            return base.Forumctx.Posts.Include("Forum").Where<Post>(Expression.Lambda<Func<Post, bool>>(Expression.Or(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_PostID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_2C_C, typeof(_Closure$__16)), fieldof(_Closure$__16.$VB$Local_vThreadId)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Post.get_ParentPostID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_2C_C, typeof(_Closure$__16)), fieldof(_Closure$__16.$VB$Local_vThreadId)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Post>();
        }

        public List<Post> GetThreads(int vforumId)
        {
            return this.GetThreads(vforumId, false);
        }

        public List<Post> GetThreads(int vforumId, bool bIncludeInactive)
        {
            ParameterExpression VB$t_ref$S1;
            _Closure$__18 $VB$Closure_ClosureVariable_AA_C = new _Closure$__18();
            $VB$Closure_ClosureVariable_AA_C.$VB$Local_vforumId = vforumId;
            if (bIncludeInactive)
            {
                ParameterExpression VB$t_ref$S0;
                return base.Forumctx.Posts.Include("Forum").Where<Post>(Expression.Lambda<Func<Post, bool>>(Expression.And(Expression.And(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_Forum)), (MethodInfo) methodof(Forum.get_ForumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_AA_C, typeof(_Closure$__18)), fieldof(_Closure$__18.$VB$Local_vforumId)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Post.get_ParentPostID)), Expression.Constant(0, typeof(int)), true, null)), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Post.get_Approved)), Expression.Constant(true, typeof(bool)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Post>();
            }
            return base.Forumctx.Posts.Include("Forum").Where<Post>(Expression.Lambda<Func<Post, bool>>(Expression.And(Expression.And(Expression.And(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_Forum)), (MethodInfo) methodof(Forum.get_ForumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_AA_C, typeof(_Closure$__18)), fieldof(_Closure$__18.$VB$Local_vforumId)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S1, (MethodInfo) methodof(Post.get_ParentPostID)), Expression.Constant(0, typeof(int)), true, null)), Expression.Equal(Expression.Property(VB$t_ref$S1, (MethodInfo) methodof(Post.get_Approved)), Expression.Constant(true, typeof(bool)), true, null)), Expression.Equal(Expression.Property(VB$t_ref$S1, (MethodInfo) methodof(Post.get_Active)), Expression.Constant(true, typeof(bool)), true, null)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Post>();
        }

        public List<Post> GetUnapprovedPosts()
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            return base.Forumctx.Posts.Include("Forum").Where<Post>(Expression.Lambda<Func<Post, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_Approved)), Expression.Constant(false, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Post, DateTime>(Expression.Lambda<Func<Post, DateTime>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Post), "lPost"), (MethodInfo) methodof(Post.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Post>();
        }

        /// <summary>
        /// </summary>
        /// <param name="vPost"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IncrementViewCount(Post vPost)
        {
            Post VB$t_ref$S0 = vPost;
            VB$t_ref$S0.ViewCount++;
            return this.AddPost(vPost);
        }

        public bool MoveThread(int threadPostID, int forumID)
        {
            Post lthreadPost = this.GetPostByIdorThreadId(threadPostID);
            bool ret = false;
            if (!Information.IsNothing(lthreadPost))
            {
                lthreadPost.ForumId = forumID;
                ret = this.UpdatePost(lthreadPost);
            }
            return ret;
        }

        public bool UnDeletePost(Post vPost)
        {
            return this.ChangeDeletedState(vPost, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vPost"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdatePost(Post vPost)
        {
            return this.AddPost(vPost);
        }

        [CompilerGenerated]
        internal class _Closure$__15
        {
            public int $VB$Local_PostId;

            [DebuggerNonUserCode]
            public _Closure$__15()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__15(PostsRepository._Closure$__15 other)
            {
                if (other != null)
                {
                    this.$VB$Local_PostId = other.$VB$Local_PostId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__16
        {
            public int $VB$Local_vThreadId;

            [DebuggerNonUserCode]
            public _Closure$__16()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__16(PostsRepository._Closure$__16 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vThreadId = other.$VB$Local_vThreadId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__17
        {
            public int $VB$Local_PostId;

            [DebuggerNonUserCode]
            public _Closure$__17()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__17(PostsRepository._Closure$__17 other)
            {
                if (other != null)
                {
                    this.$VB$Local_PostId = other.$VB$Local_PostId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__18
        {
            public int $VB$Local_vforumId;

            [DebuggerNonUserCode]
            public _Closure$__18()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__18(PostsRepository._Closure$__18 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vforumId = other.$VB$Local_vforumId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__19
        {
            public int $VB$Local_vForumId;

            [DebuggerNonUserCode]
            public _Closure$__19()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__19(PostsRepository._Closure$__19 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vForumId = other.$VB$Local_vForumId;
                }
            }
        }
    }
}

