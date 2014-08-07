using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BBICMS.Polls

{

    /// <summary>
    /// The Entity Repository Class. Contains methods that utilize the Entity Framework to 
    /// interact with the database.
    /// </summary>
    /// <remarks></remarks>
    public class PollsRepository : BasePollRepository
    {

        #region " BLL/DAL Methods "

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Poll> GetPolls()
        {
            
            string key = CacheKey + "_List";
            
            if (EnableCaching && (Cache[key] != null)) {
                return (List<Poll>)Cache[key];
            }
            
            List<Poll> lPolls;
            Pollsctx.Polls.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            
            lPolls = (from lPoll in Pollsctx.Polls
                      select lPoll).ToList();
            
            if (EnableCaching) {
                CacheData(key, lPolls, CacheDuration);
            }
            
            return lPolls;
        }

        public List<Poll> GetArchivedPolls()
        {
            
            string key = CacheKey + "_Archived_List";
            
            if (EnableCaching && (Cache[key] != null)) {
                return (List<Poll>)Cache[key];
            }
            
            List<Poll> lPolls;
            Pollsctx.Polls.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            
            lPolls = (from lPoll in Pollsctx.Polls
                    where lPoll.IsArchived == true
                      select lPoll).ToList();
            
            if (EnableCaching) {
                CacheData(key, lPolls, CacheDuration);
            }
                
            return lPolls;
        }

        public Poll GetPollById(int PollId)
        {
            return GetPollById(PollId, true);
        }

        public Poll GetPollById(int PollId, bool bIncludeOptions)
        {
            
            string key = CacheKey + string.Format("_PollId_{0}_IncludePollOptions_{1}", PollId, bIncludeOptions);
            
            if (EnableCaching && (Cache[key] != null)) {
                return (Poll)Cache[key];
            }
            
            Poll lPoll = default(Poll);
            
            if (bIncludeOptions) {
                lPoll = (from lai in Pollsctx.Polls.Include("PollOptions")
                where lai.PollID == PollId
                             select lai).FirstOrDefault();
            }
            else {
                lPoll = (from lai in Pollsctx.Polls
                         where lai.PollID == PollId
                         select lai).FirstOrDefault();
            }
            
            if (EnableCaching) {
                CacheData(key, lPoll, CacheDuration);
            }
                
            return lPoll;
        }

        public int GetPollCount()
        {
            return base.Pollsctx.Polls.Count();
        }

        public Poll AddPoll(int vPollID, System.DateTime vAddedDate, string vQuestionText, bool vIsCurrent, bool vIsArchived, System.DateTime vArchivedDate, System.DateTime vUpdatedDate)
        {

            Poll lPoll = default(Poll);

            if (vPollID > 0)
            {

                lPoll = GetPollById(vPollID);

                lPoll.PollID = vPollID;
                lPoll.AddedDate = vAddedDate;
                lPoll.QuestionText = vQuestionText;
                lPoll.IsCurrent = vIsCurrent;
                lPoll.IsArchived = vIsArchived;
                lPoll.ArchivedDate = vArchivedDate;
                lPoll.UpdatedDate = vUpdatedDate;

                lPoll.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lPoll = Poll.CreatePoll(vPollID, vAddedDate, Helpers.CurrentUserName, 
                    vQuestionText, vIsCurrent, vIsArchived, vUpdatedDate, true);
                lPoll.ArchivedDate = vArchivedDate;
            }

            return AddPoll(lPoll);
        }

        public Poll AddPoll(Poll vPoll)
        {

            try
            {
                if (vPoll.EntityState == EntityState.Detached)
                {
                    Pollsctx.AddToPolls(vPoll);
                }
                base.PurgeCacheItems(CacheKey);

                return Pollsctx.SaveChanges() > 0 ? vPoll : null;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public bool DeletePoll(int vPollId)
        {
            return ChangeDeletedState(this.GetPollById(vPollId), false);
        }

        public bool DeletePoll(Poll vPoll)
        {
            return ChangeDeletedState(vPoll, false);
        }

        public bool UnDeletePoll(Poll vPoll)
        {
            return ChangeDeletedState(vPoll, true);
        }

        private bool ChangeDeletedState(Poll vPoll, bool vState)
        {
            vPoll.Active = vState;
            vPoll.UpdatedDate = DateTime.Now;
            vPoll.UpdatedBy = CurrentUserName;

            try
            {
                Pollsctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vPoll.PollID.ToString(), ex);

                return false;

            }
        }

        #endregion

        public bool ArchivePoll(int id)
        {
            return ArchivePoll(this.GetPollById(id));
        }

        public bool ArchivePoll(Poll vPoll)
        {

            vPoll.IsCurrent = false;
            vPoll.IsArchived = false;
            vPoll.ArchivedDate = DateTime.Now;

            bool ret = (AddPoll(vPoll) != null) ? true : false;
            PurgeCacheItems("polls_polls");
            PurgeCacheItems("polls_poll_" + vPoll.PollID);
            PurgeCacheItems("polls_poll_current");
            return ret;
        }

        public bool Archive(Poll vPoll)
        {
            bool success = ArchivePoll(vPoll.PollID);
            if (success)
            {
                vPoll.IsCurrent = false;
                vPoll.IsArchived = true;
                vPoll.ArchivedDate = DateTime.Now;
            }
            return success;
        }

        public int CurrentPollID
        {
            get
            {
                int pollID = -1;
                string key = "Polls_Poll_Current";

                //If EnableCaching AndAlso Not IsNothing(Cache[key]) Then
                // pollID = CInt(Cache[key])
                //Else
                pollID = GetCurrentPollID();
                CacheData(key, pollID, CacheDuration);
                // End If

                return pollID;
            }
        }

        public int GetCurrentPollID()
        {
            
            var vPollId = (from lai in Pollsctx.Polls
                where lai.IsArchived == false && lai.IsCurrent == true
                select lai.PollID).FirstOrDefault();
            return vPollId == 0 ? -1 : vPollId;
        }

        public Poll CurrentPoll
        {
            get { return GetPollById(CurrentPollID); }
        }

    }
}