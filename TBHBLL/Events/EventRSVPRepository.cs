    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;


namespace BBICMS.Events
{
    
    public class EventRSVPRepository : BaseEventRepository
    {

public bool AddEventRSVP(EventRSVP vEventRSVP)
{
    
    try {
        if (vEventRSVP.EntityState == EntityState.Detached) {
            Eventctx.AddToEventRSVPs(vEventRSVP);
        }
        base.PurgeCacheItems(CacheKey);
            
        return Eventctx.SaveChanges() > 0 ? true : false;
    }
    catch (Exception ex) {
        return false;
        
    }
}

public List<EventRSVP> GetActiveEventRSVPs()
{
    
    string key = CacheKey + "_List_Active";
    
    if (EnableCaching && (Cache[key] != null)) {
        return (List<EventRSVP>)Cache[key];
    }
    
    List<EventRSVP> lEventRSVPs;
    Eventctx.EventRSVPs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
    
    lEventRSVPs = (from lEventRSVP in Eventctx.EventRSVPs.Include("EventInfo")
            where lEventRSVP.Active
                   select lEventRSVP).ToList();
    
    if (EnableCaching) {
        CacheData(key, lEventRSVPs, CacheDuration);
    }
    
        
    return lEventRSVPs;
}

        public List<EventRSVP> GetEventRSVP()

{
    string key = CacheKey + "_List";
    
    if (EnableCaching && (Cache[key] != null)) {
        return (List<EventRSVP>)Cache[key];
    }
    
    List<EventRSVP> lEventRSVPs = default(List<EventRSVP>);
    Eventctx.EventRSVPs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
    
    lEventRSVPs = (from lEventRSVP in Eventctx.EventRSVPs
            select lEventRSVP).ToList();
    
    if (EnableCaching) {
        CacheData(key, lEventRSVPs, CacheDuration);
    }
    
    return lEventRSVPs;
}

        public List<EventRSVP> GetEventRSVPByEventId(int EventId)
        {

    string key = CacheKey + "_List_By_EventId_" + EventId;
    
    if (EnableCaching && (Cache[key] != null)) {
        return (List<EventRSVP>)Cache[key];
    }
    
    List<EventRSVP> lEventRSVPs = default(List<EventRSVP>);
    Eventctx.EventRSVPs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
    
    lEventRSVPs = (from lEventRSVP in Eventctx.EventRSVPs.Include("EventInfo")
            where lEventRSVP.Active && lEventRSVP.EventInfo.EventId == EventId
            select lEventRSVP).ToList();
    
    if (EnableCaching) {
        CacheData(key, lEventRSVPs, CacheDuration);
    }
    
    return lEventRSVPs;

        }

        public EventRSVP GetEventRSVPById(int EventRSVPId)
        {
             return (from lai in Eventctx.EventRSVPs
                where lai.EventRSVPId == EventRSVPId
            select lai).
            FirstOrDefault();
        }

        public int GetEventRSVPCount()
        {
            return (from lai in Eventctx.EventRSVPs select lai).Count();
        }

        public bool UpdateEventRSVP(EventRSVP vEventRSVP)
        {
            return AddEventRSVP(vEventRSVP);
        }

        #region " Delete Methods "

public bool DeleteEventRSVP(int vEventRSVPID)
{
    return DeleteEventRSVP(this.GetEventRSVPById(vEventRSVPID));
}

public bool DeleteEventRSVP(EventRSVP vEventRSVP)
{
    return ChangeDeletedState(vEventRSVP, false);
}

public bool UnDeleteEventRSVP(int vEventRSVPId)
{
    return ChangeDeletedState(this.GetEventRSVPById(vEventRSVPId), true);
}

public bool UnDeleteEventRSVP(EventRSVP vEventRSVP)
{
    return ChangeDeletedState(vEventRSVP, true);
}


private bool ChangeDeletedState(EventRSVP vEventRSVP, bool vState)
{
    vEventRSVP.Active = vState;
    vEventRSVP.DateUpdated = DateTime.Now;
    vEventRSVP.UpdatedBy = CurrentUserName;
    
    try {
        Eventctx.SaveChanges();
        base.PurgeCacheItems(CacheKey);
        return true;
    }
    catch (Exception ex) {
        ActiveExceptions.Add(vEventRSVP.EventRSVPId.ToString(), ex);
        
        return false;
        
    }
}

#endregion

    }
}

