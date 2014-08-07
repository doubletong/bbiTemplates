namespace TheBeerHouse.BLL
{
    using Microsoft.VisualBasic;
    using System;
    using System.Data.Objects.DataClasses;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse;

    /// <summary>
    /// There are no comments for TheBeerHousePolling.Poll in the schema.
    /// </summary>
    /// <KeyProperties>
    /// PollID
    /// </KeyProperties>
    [Serializable, EdmEntityType(NamespaceName="TheBeerHousePolling", Name="Poll"), DataContract(IsReference=true)]
    public class Poll : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private DateTime? _ArchivedDate;
        private bool _IsArchived;
        private bool _IsCurrent;
        private bool _isDirty = false;
        private int _PollID;
        private string _QuestionText;
        private string _SetName;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private int _Votes = 0;

        [DebuggerStepThrough, CompilerGenerated]
        private static VB$AnonymousType_0<int> _Lambda$__19(PollOption po)
        {
            return new VB$AnonymousType_0<int>(po.Votes);
        }

        [CompilerGenerated, DebuggerStepThrough]
        private static int _Lambda$__20(VB$AnonymousType_0<int> p)
        {
            return p.Votes;
        }

        /// <summary>
        /// Create a new Poll object.
        /// </summary>
        /// <param name="pollID">Initial value of PollID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="questionText">Initial value of QuestionText.</param>
        /// <param name="isCurrent">Initial value of IsCurrent.</param>
        /// <param name="isArchived">Initial value of IsArchived.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static Poll CreatePoll(int pollID, DateTime addedDate, string addedBy, string questionText, bool isCurrent, bool isArchived, DateTime updatedDate, bool active)
        {
            Poll poll = new Poll();
            poll.PollID = pollID;
            poll.AddedDate = addedDate;
            poll.AddedBy = addedBy;
            poll.QuestionText = questionText;
            poll.IsCurrent = isCurrent;
            poll.IsArchived = isArchived;
            poll.UpdatedDate = updatedDate;
            poll.Active = active;
            return poll;
        }

        /// <summary>
        /// There are no comments for Property Active in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
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
                this.ReportPropertyChanging("AddedBy");
                this._AddedBy = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AddedBy");
            }
        }

        /// <summary>
        /// There are no comments for Property AddedDate in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public DateTime AddedDate
        {
            get
            {
                return this._AddedDate;
            }
            set
            {
                this.ReportPropertyChanging("AddedDate");
                this._AddedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AddedDate");
            }
        }

        /// <summary>
        /// There are no comments for Property ArchivedDate in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public DateTime? ArchivedDate
        {
            get
            {
                return this._ArchivedDate;
            }
            set
            {
                this.ReportPropertyChanging("ArchivedDate");
                this._ArchivedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ArchivedDate");
            }
        }

        public bool CanAdd
        {
            get
            {
                return (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"));
            }
        }

        public bool CanDelete
        {
            get
            {
                return (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"));
            }
        }

        public bool CanEdit
        {
            get
            {
                return (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"));
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
        /// There are no comments for Property IsArchived in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public bool IsArchived
        {
            get
            {
                return this._IsArchived;
            }
            set
            {
                this.ReportPropertyChanging("IsArchived");
                this._IsArchived = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsArchived");
            }
        }

        /// <summary>
        /// There are no comments for Property IsCurrent in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public bool IsCurrent
        {
            get
            {
                return this._IsCurrent;
            }
            set
            {
                this.ReportPropertyChanging("IsCurrent");
                this._IsCurrent = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsCurrent");
            }
        }

        public bool IsDirty
        {
            get
            {
                return this._isDirty;
            }
            set
            {
                this._isDirty = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(this.QuestionText);
            }
        }

        /// <summary>
        /// There are no comments for Property PollID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int PollID
        {
            get
            {
                return this._PollID;
            }
            set
            {
                this.ReportPropertyChanging("PollID");
                this._PollID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PollID");
            }
        }

        /// <summary>
        /// There are no comments for PollOptions in the schema.
        /// </summary>
        [DataMember, SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("TheBeerHousePolling", "FK_tbh_PollOptions_tbh_Polls", "tbh_PollOptions")]
        public EntityCollection<PollOption> PollOptions
        {
            get
            {
                return this.RelationshipManager.GetRelatedCollection<PollOption>("TheBeerHousePolling.FK_tbh_PollOptions_tbh_Polls", "tbh_PollOptions");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedCollection<PollOption>("TheBeerHousePolling.FK_tbh_PollOptions_tbh_Polls", "tbh_PollOptions", value);
                }
            }
        }

        /// <summary>
        /// There are no comments for Property QuestionText in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string QuestionText
        {
            get
            {
                return this._QuestionText;
            }
            set
            {
                this.ReportPropertyChanging("QuestionText");
                this._QuestionText = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("QuestionText");
            }
        }

        public string SetName
        {
            get
            {
                return this._SetName;
            }
            set
            {
                this._SetName = value;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanAdd
        {
            get
            {
                return (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"));
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanDelete
        {
            get
            {
                return (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"));
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanEdit
        {
            get
            {
                return (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"));
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
                return this._isDirty;
            }
            set
            {
                this._isDirty = value;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(this.QuestionText);
            }
        }

        public string TheBeerHouse.BLL.IBaseEntity.SetName
        {
            get
            {
                return this._SetName;
            }
            set
            {
                this._SetName = value;
            }
        }

        /// <summary>
        /// There are no comments for Property UpdatedBy in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string UpdatedBy
        {
            get
            {
                return this._UpdatedBy;
            }
            set
            {
                this.ReportPropertyChanging("UpdatedBy");
                this._UpdatedBy = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UpdatedBy");
            }
        }

        /// <summary>
        /// There are no comments for Property UpdatedDate in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public DateTime UpdatedDate
        {
            get
            {
                return this._UpdatedDate;
            }
            set
            {
                this.ReportPropertyChanging("UpdatedDate");
                this._UpdatedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UpdatedDate");
            }
        }

        public int Votes
        {
            get
            {
                if (this._Votes == 0)
                {
                    if (!Information.IsNothing(this.PollOptions))
                    {
                        this.PollOptions.Load();
                    }
                    this._Votes = this.PollOptions.Select<PollOption, VB$AnonymousType_0<int>>(new Func<PollOption, VB$AnonymousType_0<int>>(Poll._Lambda$__19)).Sum<VB$AnonymousType_0<int>>(new Func<VB$AnonymousType_0<int>, int>(Poll._Lambda$__20));
                }
                return this._Votes;
            }
            set
            {
                this._Votes = value;
            }
        }
    }
}

