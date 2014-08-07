namespace TheBeerHouse.BLL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    /// <summary>
    /// There are no comments for BLL.SiteMapInfo in the schema.
    /// </summary>
    /// <KeyProperties>
    /// SiteMapId
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="BLL", Name="SiteMapInfo")]
    public class SiteMapInfo : EntityObject, IBaseEntity
    {
        private bool _Active = true;
        private string _AddedBy;
        private DateTime _DateAdded;
        private DateTime _DateUpdated;
        private string _Description;
        private string _Keywords;
        private int _NodeType;
        private int _Parent;
        private string _RealURL;
        private string _Roles;
        private string _SetName = "SiteMap";
        private int _SiteMapId;
        private int _SortOrder;
        private string _Title;
        private string _UpdatedBy;
        private string _URL;
        private int _URLId;
        private string _URLType;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new SiteMapInfo object.
        /// </summary>
        /// <param name="siteMapId">Initial value of SiteMapId.</param>
        /// <param name="uRL">Initial value of URL.</param>
        /// <param name="realURL">Initial value of RealURL.</param>
        /// <param name="title">Initial value of Title.</param>
        /// <param name="keywords">Initial value of Keywords.</param>
        /// <param name="description">Initial value of Description.</param>
        /// <param name="parent">Initial value of Parent.</param>
        /// <param name="uRLId">Initial value of URLId.</param>
        /// <param name="nodeType">Initial value of NodeType.</param>
        /// <param name="sortOrder">Initial value of SortOrder.</param>
        /// <param name="dateAdded">Initial value of DateAdded.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="dateUpdated">Initial value of DateUpdated.</param>
        /// <param name="updatedBy">Initial value of UpdatedBy.</param>
        public static SiteMapInfo CreateSiteMapInfo(int siteMapId, string uRL, string realURL, string title, string keywords, string description, int parent, int uRLId, int nodeType, int sortOrder, DateTime dateAdded, string addedBy, DateTime dateUpdated, string updatedBy)
        {
            SiteMapInfo siteMapInfo = new SiteMapInfo();
            siteMapInfo.SiteMapId = siteMapId;
            siteMapInfo.URL = uRL;
            siteMapInfo.RealURL = realURL;
            siteMapInfo.Title = title;
            siteMapInfo.Keywords = keywords;
            siteMapInfo.Description = description;
            siteMapInfo.Parent = parent;
            siteMapInfo.URLId = uRLId;
            siteMapInfo.NodeType = nodeType;
            siteMapInfo.SortOrder = sortOrder;
            siteMapInfo.DateAdded = dateAdded;
            siteMapInfo.AddedBy = addedBy;
            siteMapInfo.DateUpdated = dateUpdated;
            siteMapInfo.UpdatedBy = updatedBy;
            return siteMapInfo;
        }

        private void OnSiteMapIdChanging(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("The SiteMapId cannot be less than 0.");
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
        /// Represents a URL in the site.
        /// </summary>
        /// <LongDescription>
        /// This entity represents a node in the Site's overall map. It will be used to build a SiteMapNode in the custom SiteMap provider, nodes in the SiteMaps.org custom handler, etc.
        /// </LongDescription>
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
        /// There are no comments for Property Description in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.ReportPropertyChanging("Description");
                this._Description = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Description");
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

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(this.URL) & !string.IsNullOrEmpty(this.RealURL)) & !string.IsNullOrEmpty(this.Title));
            }
        }

        /// <summary>
        /// There are no comments for Property Keywords in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string Keywords
        {
            get
            {
                return this._Keywords;
            }
            set
            {
                this.ReportPropertyChanging("Keywords");
                this._Keywords = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Keywords");
            }
        }

        /// <summary>
        /// There are no comments for Property NodeType in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int NodeType
        {
            get
            {
                return this._NodeType;
            }
            set
            {
                this.ReportPropertyChanging("NodeType");
                this._NodeType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("NodeType");
            }
        }

        /// <summary>
        /// There are no comments for Property Parent in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int Parent
        {
            get
            {
                return this._Parent;
            }
            set
            {
                this.ReportPropertyChanging("Parent");
                this._Parent = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Parent");
            }
        }

        /// <summary>
        /// There are no comments for Property RealURL in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string RealURL
        {
            get
            {
                return this._RealURL;
            }
            set
            {
                this.ReportPropertyChanging("RealURL");
                this._RealURL = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RealURL");
            }
        }

        /// <summary>
        /// There are no comments for Property Roles in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string Roles
        {
            get
            {
                return this._Roles;
            }
            set
            {
                this.ReportPropertyChanging("Roles");
                this._Roles = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Roles");
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
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

        /// <summary>
        /// There are no comments for Property SiteMapId in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int SiteMapId
        {
            get
            {
                return this._SiteMapId;
            }
            set
            {
                this.OnSiteMapIdChanging(value);
                this.ReportPropertyChanging("SiteMapId");
                this._SiteMapId = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SiteMapId");
            }
        }

        /// <summary>
        /// There are no comments for Property SortOrder in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                this.ReportPropertyChanging("SortOrder");
                this._SortOrder = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SortOrder");
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
                return ((!string.IsNullOrEmpty(this.URL) & !string.IsNullOrEmpty(this.RealURL)) & !string.IsNullOrEmpty(this.Title));
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
        /// There are no comments for Property URL in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string URL
        {
            get
            {
                return this._URL;
            }
            set
            {
                this.ReportPropertyChanging("URL");
                this._URL = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("URL");
            }
        }

        /// <summary>
        /// There are no comments for Property URLId in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int URLId
        {
            get
            {
                return this._URLId;
            }
            set
            {
                this.ReportPropertyChanging("URLId");
                this._URLId = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("URLId");
            }
        }

        /// <summary>
        /// There are no comments for Property URLType in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string URLType
        {
            get
            {
                return this._URLType;
            }
            set
            {
                this.ReportPropertyChanging("URLType");
                this._URLType = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("URLType");
            }
        }
    }
}

