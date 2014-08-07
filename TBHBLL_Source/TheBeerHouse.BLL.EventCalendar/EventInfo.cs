namespace TheBeerHouse.BLL.EventCalendar
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for CalendarofEventsModel.EventInfo in the schema.
    /// </summary>
    /// <KeyProperties>
    /// EventId
    /// </KeyProperties>
    [Serializable, EdmEntityType(NamespaceName="CalendarofEventsModel", Name="EventInfo"), DataContract(IsReference=true)]
    public class EventInfo : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private string _Address;
        private bool _AllowRegistration;
        private string _Attachment;
        private string _City;
        private DateTime _DateAdded;
        private DateTime _DateUpdated;
        private string _Duration;
        private string _EndTime;
        private DateTime _EventDate;
        private string _EventDesc;
        private DateTime? _EventEndDate;
        private DateTime _EventExpires;
        private int _EventId;
        private string _EventLocation;
        private string _EventTime;
        private string _EventTitle;
        private bool? _Featured;
        private int _Importance;
        private string _setName = "Events";
        private string _State;
        private string _UpdatedBy;
        private string _ZipCode;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new EventInfo object.
        /// </summary>
        /// <param name="eventId">Initial value of EventId.</param>
        /// <param name="eventTitle">Initial value of EventTitle.</param>
        /// <param name="eventDesc">Initial value of EventDesc.</param>
        /// <param name="eventDate">Initial value of EventDate.</param>
        /// <param name="eventExpires">Initial value of EventExpires.</param>
        /// <param name="eventTime">Initial value of EventTime.</param>
        /// <param name="importance">Initial value of Importance.</param>
        /// <param name="allowRegistration">Initial value of AllowRegistration.</param>
        /// <param name="active">Initial value of Active.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="dateAdded">Initial value of DateAdded.</param>
        /// <param name="updatedBy">Initial value of UpdatedBy.</param>
        /// <param name="dateUpdated">Initial value of DateUpdated.</param>
        public static EventInfo CreateEventInfo(int eventId, string eventTitle, string eventDesc, DateTime eventDate, DateTime eventExpires, string eventTime, int importance, bool allowRegistration, bool active, string addedBy, DateTime dateAdded, string updatedBy, DateTime dateUpdated)
        {
            EventInfo eventInfo = new EventInfo();
            eventInfo.EventId = eventId;
            eventInfo.EventTitle = eventTitle;
            eventInfo.EventDesc = eventDesc;
            eventInfo.EventDate = eventDate;
            eventInfo.EventExpires = eventExpires;
            eventInfo.EventTime = eventTime;
            eventInfo.Importance = importance;
            eventInfo.AllowRegistration = allowRegistration;
            eventInfo.Active = active;
            eventInfo.AddedBy = addedBy;
            eventInfo.DateAdded = dateAdded;
            eventInfo.UpdatedBy = updatedBy;
            eventInfo.DateUpdated = dateUpdated;
            return eventInfo;
        }

        private void OnAddedByChanging(string value)
        {
            if (!string.IsNullOrEmpty(this.AddedBy))
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

        private void OnAllowRegistrationChanging(bool value)
        {
        }

        private void OnAttachmentChanging(string value)
        {
        }

        private void OnDurationChanging(string value)
        {
        }

        private void OnEndTimeChanging(DateTime value)
        {
        }

        private void OnEventDateChanging(DateTime value)
        {
        }

        private void OnEventDescChanging(string value)
        {
        }

        private void OnEventEndDateChanging(DateTime value)
        {
        }

        private void OnEventExpiresChanging(DateTime value)
        {
        }

        private void OnEventLocationChanging(string value)
        {
        }

        private void OnEventStatusChanging(int value)
        {
        }

        private void OnEventTimeChanging(string value)
        {
        }

        private void OnEventTitleChanging(string value)
        {
        }

        private void OnEventTypeChanging(int value)
        {
        }

        private void OnFeaturedChanging(bool value)
        {
        }

        private void OnImportanceChanging(int value)
        {
        }

        private void OnRepeatTypeChanging(string value)
        {
        }

        private void OnRolesChanging(string value)
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
        [EdmScalarProperty(IsNullable=false), DataMember]
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
        /// There are no comments for Property Address in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this.ReportPropertyChanging("Address");
                this._Address = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Address");
            }
        }

        /// <summary>
        /// There are no comments for Property AllowRegistration in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public bool AllowRegistration
        {
            get
            {
                return this._AllowRegistration;
            }
            set
            {
                this.OnAllowRegistrationChanging(value);
                this.ReportPropertyChanging("AllowRegistration");
                this._AllowRegistration = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AllowRegistration");
            }
        }

        /// <summary>
        /// There are no comments for Property Attachment in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string Attachment
        {
            get
            {
                return this._Attachment;
            }
            set
            {
                this.OnAttachmentChanging(value);
                this.ReportPropertyChanging("Attachment");
                this._Attachment = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Attachment");
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
        /// There are no comments for Property City in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this.ReportPropertyChanging("City");
                this._City = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("City");
            }
        }

        /// <summary>
        /// There are no comments for Property DateAdded in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
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
        [DataMember, EdmScalarProperty(IsNullable=false)]
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
        /// There are no comments for Property Duration in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string Duration
        {
            get
            {
                return this._Duration;
            }
            set
            {
                this.OnDurationChanging(value);
                this.ReportPropertyChanging("Duration");
                this._Duration = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Duration");
            }
        }

        /// <summary>
        /// There are no comments for Property EndTime in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                this.ReportPropertyChanging("EndTime");
                this._EndTime = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EndTime");
            }
        }

        /// <summary>
        /// There are no comments for Property EventDate in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public DateTime EventDate
        {
            get
            {
                return this._EventDate;
            }
            set
            {
                this.OnEventDateChanging(value);
                this.ReportPropertyChanging("EventDate");
                this._EventDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EventDate");
            }
        }

        /// <summary>
        /// There are no comments for Property EventDesc in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string EventDesc
        {
            get
            {
                return this._EventDesc;
            }
            set
            {
                this.OnEventDescChanging(value);
                this.ReportPropertyChanging("EventDesc");
                this._EventDesc = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("EventDesc");
            }
        }

        /// <summary>
        /// There are no comments for Property EventEndDate in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public DateTime? EventEndDate
        {
            get
            {
                return this._EventEndDate;
            }
            set
            {
                this.ReportPropertyChanging("EventEndDate");
                this._EventEndDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EventEndDate");
            }
        }

        /// <summary>
        /// There are no comments for Property EventExpires in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public DateTime EventExpires
        {
            get
            {
                return this._EventExpires;
            }
            set
            {
                this.OnEventExpiresChanging(value);
                this.ReportPropertyChanging("EventExpires");
                this._EventExpires = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EventExpires");
            }
        }

        /// <summary>
        /// There are no comments for Property EventId in the schema.
        /// </summary>
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int EventId
        {
            get
            {
                return this._EventId;
            }
            set
            {
                this.ReportPropertyChanging("EventId");
                this._EventId = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EventId");
            }
        }

        /// <summary>
        /// There are no comments for Property EventLocation in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string EventLocation
        {
            get
            {
                return this._EventLocation;
            }
            set
            {
                this.OnEventLocationChanging(value);
                this.ReportPropertyChanging("EventLocation");
                this._EventLocation = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EventLocation");
            }
        }

        /// <summary>
        /// There are no comments for EventRSVPs in the schema.
        /// </summary>
        [EdmRelationshipNavigationProperty("CalendarofEventsModel", "FK_EventRSVP_Event", "tbh_EventRSVP"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<EventRSVP> EventRSVPs
        {
            get
            {
                return this.RelationshipManager.GetRelatedCollection<EventRSVP>("CalendarofEventsModel.FK_EventRSVP_Event", "tbh_EventRSVP");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedCollection<EventRSVP>("CalendarofEventsModel.FK_EventRSVP_Event", "tbh_EventRSVP", value);
                }
            }
        }

        /// <summary>
        /// There are no comments for Property EventTime in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string EventTime
        {
            get
            {
                return this._EventTime;
            }
            set
            {
                this.OnEventTimeChanging(value);
                this.ReportPropertyChanging("EventTime");
                this._EventTime = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("EventTime");
            }
        }

        /// <summary>
        /// There are no comments for Property EventTitle in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string EventTitle
        {
            get
            {
                return this._EventTitle;
            }
            set
            {
                this.OnEventTitleChanging(value);
                this.ReportPropertyChanging("EventTitle");
                this._EventTitle = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("EventTitle");
            }
        }

        /// <summary>
        /// There are no comments for Property Featured in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public bool? Featured
        {
            get
            {
                return this._Featured;
            }
            set
            {
                this.ReportPropertyChanging("Featured");
                this._Featured = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Featured");
            }
        }

        /// <summary>
        /// There are no comments for Property Importance in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int Importance
        {
            get
            {
                return this._Importance;
            }
            set
            {
                this.OnImportanceChanging(value);
                this.ReportPropertyChanging("Importance");
                this._Importance = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Importance");
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
                return ((Helpers.IsValidString(this.EventTitle, Conversions.ToString(1), 100) & !string.IsNullOrEmpty(this.EventDesc)) & (DateTime.Compare(this.EventDate, DateTime.MinValue) > 0));
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

        /// <summary>
        /// There are no comments for Property State in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string State
        {
            get
            {
                return this._State;
            }
            set
            {
                this.ReportPropertyChanging("State");
                this._State = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("State");
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
                return ((Helpers.IsValidString(this.EventTitle, Conversions.ToString(1), 100) & !string.IsNullOrEmpty(this.EventDesc)) & (DateTime.Compare(this.EventDate, DateTime.MinValue) > 0));
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

        /// <summary>
        /// There are no comments for Property ZipCode in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string ZipCode
        {
            get
            {
                return this._ZipCode;
            }
            set
            {
                this.ReportPropertyChanging("ZipCode");
                this._ZipCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ZipCode");
            }
        }
    }
}

