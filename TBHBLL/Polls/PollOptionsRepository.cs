using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BBICMS.Polls
{

public class PollOptionsRepository : BasePollRepository
    {

        #region " BLL/DAL Methods "

        public List<PollOption> GetPollOptions()
        {
            return (from lPollOption in Pollsctx.PollOptions
                    select lPollOption).ToList();
        }

        public List<PollOption> GetActivePollOptionsByPollId(int PollId)
        {
            return (from lPollOption in Pollsctx.PollOptions
                    where lPollOption.Active && lPollOption.Poll.PollID == PollId
                    select lPollOption).ToList();
        }


        public List<PollOption> GetPollOptionsByPollId(int PollId)
        {
            return GetPollOptionsByPollId(PollId, true);
        }

        public List<PollOption> GetPollOptionsByPollId(int PollId, bool bIncludeVotes)
        {
            
            string key = CacheKey + "_OptionsByPoll_" + PollId;
            
            if (EnableCaching && (Cache[key] != null)) {
                return (List<PollOption>)Cache[key];
            }
            
            List<PollOption> lPollOptions = default(List<PollOption>);
            this.Pollsctx.PollOptions.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            
            
            if (bIncludeVotes) {
                    
                lPollOptions = (from lPollOption in Pollsctx.PollOptions.Include("Poll")
                    select lPollOption).ToList();
            }
            else {

                lPollOptions = (from lPollOption in Pollsctx.PollOptions.Include("Poll")
                                where lPollOption.Poll.PollID == PollId
                                select lPollOption).ToList();
            }
            
            if (EnableCaching) {
                CacheData(key, lPollOptions, CacheDuration);
            }
            
                
            return lPollOptions;
        }

        public PollOption GetPollOptionById(int PollOptionId)
        {
            return (from lPollOption in Pollsctx.PollOptions
                    where lPollOption.OptionID == PollOptionId 
                    select lPollOption).FirstOrDefault();
        }

        public int GetPollOptionCount()
        {
            return (from lPollOption in Pollsctx.PollOptions
                    select lPollOption).Count();
        }

        public PollOption AddPollOption(int vPollOptionID, System.DateTime vAddedDate, int vPollID, string vOptionText, int vVotes, System.DateTime vUpdatedDate)
        {

            PollOption lPollOption;

            if (vPollOptionID > 0)
            {

                lPollOption = GetPollOptionById(vPollOptionID);

                lPollOption.OptionID = vPollOptionID;
                lPollOption.AddedDate = vAddedDate;
                lPollOption.PollId = vPollID;
                lPollOption.OptionText = vOptionText;
                lPollOption.Votes = vVotes;
                lPollOption.UpdatedDate = vUpdatedDate;


                lPollOption.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lPollOption = PollOption.CreatePollOption(vPollOptionID, vAddedDate, Helpers.CurrentUserName, 
                    vOptionText, vVotes, vUpdatedDate, true);
            }

            return AddPollOption(lPollOption);
        }

        public PollOption AddPollOption(PollOption vPollOption)
        {

            try
            {
                if (vPollOption.EntityState == EntityState.Detached)
                {
                    Pollsctx.AddToPollOptions(vPollOption);
                }
                base.PurgeCacheItems(CacheKey);

                return Pollsctx.SaveChanges() > 0 ? vPollOption : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vPollOption.OptionID, ex);
                return null;

            }
        }

        public bool DeletePollOption(PollOption vPollOption)
        {
            return ChangeDeletedState(vPollOption, false);
        }

        public bool UnDeletePollOption(PollOption vPollOption)
        {
            return ChangeDeletedState(vPollOption, true);
        }

        private bool ChangeDeletedState(PollOption vPollOption, bool vState)
        {
            vPollOption.Active = vState;
            vPollOption.UpdatedDate = DateTime.Now;
            vPollOption.UpdatedBy = CurrentUserName;

            try
            {
                Pollsctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vPollOption.OptionID.ToString(), ex);

                return false;

            }
        }

        #endregion

        public bool Vote(int vPollOptionId)
        {
            return Vote(GetPollOptionById(vPollOptionId));
        }

        public bool Vote(PollOption vPollOption)
        {
            vPollOption.Votes += 1;
            return (AddPollOption(vPollOption) != null) ? true : false;
        }

    }
}