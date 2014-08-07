namespace TheBeerHouse.BLL
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

    /// <summary>
    /// The Entity Repository Class. Contains methods that utilize the Entity Framework to 
    /// interact with the database.
    /// </summary>
    /// <remarks></remarks>
    public class PollsRepository : BasePollRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vPoll"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddPoll(Poll vPoll)
        {
            bool AddPoll;
            try
            {
                if (vPoll.EntityState == EntityState.Detached)
                {
                    this.Pollsctx.AddToPolls(vPoll);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddPoll = this.Pollsctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                AddPoll = false;
                ProjectData.ClearProjectError();
                return AddPoll;
                ProjectData.ClearProjectError();
            }
            return AddPoll;
        }

        /// <summary>
        /// </summary>
        /// <param name="vPoll"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Archive(Poll vPoll)
        {
            bool success = this.ArchivePoll(vPoll.PollID);
            if (success)
            {
                vPoll.IsCurrent = false;
                vPoll.IsArchived = true;
                vPoll.ArchivedDate = DateTime.Now;
            }
            return success;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ArchivePoll(int id)
        {
            return this.ArchivePoll(this.GetPollById(id));
        }

        /// <summary>
        /// </summary>
        /// <param name="vPoll"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ArchivePoll(Poll vPoll)
        {
            vPoll.IsCurrent = false;
            vPoll.IsArchived = true;
            vPoll.ArchivedDate = DateAndTime.Now;
            bool ret = this.UpdatePoll(vPoll);
            this.PurgeCacheItems("polls_polls");
            this.PurgeCacheItems("polls_poll_" + Conversions.ToString(vPoll.PollID));
            this.PurgeCacheItems("polls_poll_current");
            return ret;
        }

        /// <summary>
        /// </summary>
        /// <param name="vPoll"></param>
        /// <param name="vState"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ChangeDeletedState(Poll vPoll, bool vState)
        {
            bool ChangeDeletedState;
            vPoll.Active = vState;
            vPoll.UpdatedDate = DateAndTime.Now;
            vPoll.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Pollsctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vPoll.PollID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vPollId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeletePoll(int vPollId)
        {
            return this.ChangeDeletedState(this.GetPollById(vPollId), false);
        }

        /// <summary>
        /// </summary>
        /// <param name="vPoll"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeletePoll(Poll vPoll)
        {
            return this.ChangeDeletedState(vPoll, false);
        }

        public List<Poll> GetArchivedPolls()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_Archived_List";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Poll>) BaseRepository.Cache[key];
            }
            this.Pollsctx.Polls.MergeOption = MergeOption.NoTracking;
            List<Poll> lPolls = this.Pollsctx.Polls.Where<Poll>(Expression.Lambda<Func<Poll, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Poll), "lPoll"), (MethodInfo) methodof(Poll.get_IsArchived)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Poll>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lPolls);
            }
            return lPolls;
        }

        public int GetCurrentPollID()
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            int vPollId = base.Pollsctx.Polls.Where<Poll>(Expression.Lambda<Func<Poll, bool>>(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Poll), "lai"), (MethodInfo) methodof(Poll.get_IsArchived)), Expression.Constant(false, typeof(bool)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Poll.get_IsCurrent)), Expression.Constant(true, typeof(bool)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).Select<Poll, int>(Expression.Lambda<Func<Poll, int>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Poll), "lai"), (MethodInfo) methodof(Poll.get_PollID)), new ParameterExpression[] { VB$t_ref$S1 })).FirstOrDefault<int>();
            return ((vPollId == 0) ? -1 : vPollId);
        }

        /// <summary>
        /// </summary>
        /// <param name="PollId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Poll GetPollById(int PollId)
        {
            return this.GetPollById(PollId, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="PollId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Poll GetPollById(int PollId, bool bIncludeOptions)
        {
            Poll lPoll;
            _Closure$__36 $VB$Closure_ClosureVariable_4F_C = new _Closure$__36();
            $VB$Closure_ClosureVariable_4F_C.$VB$Local_PollId = PollId;
            string key = this.CacheKey + string.Format("_PollId_{0}_IncludePollOptions_{1}", $VB$Closure_ClosureVariable_4F_C.$VB$Local_PollId, bIncludeOptions);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (Poll) BaseRepository.Cache[key];
            }
            if (bIncludeOptions)
            {
                ParameterExpression VB$t_ref$S0;
                lPoll = base.Pollsctx.Polls.Include("PollOptions").Where<Poll>(Expression.Lambda<Func<Poll, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Poll), "lai"), (MethodInfo) methodof(Poll.get_PollID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_4F_C, typeof(_Closure$__36)), fieldof(_Closure$__36.$VB$Local_PollId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Poll>();
            }
            else
            {
                ParameterExpression VB$t_ref$S1;
                lPoll = base.Pollsctx.Polls.Where<Poll>(Expression.Lambda<Func<Poll, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Poll), "lai"), (MethodInfo) methodof(Poll.get_PollID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_4F_C, typeof(_Closure$__36)), fieldof(_Closure$__36.$VB$Local_PollId)), true, null), new ParameterExpression[] { VB$t_ref$S1 })).FirstOrDefault<Poll>();
            }
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lPoll);
            }
            return lPoll;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPollCount()
        {
            return base.Pollsctx.Polls.Count<Poll>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Poll> GetPolls()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_List";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Poll>) BaseRepository.Cache[key];
            }
            this.Pollsctx.Polls.MergeOption = MergeOption.NoTracking;
            List<Poll> lPolls = this.Pollsctx.Polls.Select<Poll, Poll>(Expression.Lambda<Func<Poll, Poll>>(VB$t_ref$S0 = Expression.Parameter(typeof(Poll), "lPoll"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Poll>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lPolls);
            }
            return lPolls;
        }

        /// <summary>
        /// </summary>
        /// <param name="vPoll"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeletePoll(Poll vPoll)
        {
            return this.ChangeDeletedState(vPoll, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vPoll"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdatePoll(Poll vPoll)
        {
            return this.AddPoll(vPoll);
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Poll CurrentPoll
        {
            get
            {
                return this.GetPollById(this.CurrentPollID);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CurrentPollID
        {
            get
            {
                int pollID = -1;
                string key = "Polls_Poll_Current";
                pollID = this.GetCurrentPollID();
                BaseRepository.CacheData(key, pollID);
                return pollID;
            }
        }

        [CompilerGenerated]
        internal class _Closure$__36
        {
            public int $VB$Local_PollId;

            [DebuggerNonUserCode]
            public _Closure$__36()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__36(PollsRepository._Closure$__36 other)
            {
                if (other != null)
                {
                    this.$VB$Local_PollId = other.$VB$Local_PollId;
                }
            }
        }
    }
}

