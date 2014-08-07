namespace TheBeerHouse.BLL.EventCalendar
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
    public class EventRepository : BaseEventRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vEventInfo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddEventInfo(TheBeerHouse.BLL.EventCalendar.EventInfo vEventInfo)
        {
            bool AddEventInfo;
            try
            {
                if (vEventInfo.EntityState == EntityState.Detached)
                {
                    this.Eventctx.AddToEventInfos(vEventInfo);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddEventInfo = this.Eventctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vEventInfo.EventId), ex);
                AddEventInfo = false;
                ProjectData.ClearProjectError();
                return AddEventInfo;
                ProjectData.ClearProjectError();
            }
            return AddEventInfo;
        }

        private bool ChangeDeletedState(TheBeerHouse.BLL.EventCalendar.EventInfo vEventInfo, bool vState)
        {
            bool ChangeDeletedState;
            vEventInfo.Active = vState;
            vEventInfo.DateUpdated = DateAndTime.Now;
            vEventInfo.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Eventctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vEventInfo.EventId), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventID"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteEventInfo(int vEventID)
        {
            bool DeleteEventInfo;
            this.DeleteEventInfo(this.GetEventInfoById(vEventID));
            return DeleteEventInfo;
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventInfo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteEventInfo(TheBeerHouse.BLL.EventCalendar.EventInfo vEventInfo)
        {
            return this.ChangeDeletedState(vEventInfo, false);
        }

        public List<TheBeerHouse.BLL.EventCalendar.EventInfo> GetActiveEvents()
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            string key = this.CacheKey + "_FullList_Active";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<TheBeerHouse.BLL.EventCalendar.EventInfo>) BaseRepository.Cache[key];
            }
            this.Eventctx.EventInfos.MergeOption = MergeOption.NoTracking;
            List<TheBeerHouse.BLL.EventCalendar.EventInfo> lEvents = this.Eventctx.EventInfos.Where<TheBeerHouse.BLL.EventCalendar.EventInfo>(Expression.Lambda<Func<TheBeerHouse.BLL.EventCalendar.EventInfo, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(TheBeerHouse.BLL.EventCalendar.EventInfo), "lEventInfo"), (MethodInfo) methodof(TheBeerHouse.BLL.EventCalendar.EventInfo.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).OrderBy<TheBeerHouse.BLL.EventCalendar.EventInfo, DateTime>(Expression.Lambda<Func<TheBeerHouse.BLL.EventCalendar.EventInfo, DateTime>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(TheBeerHouse.BLL.EventCalendar.EventInfo), "lEventInfo"), (MethodInfo) methodof(TheBeerHouse.BLL.EventCalendar.EventInfo.get_EventDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<TheBeerHouse.BLL.EventCalendar.EventInfo>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lEvents);
            }
            return lEvents;
        }

        public List<TheBeerHouse.BLL.EventCalendar.EventInfo> GetDaysEvents(DateTime vDate)
        {
            _Closure$__11 $VB$Closure_ClosureVariable_84_C = new _Closure$__11();
            $VB$Closure_ClosureVariable_84_C.$VB$Local_vDate = vDate;
            return this.GetActiveEvents().Where<TheBeerHouse.BLL.EventCalendar.EventInfo>(new Func<TheBeerHouse.BLL.EventCalendar.EventInfo, bool>($VB$Closure_ClosureVariable_84_C._Lambda$__5)).ToList<TheBeerHouse.BLL.EventCalendar.EventInfo>();
        }

        /// <summary>
        /// </summary>
        /// <param name="EventInfoId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public TheBeerHouse.BLL.EventCalendar.EventInfo GetEventInfoById(int EventInfoId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__10 $VB$Closure_ClosureVariable_4B_C = new _Closure$__10();
            $VB$Closure_ClosureVariable_4B_C.$VB$Local_EventInfoId = EventInfoId;
            return this.Eventctx.EventInfos.Where<TheBeerHouse.BLL.EventCalendar.EventInfo>(Expression.Lambda<Func<TheBeerHouse.BLL.EventCalendar.EventInfo, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(TheBeerHouse.BLL.EventCalendar.EventInfo), "lai"), (MethodInfo) methodof(TheBeerHouse.BLL.EventCalendar.EventInfo.get_EventId)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_4B_C, typeof(_Closure$__10)), fieldof(_Closure$__10.$VB$Local_EventInfoId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<TheBeerHouse.BLL.EventCalendar.EventInfo>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetEventInfoCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Eventctx.EventInfos.Select<TheBeerHouse.BLL.EventCalendar.EventInfo, TheBeerHouse.BLL.EventCalendar.EventInfo>(Expression.Lambda<Func<TheBeerHouse.BLL.EventCalendar.EventInfo, TheBeerHouse.BLL.EventCalendar.EventInfo>>(VB$t_ref$S0 = Expression.Parameter(typeof(TheBeerHouse.BLL.EventCalendar.EventInfo), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<TheBeerHouse.BLL.EventCalendar.EventInfo>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<TheBeerHouse.BLL.EventCalendar.EventInfo> GetEvents()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_FullList";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<TheBeerHouse.BLL.EventCalendar.EventInfo>) BaseRepository.Cache[key];
            }
            this.Eventctx.EventInfos.MergeOption = MergeOption.NoTracking;
            List<TheBeerHouse.BLL.EventCalendar.EventInfo> lEvents = this.Eventctx.EventInfos.OrderBy<TheBeerHouse.BLL.EventCalendar.EventInfo, DateTime>(Expression.Lambda<Func<TheBeerHouse.BLL.EventCalendar.EventInfo, DateTime>>(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(TheBeerHouse.BLL.EventCalendar.EventInfo), "lEventInfo"), (MethodInfo) methodof(TheBeerHouse.BLL.EventCalendar.EventInfo.get_EventDate)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<TheBeerHouse.BLL.EventCalendar.EventInfo>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lEvents);
            }
            return lEvents;
        }

        public ObjectQuery<TheBeerHouse.BLL.EventCalendar.EventInfo> GetEventsQuerys()
        {
            ParameterExpression VB$t_ref$S0;
            return (ObjectQuery<TheBeerHouse.BLL.EventCalendar.EventInfo>) this.Eventctx.EventInfos.Select<TheBeerHouse.BLL.EventCalendar.EventInfo, TheBeerHouse.BLL.EventCalendar.EventInfo>(Expression.Lambda<Func<TheBeerHouse.BLL.EventCalendar.EventInfo, TheBeerHouse.BLL.EventCalendar.EventInfo>>(VB$t_ref$S0 = Expression.Parameter(typeof(TheBeerHouse.BLL.EventCalendar.EventInfo), "lEventInfo"), new ParameterExpression[] { VB$t_ref$S0 }));
        }

        public List<TheBeerHouse.BLL.EventCalendar.EventInfo> GetTodaysEvents()
        {
            return this.GetDaysEvents(DateAndTime.Today);
        }

        public List<TheBeerHouse.BLL.EventCalendar.EventInfo> GetUpcomingEvents()
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            string key = this.CacheKey + "_Upcoming_" + Conversions.ToString(DateAndTime.Today);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<TheBeerHouse.BLL.EventCalendar.EventInfo>) BaseRepository.Cache[key];
            }
            this.Eventctx.EventInfos.MergeOption = MergeOption.NoTracking;
            List<TheBeerHouse.BLL.EventCalendar.EventInfo> lEvents = this.Eventctx.EventInfos.Where<TheBeerHouse.BLL.EventCalendar.EventInfo>(Expression.Lambda<Func<TheBeerHouse.BLL.EventCalendar.EventInfo, bool>>(Expression.And(Expression.GreaterThanOrEqual(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(TheBeerHouse.BLL.EventCalendar.EventInfo), "lEventInfo"), (MethodInfo) methodof(TheBeerHouse.BLL.EventCalendar.EventInfo.get_EventDate)), Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Today)), true, (MethodInfo) methodof(DateTime.op_GreaterThanOrEqual)), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(TheBeerHouse.BLL.EventCalendar.EventInfo.get_Active)), Expression.Constant(true, typeof(bool)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).OrderBy<TheBeerHouse.BLL.EventCalendar.EventInfo, DateTime>(Expression.Lambda<Func<TheBeerHouse.BLL.EventCalendar.EventInfo, DateTime>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(TheBeerHouse.BLL.EventCalendar.EventInfo), "lEventInfo"), (MethodInfo) methodof(TheBeerHouse.BLL.EventCalendar.EventInfo.get_EventDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<TheBeerHouse.BLL.EventCalendar.EventInfo>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lEvents);
            }
            return lEvents;
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventID"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeleteEventInfo(int vEventID)
        {
            bool UnDeleteEventInfo;
            this.UnDeleteEventInfo(this.GetEventInfoById(vEventID));
            return UnDeleteEventInfo;
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventInfo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeleteEventInfo(TheBeerHouse.BLL.EventCalendar.EventInfo vEventInfo)
        {
            return this.ChangeDeletedState(vEventInfo, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventInfo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateEventInfo(TheBeerHouse.BLL.EventCalendar.EventInfo vEventInfo)
        {
            return this.AddEventInfo(vEventInfo);
        }

        [CompilerGenerated]
        internal class _Closure$__10
        {
            public int $VB$Local_EventInfoId;

            [DebuggerNonUserCode]
            public _Closure$__10()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__10(EventRepository._Closure$__10 other)
            {
                if (other != null)
                {
                    this.$VB$Local_EventInfoId = other.$VB$Local_EventInfoId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__11
        {
            public DateTime $VB$Local_vDate;

            [DebuggerNonUserCode]
            public _Closure$__11()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__11(EventRepository._Closure$__11 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vDate = other.$VB$Local_vDate;
                }
            }

            [CompilerGenerated, DebuggerStepThrough]
            public bool _Lambda$__5(TheBeerHouse.BLL.EventCalendar.EventInfo lEventInfo)
            {
                return (DateTime.Compare(lEventInfo.EventDate, this.$VB$Local_vDate) == 0);
            }
        }
    }
}

