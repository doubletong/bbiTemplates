using System;
using BBICMS;
using BLL;

namespace BBICMS.Events
{
    public partial class EventInfo : IBaseEntity
    {
        private string _setName = "Events";
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

        public bool IsValid
        {
            get
            {
                if (Helpers.IsValidString(EventTitle, 1, 100) &&
                    string.IsNullOrEmpty(EventDesc) == false && EventDate > DateTime.MinValue)
                {
                    return true;
                }
                return false;
            }
        }

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