using System.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBICMS;

namespace BBICMS.Events
{

    public class EventRepository : BaseEventRepository
    {

        public EventInfo AddEventInfo(int vEventId, string vEventTitle, string vEventDesc, 
            DateTime vEventDate, DateTime vEventEndDate, DateTime vEventExpires, 
            string vEventTime, string vEndTime, string vEventLocation, string vAddress,
            string vCity, string vState, string vZipCode, bool vFeatured, string vDuration, 
            int vImportance, bool vAllowRegistration, string vAttachment)
        {

            EventInfo lEventInfo;

            if (vEventId > 0)
            {

                lEventInfo = GetEventInfoById(vEventId);

                lEventInfo.EventId = vEventId;
                lEventInfo.EventTitle = vEventTitle;
                lEventInfo.EventDesc = vEventDesc;
                lEventInfo.EventDate = vEventDate;
                lEventInfo.EventEndDate = vEventEndDate;
                lEventInfo.EventExpires = vEventExpires;
                lEventInfo.EventTime = vEventTime;
                lEventInfo.EndTime = vEndTime;
                lEventInfo.EventLocation = vEventLocation;
                lEventInfo.Address = vAddress;
                lEventInfo.City = vCity;
                lEventInfo.State = vState;
                lEventInfo.ZipCode = vZipCode;
                lEventInfo.Featured = vFeatured;
                lEventInfo.Duration = vDuration;
                lEventInfo.Importance = vImportance;
                lEventInfo.AllowRegistration = vAllowRegistration;
                lEventInfo.Attachment = vAttachment;

                lEventInfo.DateUpdated = DateTime.Now;

                lEventInfo.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lEventInfo = EventInfo.CreateEventInfo(vEventId, vEventTitle, vEventDesc, vEventDate, 
                    vEventExpires, vEventTime, vImportance, vAllowRegistration, true, Helpers.CurrentUserName,
                DateTime.Now, Helpers.CurrentUserName, DateTime.Now);

                lEventInfo.EventEndDate = vEventEndDate;
                lEventInfo.EndTime = vEndTime;
                lEventInfo.EventLocation = vEventLocation;
                lEventInfo.Address = vAddress;
                lEventInfo.City = vCity;
                lEventInfo.State = vState;
                lEventInfo.ZipCode = vZipCode;
                lEventInfo.Featured = vFeatured;
                lEventInfo.Duration = vDuration;
                lEventInfo.Importance = vImportance;

                lEventInfo.AllowRegistration = vAllowRegistration;
            }


            return AddEventInfo(lEventInfo);
        }

        public EventInfo AddEventInfo(EventInfo vEventInfo)
        {

            try
            {

                if (vEventInfo.EntityState == EntityState.Detached)
                {
                    Eventctx.AddToEventInfos(vEventInfo);
                }
                base.PurgeCacheItems(CacheKey);

                return Eventctx.SaveChanges() > 0 ? vEventInfo : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vEventInfo.EventId, ex);
                return null;

            }
        }

        private bool ChangeDeletedState(EventInfo vEventInfo, bool vState)
        {
            vEventInfo.Active = vState;
            vEventInfo.DateUpdated = DateTime.Now;
            vEventInfo.UpdatedBy = CurrentUserName;

            try
            {
                Eventctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vEventInfo.EventId.ToString(), ex);

                return false;

            }
        }

        public bool DeleteEventInfo(int vEventID)
        {
            return DeleteEventInfo(GetEventInfoById(vEventID));
        }

        public bool UnDeleteEventInfo(int vEventID)
        {
            return UnDeleteEventInfo(GetEventInfoById(vEventID));
        }

        public bool DeleteEventInfo(EventInfo vEventInfo)
        {
            return ChangeDeletedState(vEventInfo, false);
        }

        public bool UnDeleteEventInfo(EventInfo vEventInfo)
        {
            return ChangeDeletedState(vEventInfo, true);
        }

        public EventInfo GetEventInfoById(int EventInfoId)
        {
            return (from lai in Eventctx.EventInfos
                    where lai.EventId == EventInfoId
                    select lai).FirstOrDefault();

        }

        public int GetEventInfoCount()
        {
            return (from lai in Eventctx.EventInfos
                    select lai).Count();
        }

        public List<EventInfo> GetEvents()
        {

            string key = CacheKey + "_FullList";

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<EventInfo>)Cache[key];
            }

            List<EventInfo> lEvents = default(List<EventInfo>);
            Eventctx.EventInfos.MergeOption = MergeOption.NoTracking;

            lEvents = (from lEventInfo in Eventctx.EventInfos
                       orderby lEventInfo.EventDate ascending
                       select lEventInfo).ToList();

            if (EnableCaching)
            {
                CacheData(key, lEvents, CacheDuration);
            }

            return lEvents;
        }

        public IQueryable<EventInfo> GetEventsQuerys(){
            return (from lEventInfo in Eventctx.EventInfos
                    select lEventInfo);
        }

        public List<EventInfo> GetActiveEvents()
        {

            string key = CacheKey + "_FullList_Active";

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<EventInfo>)Cache[key];
            }

            List<EventInfo> lEvents = default(List<EventInfo>);
            Eventctx.EventInfos.MergeOption = System.Data.Objects.MergeOption.NoTracking;

            lEvents = (from lEventInfo in Eventctx.EventInfos
                       where lEventInfo.Active
                       orderby lEventInfo.EventDate ascending
                       select lEventInfo).ToList();

            if (EnableCaching)
            {
                CacheData(key, lEvents, CacheDuration);
            }

            return lEvents;
        }

        public List<EventInfo> GetTodaysEvents()
        {
            return GetDaysEvents(DateTime.Today);
        }

        public List<EventInfo> GetDaysEvents(DateTime vDate)
        {
            return (from lEventInfo in GetActiveEvents()
                    where lEventInfo.EventDate == vDate
                    select lEventInfo).ToList();
        }

        public List<EventInfo> GetUpcomingEvents()
        {
            return GetUpcomingEvents(0);
        }

        public List<EventInfo> GetUpcomingEvents(int vTotal)
        {

            string key = CacheKey + "_Upcoming_" + vTotal;

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<EventInfo>)Cache[key];
            }

            List<EventInfo> lEvents = default(List<EventInfo>);
            Eventctx.EventInfos.MergeOption = System.Data.Objects.MergeOption.NoTracking;

            lEvents = (from lEventInfo in Eventctx.EventInfos
                       where lEventInfo.EventDate >= DateTime.Today
                       && lEventInfo.Active == true
                       orderby lEventInfo.EventDate ascending
                       select lEventInfo).Take(vTotal > 0 ? vTotal : 20).ToList();

            if (EnableCaching)
            {
                CacheData(key, lEvents, CacheDuration);
            }


            return lEvents;
        }

        public EventInfo UpdateEventInfo(EventInfo vEventInfo)
        {
            return AddEventInfo(vEventInfo);
        }

    }
}

