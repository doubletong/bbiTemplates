using System;
using BLL;


namespace BBICMS.Events
{

    public partial class EventRSVP : IBaseEntity
    {

        #region " On Changing Methods "


        private void OnEventIdChanging(int value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The EventId cannot be less than 0.")
            // End If

            // If value <> EventId Then
            // IsDirty = True
            // End If

        }

        partial void OnFirstNameChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The FirstName cannot be less than 0.")
            // End If

            // If value <> FirstName Then
            // IsDirty = True
            // End If

        }

        partial void OnLastNameChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The LastName cannot be less than 0.")
            // End If

            // If value <> LastName Then
            // IsDirty = True
            // End If

        }

        partial void OnOrganizationChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The Organization cannot be less than 0.")
            // End If

            // If value <> Organization Then
            // IsDirty = True
            // End If

        }

        partial void OnJobTitleChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The JobTitle cannot be less than 0.")
            // End If

            // If value <> JobTitle Then
            // IsDirty = True
            // End If

        }

        partial void OnEMailChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The EMail cannot be less than 0.")
            // End If

            // If value <> EMail Then
            // IsDirty = True
            // End If

        }

        partial void OnPhoneChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The Phone cannot be less than 0.")
            // End If

            // If value <> Phone Then
            // IsDirty = True
            // End If

        }

        partial void OnNoGuestsChanging(int value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The NoGuests cannot be less than 0.")
            // End If

            // If value <> NoGuests Then
            // IsDirty = True
            // End If

        }

        partial void OnGuestNamesChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The GuestNames cannot be less than 0.")
            // End If

            // If value <> GuestNames Then
            // IsDirty = True
            // End If

        }

        partial void OnAttendStatusChanging(int value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The AttendStatus cannot be less than 0.")
            // End If

            // If value <> AttendStatus Then
            // IsDirty = True
            // End If

        }

        #endregion

        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }

        public string EventTitle
        {
            get
            {
                if ((this.EventInfo != null))
                {
                    return EventInfo.EventTitle;
                }
                return "NA";
            }
        }


        private string _setName = "EventRSVPs";
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.FirstName) == false & string.IsNullOrEmpty(this.LastName) == false)
                {
                    return true;
                }
                return false;
            }
        }

        bool bIsDirty = false;
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

        partial void OnAddedByChanging(string value)
        {
            if (string.IsNullOrEmpty(this.AddedBy) == false)
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

    }
}