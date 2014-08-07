namespace TheBeerHouse.BLL
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// There are no comments for TheBeerHousePolling.PollOption in the schema.
    /// </summary>
    /// <KeyProperties>
    /// OptionID
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="TheBeerHousePolling", Name="PollOption")]
    public class PollOption : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private int _OptionID;
        private string _OptionText;
        private string _setName = "PollOptions";
        private double _TotalVotes = 0.0;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private int _Votes;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new PollOption object.
        /// </summary>
        /// <param name="optionID">Initial value of OptionID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="optionText">Initial value of OptionText.</param>
        /// <param name="votes">Initial value of Votes.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static PollOption CreatePollOption(int optionID, DateTime addedDate, string addedBy, string optionText, int votes, DateTime updatedDate, bool active)
        {
            PollOption pollOption = new PollOption();
            pollOption.OptionID = optionID;
            pollOption.AddedDate = addedDate;
            pollOption.AddedBy = addedBy;
            pollOption.OptionText = optionText;
            pollOption.Votes = votes;
            pollOption.UpdatedDate = updatedDate;
            pollOption.Active = active;
            return pollOption;
        }

        private void OnAddedByChanging(string value)
        {
            if (!string.IsNullOrEmpty(this.AddedBy))
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

        private void OnAddedDateChanging(DateTime value)
        {
        }

        private void OnOptionTextChanging(string value)
        {
        }

        private void OnPollIDChanging(int value)
        {
        }

        private void OnUpdatedDateChanging(DateTime value)
        {
        }

        private void OnVotesChanging(int value)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this.PollId, this.OptionText, this.Votes);
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
        /// There are no comments for Property AddedDate in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public DateTime AddedDate
        {
            get
            {
                return this._AddedDate;
            }
            set
            {
                this.OnAddedDateChanging(value);
                this.ReportPropertyChanging("AddedDate");
                this._AddedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AddedDate");
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
                return (!string.IsNullOrEmpty(this.OptionText) & (this.PollId > 0));
            }
        }

        /// <summary>
        /// There are no comments for Property OptionID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int OptionID
        {
            get
            {
                return this._OptionID;
            }
            set
            {
                this.ReportPropertyChanging("OptionID");
                this._OptionID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OptionID");
            }
        }

        /// <summary>
        /// There are no comments for Property OptionText in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string OptionText
        {
            get
            {
                return this._OptionText;
            }
            set
            {
                this.OnOptionTextChanging(value);
                this.ReportPropertyChanging("OptionText");
                this._OptionText = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("OptionText");
            }
        }

        public double Percentage
        {
            get
            {
                if (this.TotalVotes == 0)
                {
                    return -1.0;
                }
                return (((double) (this.Votes * 100)) / ((double) this.TotalVotes));
            }
        }

        /// <summary>
        /// There are no comments for Poll in the schema.
        /// </summary>
        [DataMember, EdmRelationshipNavigationProperty("TheBeerHousePolling", "FK_tbh_PollOptions_tbh_Polls", "tbh_Polls"), SoapIgnore, XmlIgnore]
        public TheBeerHouse.BLL.Poll Poll
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Poll>("TheBeerHousePolling.FK_tbh_PollOptions_tbh_Polls", "tbh_Polls").Value;
            }
            set
            {
                this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Poll>("TheBeerHousePolling.FK_tbh_PollOptions_tbh_Polls", "tbh_Polls").Value = value;
            }
        }

        public int PollId
        {
            get
            {
                if (!Information.IsNothing(this.PollReference.EntityKey))
                {
                    return Conversions.ToInteger(this.PollReference.EntityKey.EntityKeyValues[0].Value);
                }
                return 0;
            }
            set
            {
                if (!Information.IsNothing(this.PollReference.EntityKey))
                {
                    this.PollReference = null;
                }
                this.PollReference.EntityKey = new EntityKey("PollEntities.Polls", "PollID", value);
            }
        }

        /// <summary>
        /// There are no comments for Poll in the schema.
        /// </summary>
        [Browsable(false), DataMember]
        public EntityReference<TheBeerHouse.BLL.Poll> PollReference
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Poll>("TheBeerHousePolling.FK_tbh_PollOptions_tbh_Polls", "tbh_Polls");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedReference<TheBeerHouse.BLL.Poll>("TheBeerHousePolling.FK_tbh_PollOptions_tbh_Polls", "tbh_Polls", value);
                }
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
                return (!string.IsNullOrEmpty(this.OptionText) & (this.PollId > 0));
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

        public int TotalVotes
        {
            get
            {
                if (!Information.IsNothing(this.Poll))
                {
                    this._TotalVotes = this.Poll.Votes;
                }
                return (int) Math.Round(this._TotalVotes);
            }
            set
            {
                this._TotalVotes = value;
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
        [EdmScalarProperty(IsNullable=false), DataMember]
        public DateTime UpdatedDate
        {
            get
            {
                return this._UpdatedDate;
            }
            set
            {
                this.OnUpdatedDateChanging(value);
                this.ReportPropertyChanging("UpdatedDate");
                this._UpdatedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UpdatedDate");
            }
        }

        /// <summary>
        /// There are no comments for Property Votes in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int Votes
        {
            get
            {
                return this._Votes;
            }
            set
            {
                this.OnVotesChanging(value);
                this.ReportPropertyChanging("Votes");
                this._Votes = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Votes");
            }
        }
    }
}

