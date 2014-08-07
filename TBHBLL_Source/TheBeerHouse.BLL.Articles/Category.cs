namespace TheBeerHouse.BLL.Articles
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for ArticlesModel.Category in the schema.
    /// </summary>
    /// <KeyProperties>
    /// CategoryID
    /// </KeyProperties>
    [Serializable, EdmEntityType(NamespaceName="ArticlesModel", Name="Category"), DataContract(IsReference=true)]
    public class Category : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private int _CategoryID;
        private string _Description;
        private string _ImageUrl;
        private int _Importance;
        private string _setName = "Categories";
        private string _Title;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new Category object.
        /// </summary>
        /// <param name="categoryID">Initial value of CategoryID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="title">Initial value of Title.</param>
        /// <param name="importance">Initial value of Importance.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static Category CreateCategory(int categoryID, DateTime addedDate, string addedBy, string title, int importance, DateTime updatedDate, bool active)
        {
            Category category = new Category();
            category.CategoryID = categoryID;
            category.AddedDate = addedDate;
            category.AddedBy = addedBy;
            category.Title = title;
            category.Importance = importance;
            category.UpdatedDate = updatedDate;
            category.Active = active;
            return category;
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
                this.ReportPropertyChanging("AddedDate");
                this._AddedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AddedDate");
            }
        }

        /// <summary>
        /// There are no comments for Articles in the schema.
        /// </summary>
        [DataMember, XmlIgnore, EdmRelationshipNavigationProperty("ArticlesModel", "FK_tbh_Articles_tbh_Categories", "tbh_Articles"), SoapIgnore]
        public EntityCollection<Article> Articles
        {
            get
            {
                return this.RelationshipManager.GetRelatedCollection<Article>("ArticlesModel.FK_tbh_Articles_tbh_Categories", "tbh_Articles");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedCollection<Article>("ArticlesModel.FK_tbh_Articles_tbh_Categories", "tbh_Articles", value);
                }
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
        /// There are no comments for Property CategoryID in the schema.
        /// </summary>
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int CategoryID
        {
            get
            {
                return this._CategoryID;
            }
            set
            {
                this.ReportPropertyChanging("CategoryID");
                this._CategoryID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CategoryID");
            }
        }

        /// <summary>
        /// There are no comments for Property Description in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.ReportPropertyChanging("Description");
                this._Description = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Description");
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
                this.ReportPropertyChanging("ImageUrl");
                this._ImageUrl = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ImageUrl");
            }
        }

        /// <summary>
        /// There are no comments for Property Importance in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int Importance
        {
            get
            {
                return this._Importance;
            }
            set
            {
                this.ReportPropertyChanging("Importance");
                this._Importance = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Importance");
            }
        }

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

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Title) & (this.Importance > -1))
                {
                    return false;
                }
                return true;
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
                if (string.IsNullOrEmpty(this.Title) & (this.Importance > -1))
                {
                    return false;
                }
                return true;
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
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
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

