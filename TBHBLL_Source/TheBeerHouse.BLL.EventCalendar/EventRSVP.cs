namespace TheBeerHouse.BLL.EventCalendar
{
    using Microsoft.VisualBasic;
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for CalendarofEventsModel.EventRSVP in the schema.
    /// </summary>
    /// <KeyProperties>
    /// EventRSVPId
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="CalendarofEventsModel", Name="EventRSVP")]
    public class EventRSVP : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private int _AttendStatus;
        private DateTime _DateAdded;
        private DateTime _DateUpdated;
        private string _EMail;
        private int _EventRSVPId;
        private string _FirstName;
        private string _GuestNames;
        private string _JobTitle;
        private string _LastName;
        private int _NoGuests;
        private string _Organization;
        private string _Phone;
        private string _setName = "EventRSVPs";
        private string _UpdatedBy;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new EventRSVP object.
        /// </summary>
        /// <param name="eventRSVPId">Initial value of EventRSVPId.</param>
        /// <param name="firstName">Initial value of FirstName.</param>
        /// <param name="lastName">Initial value of LastName.</param>
        /// <param name="noGuests">Initial value of NoGuests.</param>
        /// <param name="attendStatus">Initial value of AttendStatus.</param>
        /// <param name="active">Initial value of Active.</param>
        /// <param name="dateAdded">Initial value of DateAdded.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="dateUpdated">Initial value of DateUpdated.</param>
        /// <param name="updatedBy">Initial value of UpdatedBy.</param>
        public static EventRSVP CreateEventRSVP(int eventRSVPId, string firstName, string lastName, int noGuests, int attendStatus, bool active, DateTime dateAdded, string addedBy, DateTime dateUpdated, string updatedBy)
        {
            EventRSVP eventRSVP = new EventRSVP();
            eventRSVP.EventRSVPId = eventRSVPId;
            eventRSVP.FirstName = firstName;
            eventRSVP.LastName = lastName;
            eventRSVP.NoGuests = noGuests;
            eventRSVP.AttendStatus = attendStatus;
            eventRSVP.Active = active;
            eventRSVP.DateAdded = dateAdded;
            eventRSVP.AddedBy = addedBy;
            eventRSVP.DateUpdated = dateUpdated;
            eventRSVP.UpdatedBy = updatedBy;
            return eventRSVP;
        }

        private void OnAddedByChanging(string value)
        {
            if (!string.IsNullOrEmpty(this.AddedBy))
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

        private void OnAttendStatusChanging(int value)
        {
        }

        private void OnEMailChanging(string value)
        {
        }

        private void OnEventIdChanging(int value)
        {
        }

        private void OnFirstNameChanging(string value)
        {
        }

        private void OnGuestNamesChanging(string value)
        {
        }

        private void OnJobTitleChanging(string value)
        {
        }

        private void OnLastNameChanging(string value)
        {
        }

        private void OnNoGuestsChanging(int value)
        {
        }

        private void OnOrganizationChanging(string value)
        {
        }

        private void OnPhoneChanging(string value)
        {
        }

        /// <summary>
        /// There are no comments for Property Active in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                this.ReportPropertyChanging("Active");
                this._Active = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Active");
            }
        }

        /// <summary>
        /// There are no comments for Property AddedBy in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string AddedBy
        {
            get
            {
                return this._AddedBy;
            }
            set
            {
                this.OnAddedByChanging(value);
                this.ReportPropertyChanging("AddedBy");
                this._AddedBy = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AddedBy");
            }
        }

        /// <summary>
        /// There are no comments for Property AttendStatus in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int AttendStatus
        {
            get
            {
                return this._AttendStatus;
            }
            set
            {
                this.OnAttendStatusChanging(value);
                this.ReportPropertyChanging("AttendStatus");
                this._AttendStatus = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AttendStatus");
            }
        }

        public bool CanAdd
        {
            get
            {
                return true;
            }
        }

        public bool CanDelete
        {
            get
            {
                return true;
            }
        }

        public bool CanEdit
        {
            get
            {
                return true;
            }
        }

        public bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// There are no comments for Property DateAdded in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public DateTime DateAdded
        {
            get
            {
                return this._DateAdded;
            }
            set
            {
                this.ReportPropertyChanging("DateAdded");
                this._DateAdded = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("DateAdded");
            }
        }

        /// <summary>
        /// There are no comments for Property DateUpdated in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public DateTime DateUpdated
        {
            get
            {
                return this._DateUpdated;
            }
            set
            {
                this.ReportPropertyChanging("DateUpdated");
                this._DateUpdated = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("DateUpdated");
            }
        }

        /// <summary>
        /// There are no comments for Property EMail in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string EMail
        {
            get
            {
                return this._EMail;
            }
            set
            {
                this.OnEMailChanging(value);
                this.ReportPropertyChanging("EMail");
                this._EMail = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EMail");
            }
        }

        /// <summary>
        /// There are no comments for EventInfo in the schema.
        /// </summary>
        [SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("CalendarofEventsModel", "FK_EventRSVP_Event", "tbh_EventInfo"), DataMember]
        public TheBeerHouse.BLL.EventCalendar.EventInfo EventInfo
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.EventCalendar.EventInfo>("CalendarofEventsModel.FK_EventRSVP_Event", "tbh_EventInfo").Value;
            }
            set
            {
                this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.EventCalendar.EventInfo>("CalendarofEventsModel.FK_EventRSVP_Event", "tbh_EventInfo").Value = value;
            }
        }

        /// <summary>
        /// There are no comments for EventInfo in the schema.
        /// </summary>
        [DataMember, Browsable(false)]
        public EntityReference<TheBeerHouse.BLL.EventCalendar.EventInfo> EventInfoReference
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.EventCalendar.EventInfo>("CalendarofEventsModel.FK_EventRSVP_Event", "tbh_EventInfo");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedReference<TheBeerHouse.BLL.EventCalendar.EventInfo>("CalendarofEventsModel.FK_EventRSVP_Event", "tbh_EventInfo", value);
                }
            }
        }

        /// <summary>
        /// There are no comments for Property EventRSVPId in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int EventRSVPId
        {
            get
            {
                return this._EventRSVPId;
            }
            set
            {
                this.ReportPropertyChanging("EventRSVPId");
                this._EventRSVPId = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EventRSVPId");
            }
        }

        public string EventTitle
        {
            get
            {
                if (!Information.IsNothing(this.EventInfo))
                {
                    return this.EventInfo.EventTitle;
                }
                return "NA";
            }
        }

        /// <summary>
        /// There are no comments for Property FirstName in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this.OnFirstNameChanging(value);
                this.ReportPropertyChanging("FirstName");
                this._FirstName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FirstName");
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        /// <summary>
        /// There are no comments for Property GuestNames in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string GuestNames
        {
            get
            {
                return this._GuestNames;
            }
            set
            {
                this.OnGuestNamesChanging(value);
                this.ReportPropertyChanging("GuestNames");
                this._GuestNames = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("GuestNames");
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsDirty
        {
            get
            {
                return this.bIsDirty;
            }
            set
            {
                this.bIsDirty = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(this.FirstName) & !string.IsNullOrEmpty(this.LastName));
            }
        }

        /// <summary>
        /// There are no comments for Property JobTitle in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string JobTitle
        {
            get
            {
                return this._JobTitle;
            }
            set
            {
                this.OnJobTitleChanging(value);
                this.ReportPropertyChanging("JobTitle");
                this._JobTitle = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("JobTitle");
            }
        }

        /// <summary>
        /// There are no comments for Property LastName in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this.OnLastNameChanging(value);
                this.ReportPropertyChanging("LastName");
                this._LastName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// There are no comments for Property NoGuests in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int NoGuests
        {
            get
            {
                return this._NoGuests;
            }
            set
            {
                this.OnNoGuestsChanging(value);
                this.ReportPropertyChanging("NoGuests");
                this._NoGuests = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("NoGuests");
            }
        }

        /// <summary>
        /// There are no comments for Property Organization in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string Organization
        {
            get
            {
                return this._Organization;
            }
            set
            {
                this.OnOrganizationChanging(value);
                this.ReportPropertyChanging("Organization");
                this._Organization = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Organization");
            }
        }

        /// <summary>
        /// There are no comments for Property Phone in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                this.OnPhoneChanging(value);
                this.ReportPropertyChanging("Phone");
                this._Phone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Phone");
            }
        }

        /// <summary>
        /// Returns the name of the Data Set the Entity belongs to. Needs to be set
        /// in the derived class.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SetName
        {
            get
            {
                return this._setName;
            }
            set
            {
                this._setName = value;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanAdd
        {
            get
            {
                return true;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanDelete
        {
            get
            {
                return true;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanEdit
        {
            get
            {
                return true;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanRead
        {
            get
            {
                return true;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.IsDirty
        {
            get
            {
                return this.bIsDirty;
            }
            set
            {
                this.bIsDirty = value;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(this.FirstName) & !string.IsNullOrEmpty(this.LastName));
            }
        }

        public string TheBeerHouse.BLL.IBaseEntity.SetName
        {
            get
            {
                return this._setName;
            }
            set
            {
                this._setName = value;
            }
        }

        /// <summary>
        /// There are no comments for Property UpdatedBy in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string UpdatedBy
        {
            get
            {
                return this._UpdatedBy;
            }
            set
            {
                this.ReportPropertyChanging("UpdatedBy");
                this._UpdatedBy = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("UpdatedBy");
            }
        }
    }
}

