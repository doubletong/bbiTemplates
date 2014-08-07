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
    public class PollOptionsRepository : BasePollRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vPollOption"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddPollOption(PollOption vPollOption)
        {
            bool AddPollOption;
            try
            {
                if (vPollOption.EntityState == EntityState.Detached)
                {
                    this.Pollsctx.AddToPollOptions(vPollOption);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddPollOption = this.Pollsctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vPollOption.OptionID), ex);
                AddPollOption = false;
                ProjectData.ClearProjectError();
                return AddPollOption;
                ProjectData.ClearProjectError();
            }
            return AddPollOption;
        }

        /// <summary>
        /// </summary>
        /// <param name="vPollOption"></param>
        /// <param name="vState"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ChangeDeletedState(PollOption vPollOption, bool vState)
        {
            bool ChangeDeletedState;
            vPollOption.Active = vState;
            vPollOption.UpdatedDate = DateAndTime.Now;
            vPollOption.UpdatedBy = BaseRepository.CurrentUserName;
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
                this.ActiveExceptions.Add(vPollOption.OptionID.ToString(), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vPollOption"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeletePollOption(PollOption vPollOption)
        {
            return this.ChangeDeletedState(vPollOption, false);
        }

        public List<PollOption> GetActivePollOptionsByPollId(int PollId)
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            _Closure$__33 $VB$Closure_ClosureVariable_18_C = new _Closure$__33();
            $VB$Closure_ClosureVariable_18_C.$VB$Local_PollId = PollId;
            return this.Pollsctx.PollOptions.Include("Poll").Where<PollOption>(Expression.Lambda<Func<PollOption, bool>>(Expression.And(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(PollOption), "lPollOption"), (MethodInfo) methodof(PollOption.get_Poll)), (MethodInfo) methodof(Poll.get_PollID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_18_C, typeof(_Closure$__33)), fieldof(_Closure$__33.$VB$Local_PollId)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(PollOption.get_Active)), Expression.Constant(true, typeof(bool)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).Select<PollOption, PollOption>(Expression.Lambda<Func<PollOption, PollOption>>(VB$t_ref$S1 = Expression.Parameter(typeof(PollOption), "lPollOption"), new ParameterExpression[] { VB$t_ref$S1 })).ToList<PollOption>();
        }

        /// <summary>
        /// </summary>
        /// <param name="PollOptionId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public PollOption GetPollOptionById(int PollOptionId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__35 $VB$Closure_ClosureVariable_51_C = new _Closure$__35();
            $VB$Closure_ClosureVariable_51_C.$VB$Local_PollOptionId = PollOptionId;
            return this.Pollsctx.PollOptions.Where<PollOption>(Expression.Lambda<Func<PollOption, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(PollOption), "lai"), (MethodInfo) methodof(PollOption.get_OptionID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_51_C, typeof(_Closure$__35)), fieldof(_Closure$__35.$VB$Local_PollOptionId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<PollOption>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPollOptionCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Pollsctx.PollOptions.Select<PollOption, PollOption>(Expression.Lambda<Func<PollOption, PollOption>>(VB$t_ref$S0 = Expression.Parameter(typeof(PollOption), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<PollOption>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<PollOption> GetPollOptions()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Pollsctx.PollOptions.Select<PollOption, PollOption>(Expression.Lambda<Func<PollOption, PollOption>>(VB$t_ref$S0 = Expression.Parameter(typeof(PollOption), "lPollOption"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<PollOption>();
        }

        public List<PollOption> GetPollOptionsByPollId(int PollId)
        {
            return this.GetPollOptionsByPollId(PollId, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="PollId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<PollOption> GetPollOptionsByPollId(int PollId, bool bIncludeVotes)
        {
            List<PollOption> lPollOptions;
            _Closure$__34 $VB$Closure_ClosureVariable_2B_C = new _Closure$__34();
            $VB$Closure_ClosureVariable_2B_C.$VB$Local_PollId = PollId;
            string key = this.CacheKey + "_OptionsByPoll_" + Conversions.ToString($VB$Closure_ClosureVariable_2B_C.$VB$Local_PollId);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<PollOption>) BaseRepository.Cache[key];
            }
            this.Pollsctx.PollOptions.MergeOption = MergeOption.NoTracking;
            if (bIncludeVotes)
            {
                ParameterExpression VB$t_ref$S0;
                ParameterExpression VB$t_ref$S1;
                lPollOptions = this.Pollsctx.PollOptions.Include("Poll").Where<PollOption>(Expression.Lambda<Func<PollOption, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(PollOption), "lPollOption"), (MethodInfo) methodof(PollOption.get_Poll)), (MethodInfo) methodof(Poll.get_PollID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_2B_C, typeof(_Closure$__34)), fieldof(_Closure$__34.$VB$Local_PollId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).Select<PollOption, PollOption>(Expression.Lambda<Func<PollOption, PollOption>>(VB$t_ref$S1 = Expression.Parameter(typeof(PollOption), "lPollOption"), new ParameterExpression[] { VB$t_ref$S1 })).ToList<PollOption>();
            }
            else
            {
                ParameterExpression VB$t_ref$S2;
                ParameterExpression VB$t_ref$S3;
                lPollOptions = this.Pollsctx.PollOptions.Where<PollOption>(Expression.Lambda<Func<PollOption, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(PollOption), "lPollOption"), (MethodInfo) methodof(PollOption.get_Poll)), (MethodInfo) methodof(Poll.get_PollID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_2B_C, typeof(_Closure$__34)), fieldof(_Closure$__34.$VB$Local_PollId)), true, null), new ParameterExpression[] { VB$t_ref$S2 })).Select<PollOption, PollOption>(Expression.Lambda<Func<PollOption, PollOption>>(VB$t_ref$S3 = Expression.Parameter(typeof(PollOption), "lPollOption"), new ParameterExpression[] { VB$t_ref$S3 })).ToList<PollOption>();
            }
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lPollOptions);
            }
            return lPollOptions;
        }

        /// <summary>
        /// </summary>
        /// <param name="vPollOption"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeletePollOption(PollOption vPollOption)
        {
            return this.ChangeDeletedState(vPollOption, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vPollOption"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdatePollOption(PollOption vPollOption)
        {
            return this.AddPollOption(vPollOption);
        }

        /// <summary>
        /// </summary>
        /// <param name="vPollOptionId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Vote(int vPollOptionId)
        {
            return this.Vote(this.GetPollOptionById(vPollOptionId));
        }

        /// <summary>
        /// </summary>
        /// <param name="vPollOption"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Vote(PollOption vPollOption)
        {
            PollOption VB$t_ref$S0 = vPollOption;
            VB$t_ref$S0.Votes++;
            return this.UpdatePollOption(vPollOption);
        }

        [CompilerGenerated]
        internal class _Closure$__33
        {
            public int $VB$Local_PollId;

            [DebuggerNonUserCode]
            public _Closure$__33()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__33(PollOptionsRepository._Closure$__33 other)
            {
                if (other != null)
                {
                    this.$VB$Local_PollId = other.$VB$Local_PollId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__34
        {
            public int $VB$Local_PollId;

            [DebuggerNonUserCode]
            public _Closure$__34()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__34(PollOptionsRepository._Closure$__34 other)
            {
                if (other != null)
                {
                    this.$VB$Local_PollId = other.$VB$Local_PollId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__35
        {
            public int $VB$Local_PollOptionId;

            [DebuggerNonUserCode]
            public _Closure$__35()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__35(PollOptionsRepository._Closure$__35 other)
            {
                if (other != null)
                {
                    this.$VB$Local_PollOptionId = other.$VB$Local_PollOptionId;
                }
            }
        }
    }
}

