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
    public class ForumsRepository : BaseForumRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vForum"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddForum(Forum vForum)
        {
            bool AddForum;
            try
            {
                if (vForum.EntityState == EntityState.Detached)
                {
                    this.Forumctx.AddToForums(vForum);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddForum = this.Forumctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vForum.ForumID), ex);
                AddForum = false;
                ProjectData.ClearProjectError();
                return AddForum;
                ProjectData.ClearProjectError();
            }
            return AddForum;
        }

        private bool ChangeDeletedState(Forum vForum, bool vState)
        {
            bool ChangeDeletedState;
            vForum.Active = vState;
            vForum.UpdatedDate = DateAndTime.Now;
            vForum.UpdatedBy = BaseRepository.CurrentUserName;
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
                this.ActiveExceptions.Add(Conversions.ToString(vForum.ForumID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vForum"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteForum(Forum vForum)
        {
            return this.ChangeDeletedState(vForum, false);
        }

        public List<Forum> GetActiveForums()
        {
            ParameterExpression VB$t_ref$S0;
            this.Forumctx.Forums.MergeOption = MergeOption.NoTracking;
            return this.Forumctx.Forums.Include("Posts").Where<Forum>(Expression.Lambda<Func<Forum, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Forum), "lForum"), (MethodInfo) methodof(Forum.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Forum>();
        }

        /// <summary>
        /// </summary>
        /// <param name="ForumId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Forum GetForumById(int ForumId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__14 $VB$Closure_ClosureVariable_49_C = new _Closure$__14();
            $VB$Closure_ClosureVariable_49_C.$VB$Local_ForumId = ForumId;
            return this.Forumctx.Forums.Where<Forum>(Expression.Lambda<Func<Forum, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Forum), "lai"), (MethodInfo) methodof(Forum.get_ForumID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_49_C, typeof(_Closure$__14)), fieldof(_Closure$__14.$VB$Local_ForumId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Forum>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetForumCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Forumctx.Forums.Select<Forum, Forum>(Expression.Lambda<Func<Forum, Forum>>(VB$t_ref$S0 = Expression.Parameter(typeof(Forum), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Forum>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Forum> GetForums()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_FullList";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Forum>) BaseRepository.Cache[key];
            }
            this.Forumctx.Forums.MergeOption = MergeOption.NoTracking;
            List<Forum> lForums = this.Forumctx.Forums.Select<Forum, Forum>(Expression.Lambda<Func<Forum, Forum>>(VB$t_ref$S0 = Expression.Parameter(typeof(Forum), "lForum"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Forum>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lForums);
            }
            return lForums;
        }

        public bool UnDeleteForum(Forum vForum)
        {
            return this.ChangeDeletedState(vForum, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vForum"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateForum(Forum vForum)
        {
            return this.AddForum(vForum);
        }

        [CompilerGenerated]
        internal class _Closure$__14
        {
            public int $VB$Local_ForumId;

            [DebuggerNonUserCode]
            public _Closure$__14()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__14(ForumsRepository._Closure$__14 other)
            {
                if (other != null)
                {
                    this.$VB$Local_ForumId = other.$VB$Local_ForumId;
                }
            }
        }
    }
}

