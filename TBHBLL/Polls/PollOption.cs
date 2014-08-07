using System;
using System.Data;
using BLL;

namespace BBICMS.Polls

{
    public partial class PollOption : IBaseEntity
    {
        #region " On Changing Methods "

        private double _TotalVotes = 0;

        public int PollId
        {
            get
            {
                if ((PollReference.EntityKey != null))
                {
                    return int.Parse(PollReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if ((PollReference.EntityKey != null))
                {
                    PollReference = null;
                }
                PollReference.EntityKey = new EntityKey("PollEntities.Polls", "PollID", value);
            }
        }

        public int TotalVotes
        {
            get
            {
                if ((Poll != null))
                {
                    _TotalVotes = Poll.Votes;
                }
                return int.Parse(_TotalVotes.ToString());
            }
            set { _TotalVotes = value; }
        }

        public double Percentage
        {
            get
            {
                if (TotalVotes == 0)
                {
                    return -1;
                }
                return ((Votes*100)/TotalVotes);
            }
        }

        public override string ToString()
        {
            //, {3:N1} , Me.Percentage)
            return string.Format("{0}, {1}, {2}", PollId, OptionText, Votes);
        }


        partial void OnAddedDateChanging(DateTime value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The AddedDate cannot be less than 0.")
            // End If

            // If value <> AddedDate Then
            // IsDirty = True
            // End If
        }

        partial void OnOptionTextChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The OptionText cannot be less than 0.")
            // End If

            // If value <> OptionText Then
            // IsDirty = True
            // End If
        }

        partial void OnVotesChanging(int value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The Votes cannot be less than 0.")
            // End If

            // If value <> Votes Then
            // IsDirty = True
            // End If
        }

        partial void OnUpdatedDateChanging(DateTime value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The UpdatedDate cannot be less than 0.")
            // End If

            // If value <> UpdatedDate Then
            // IsDirty = True
            // End If
        }

        #endregion

        private string _setName = "PollOptions";
        private bool bIsDirty = false;

        #region IBaseEntity Members

        /// <summary>
        /// Returns the name of the Data Set the Entity belongs to. Needs to be set
        /// in the derived class.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(OptionText) == false & PollId > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsDirty
        {
            get { return bIsDirty; }
            set { bIsDirty = value; }
        }


        public bool CanAdd
        {
            get { return true; }
        }

        public bool CanDelete
        {
            get { return true; }
        }

        public bool CanEdit
        {
            get { return true; }
        }

        public bool CanRead
        {
            get { return true; }
        }

        #endregion

        partial void OnAddedByChanging(string value)
        {
            if (string.IsNullOrEmpty(AddedBy) == false)
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }
    }
}