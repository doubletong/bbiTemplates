namespace TheBeerHouse.BLL.Newsletters
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Threading;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for NewsletterModel.Newsletter in the schema.
    /// </summary>
    /// <KeyProperties>
    /// NewsletterID
    /// </KeyProperties>
    [Serializable, EdmEntityType(NamespaceName="NewsletterModel", Name="Newsletter"), DataContract(IsReference=true)]
    public class Newsletter : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private DateTime? _DateSent;
        private string _HtmlBody;
        private static bool _isSending = false;
        private int _NewsletterID;
        private string _PlainTextBody;
        private static int _sentMails = 0;
        private string _setName = "tbh_Newsletterss";
        private string _Subject;
        private static int _totalMails = -1;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private bool bIsDirty = false;
        public static ReaderWriterLock Lock = new ReaderWriterLock();

        /// <summary>
        /// Create a new Newsletter object.
        /// </summary>
        /// <param name="newsletterID">Initial value of NewsletterID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="subject">Initial value of Subject.</param>
        /// <param name="plainTextBody">Initial value of PlainTextBody.</param>
        /// <param name="htmlBody">Initial value of HtmlBody.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static Newsletter CreateNewsletter(int newsletterID, DateTime addedDate, string addedBy, string subject, string plainTextBody, string htmlBody, DateTime updatedDate, bool active)
        {
            Newsletter newsletter = new Newsletter();
            newsletter.NewsletterID = newsletterID;
            newsletter.AddedDate = addedDate;
            newsletter.AddedBy = addedBy;
            newsletter.Subject = subject;
            newsletter.PlainTextBody = plainTextBody;
            newsletter.HtmlBody = htmlBody;
            newsletter.UpdatedDate = updatedDate;
            newsletter.Active = active;
            return newsletter;
        }

        private void OnAddedByChanging(string value)
        {
            if (!string.IsNullOrEmpty(this.AddedBy))
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
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
        /// There are no comments for Property DateSent in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public DateTime? DateSent
        {
            get
            {
                return this._DateSent;
            }
            set
            {
                this.ReportPropertyChanging("DateSent");
                this._DateSent = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("DateSent");
            }
        }

        /// <summary>
        /// There are no comments for Property HtmlBody in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string HtmlBody
        {
            get
            {
                return this._HtmlBody;
            }
            set
            {
                this.ReportPropertyChanging("HtmlBody");
                this._HtmlBody = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("HtmlBody");
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

        public static bool IsSending
        {
            get
            {
                return _isSending;
            }
            set
            {
                _isSending = value;
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
                return (!string.IsNullOrEmpty(this.Subject) & (!string.IsNullOrEmpty(this.PlainTextBody) | !string.IsNullOrEmpty(this.HtmlBody)));
            }
        }

        /// <summary>
        /// There are no comments for Property NewsletterID in the schema.
        /// </summary>
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int NewsletterID
        {
            get
            {
                return this._NewsletterID;
            }
            set
            {
                this.ReportPropertyChanging("NewsletterID");
                this._NewsletterID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("NewsletterID");
            }
        }

        public static double PercentageCompleted
        {
            get
            {
                if (TotalMails == 0)
                {
                    return 0.0;
                }
                return ((SentMails * 100.0) / ((double) TotalMails));
            }
        }

        /// <summary>
        /// There are no comments for Property PlainTextBody in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string PlainTextBody
        {
            get
            {
                return this._PlainTextBody;
            }
            set
            {
                this.ReportPropertyChanging("PlainTextBody");
                this._PlainTextBody = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PlainTextBody");
            }
        }

        public static int SentMails
        {
            get
            {
                return _sentMails;
            }
            set
            {
                _sentMails = value;
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
        /// There are no comments for Property Subject in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string Subject
        {
            get
            {
                return this._Subject;
            }
            set
            {
                this.ReportPropertyChanging("Subject");
                this._Subject = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Subject");
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
                return (!string.IsNullOrEmpty(this.Subject) & (!string.IsNullOrEmpty(this.PlainTextBody) | !string.IsNullOrEmpty(this.HtmlBody)));
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

        public static int TotalMails
        {
            get
            {
                return _totalMails;
            }
            set
            {
                _totalMails = value;
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
                this.ReportPropertyChanging("UpdatedDate");
                this._UpdatedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UpdatedDate");
            }
        }
    }
}

