namespace TheBeerHouse.BLL.Forums
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for ForumsModel.Forum in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ForumID
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ForumsModel", Name="Forum")]
    public class Forum : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private string _Description;
        private int _ForumID;
        private string _ImageUrl;
        private int _Importance;
        private bool _Moderated;
        private string _postBody;
        private string _setName = "Forums";
        private string _Title;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new Forum object.
        /// </summary>
        /// <param name="forumID">Initial value of ForumID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="title">Initial value of Title.</param>
        /// <param name="moderated">Initial value of Moderated.</param>
        /// <param name="importance">Initial value of Importance.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static Forum CreateForum(int forumID, DateTime addedDate, string addedBy, string title, bool moderated, int importance, DateTime updatedDate, bool active)
        {
            Forum forum = new Forum();
            forum.ForumID = forumID;
            forum.AddedDate = addedDate;
            forum.AddedBy = addedBy;
            forum.Title = title;
            forum.Moderated = moderated;
            forum.Importance = importance;
            forum.UpdatedDate = updatedDate;
            forum.Active = active;
            return forum;
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

        private void OnDescriptionChanging(string value)
        {
        }

        private void OnImageUrlChanging(string value)
        {
        }

        private void OnImportanceChanging(int value)
        {
        }

        private void OnModeratedChanging(bool value)
        {
        }

        private void OnTitleChanging(string value)
        {
        }

        private void OnUpdatedDateChanging(DateTime value)
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
        /// There are no comments for Property Description in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.OnDescriptionChanging(value);
                this.ReportPropertyChanging("Description");
                this._Description = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Description");
            }
        }

        /// <summary>
        /// There are no comments for Property ForumID in the schema.
        /// </summary>
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int ForumID
        {
            get
            {
                return this._ForumID;
            }
            set
            {
                this.ReportPropertyChanging("ForumID");
                this._ForumID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ForumID");
            }
        }

        /// <summary>
        /// There are no comments for Property ImageUrl in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string ImageUrl
        {
            get
            {
                return this._ImageUrl;
            }
            set
            {
                this.OnImageUrlChanging(value);
                this.ReportPropertyChanging("ImageUrl");
                this._ImageUrl = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ImageUrl");
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
                return (!string.IsNullOrEmpty(this.Title) & !string.IsNullOrEmpty(this.Description));
            }
        }

        /// <summary>
        /// There are no comments for Property Moderated in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public bool Moderated
        {
            get
            {
                return this._Moderated;
            }
            set
            {
                this.OnModeratedChanging(value);
                this.ReportPropertyChanging("Moderated");
                this._Moderated = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Moderated");
            }
        }

        public string PostBody
        {
            get
            {
                return this._postBody;
            }
            set
            {
                this._postBody = value;
            }
        }

        /// <summary>
        /// There are no comments for Posts in the schema.
        /// </summary>
        [SoapIgnore, EdmRelationshipNavigationProperty("ForumsModel", "FK_tbh_Posts_tbh_Forums", "tbh_Posts"), DataMember, XmlIgnore]
        public EntityCollection<Post> Posts
        {
            get
            {
                return this.RelationshipManager.GetRelatedCollection<Post>("ForumsModel.FK_tbh_Posts_tbh_Forums", "tbh_Posts");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedCollection<Post>("ForumsModel.FK_tbh_Posts_tbh_Forums", "tbh_Posts", value);
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
                return (!string.IsNullOrEmpty(this.Title) & !string.IsNullOrEmpty(this.Description));
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
        /// There are no comments for Property Title in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this.OnTitleChanging(value);
                this.ReportPropertyChanging("Title");
                this._Title = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Title");
            }
        }

        /// <summary>
        /// There are no comments for Property UpdatedBy in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
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
                this.OnUpdatedDateChanging(value);
                this.ReportPropertyChanging("UpdatedDate");
                this._UpdatedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UpdatedDate");
            }
        }
    }
}

