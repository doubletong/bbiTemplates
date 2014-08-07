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
    public class EventRSVPRepository : BaseEventRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vEventRSVP"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddEventRSVP(EventRSVP vEventRSVP)
        {
            bool AddEventRSVP;
            try
            {
                if (vEventRSVP.EntityState == EntityState.Detached)
                {
                    this.Eventctx.AddToEventRSVPs(vEventRSVP);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddEventRSVP = this.Eventctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                AddEventRSVP = false;
                ProjectData.ClearProjectError();
                return AddEventRSVP;
                ProjectData.ClearProjectError();
            }
            return AddEventRSVP;
        }

        private bool ChangeDeletedState(EventRSVP vEventRSVP, bool vState)
        {
            bool ChangeDeletedState;
            vEventRSVP.Active = vState;
            vEventRSVP.DateUpdated = DateAndTime.Now;
            vEventRSVP.UpdatedBy = BaseRepository.CurrentUserName;
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
                this.ActiveExceptions.Add(Conversions.ToString(vEventRSVP.EventRSVPId), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventRSVPID"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteEventRSVP(int vEventRSVPID)
        {
            return this.DeleteEventRSVP(this.GetEventRSVPById(vEventRSVPID));
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventRSVP"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteEventRSVP(EventRSVP vEventRSVP)
        {
            return this.ChangeDeletedState(vEventRSVP, false);
        }

        public List<EventRSVP> GetActiveEventRSVPs()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_List_Active";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<EventRSVP>) BaseRepository.Cache[key];
            }
            this.Eventctx.EventRSVPs.MergeOption = MergeOption.NoTracking;
            List<EventRSVP> lEventRSVPs = this.Eventctx.EventRSVPs.Include("EventInfo").Where<EventRSVP>(Expression.Lambda<Func<EventRSVP, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(EventRSVP), "lEventRSVP"), (MethodInfo) methodof(EventRSVP.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<EventRSVP>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lEventRSVPs);
            }
            return lEventRSVPs;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<EventRSVP> GetEventRSVP()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_List";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<EventRSVP>) BaseRepository.Cache[key];
            }
            this.Eventctx.EventRSVPs.MergeOption = MergeOption.NoTracking;
            List<EventRSVP> lEventRSVPs = this.Eventctx.EventRSVPs.Select<EventRSVP, EventRSVP>(Expression.Lambda<Func<EventRSVP, EventRSVP>>(VB$t_ref$S0 = Expression.Parameter(typeof(EventRSVP), "lEventRSVP"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<EventRSVP>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lEventRSVPs);
            }
            return lEventRSVPs;
        }

        public List<EventRSVP> GetEventRSVPByEventId(int EventId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__12 $VB$Closure_ClosureVariable_41_C = new _Closure$__12();
            $VB$Closure_ClosureVariable_41_C.$VB$Local_EventId = EventId;
            string key = this.CacheKey + "_List_By_EventId_" + Conversions.ToString($VB$Closure_ClosureVariable_41_C.$VB$Local_EventId);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<EventRSVP>) BaseRepository.Cache[key];
            }
            this.Eventctx.EventRSVPs.MergeOption = MergeOption.NoTracking;
            List<EventRSVP> lEventRSVPs = this.Eventctx.EventRSVPs.Include("EventInfo").Where<EventRSVP>(Expression.Lambda<Func<EventRSVP, bool>>(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(EventRSVP), "lEventRSVP"), (MethodInfo) methodof(EventRSVP.get_Active)), Expression.Constant(true, typeof(bool)), true, null), Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(EventRSVP.get_EventInfo)), (MethodInfo) methodof(TheBeerHouse.BLL.EventCalendar.EventInfo.get_EventId)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_41_C, typeof(_Closure$__12)), fieldof(_Closure$__12.$VB$Local_EventId)), true, null)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<EventRSVP>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lEventRSVPs);
            }
            return lEventRSVPs;
        }

        /// <summary>
        /// </summary>
        /// <param name="EventRSVPId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EventRSVP GetEventRSVPById(int EventRSVPId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__13 $VB$Closure_ClosureVariable_5E_C = new _Closure$__13();
            $VB$Closure_ClosureVariable_5E_C.$VB$Local_EventRSVPId = EventRSVPId;
            return this.Eventctx.EventRSVPs.Where<EventRSVP>(Expression.Lambda<Func<EventRSVP, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(EventRSVP), "lai"), (MethodInfo) methodof(EventRSVP.get_EventRSVPId)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_5E_C, typeof(_Closure$__13)), fieldof(_Closure$__13.$VB$Local_EventRSVPId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<EventRSVP>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetEventRSVPCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Eventctx.EventRSVPs.Select<EventRSVP, EventRSVP>(Expression.Lambda<Func<EventRSVP, EventRSVP>>(VB$t_ref$S0 = Expression.Parameter(typeof(EventRSVP), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<EventRSVP>();
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventRSVPId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeleteEventRSVP(int vEventRSVPId)
        {
            return this.ChangeDeletedState(this.GetEventRSVPById(vEventRSVPId), true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventRSVP"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeleteEventRSVP(EventRSVP vEventRSVP)
        {
            return this.ChangeDeletedState(vEventRSVP, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vEventRSVP"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateEventRSVP(EventRSVP vEventRSVP)
        {
            return this.AddEventRSVP(vEventRSVP);
        }

        [CompilerGenerated]
        internal class _Closure$__12
        {
            public int $VB$Local_EventId;

            [DebuggerNonUserCode]
            public _Closure$__12()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__12(EventRSVPRepository._Closure$__12 other)
            {
                if (other != null)
                {
                    this.$VB$Local_EventId = other.$VB$Local_EventId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__13
        {
            public int $VB$Local_EventRSVPId;

            [DebuggerNonUserCode]
            public _Closure$__13()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__13(EventRSVPRepository._Closure$__13 other)
            {
                if (other != null)
                {
                    this.$VB$Local_EventRSVPId = other.$VB$Local_EventRSVPId;
                }
            }
        }
    }
}

