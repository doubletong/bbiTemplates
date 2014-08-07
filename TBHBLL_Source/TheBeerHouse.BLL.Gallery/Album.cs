namespace TheBeerHouse.BLL.Gallery
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for TheBeerHouseVBModel.Album in the schema.
    /// </summary>
    /// <KeyProperties>
    /// AlbumID
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="TheBeerHouseVBModel", Name="Album")]
    public class Album : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AlbumCreateDate;
        private string _AlbumDesc;
        private int _AlbumID;
        private string _AlbumName;
        private DateTime _DateAdded;
        private DateTime _DateUpdated;
        private int _ParentAlbumID;
        private Picture _SamplePicture;
        private string _setName = "Albums";
        private string _UpdatedBy;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new Album object.
        /// </summary>
        /// <param name="albumID">Initial value of AlbumID.</param>
        /// <param name="albumCreateDate">Initial value of AlbumCreateDate.</param>
        /// <param name="parentAlbumID">Initial value of ParentAlbumID.</param>
        /// <param name="active">Initial value of Active.</param>
        /// <param name="dateAdded">Initial value of DateAdded.</param>
        /// <param name="dateUpdated">Initial value of DateUpdated.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="updatedBy">Initial value of UpdatedBy.</param>
        public static Album CreateAlbum(int albumID, DateTime albumCreateDate, int parentAlbumID, bool active, DateTime dateAdded, DateTime dateUpdated, string addedBy, string updatedBy)
        {
            Album album = new Album();
            album.AlbumID = albumID;
            album.AlbumCreateDate = albumCreateDate;
            album.ParentAlbumID = parentAlbumID;
            album.Active = active;
            album.DateAdded = dateAdded;
            album.DateUpdated = dateUpdated;
            album.AddedBy = addedBy;
            album.UpdatedBy = updatedBy;
            return album;
        }

        private void OnAddedByChanging(string value)
        {
            if (!string.IsNullOrEmpty(this.AddedBy))
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

        private void OnAlbumCreateDateChanging(DateTime value)
        {
        }

        private void OnAlbumDescChanging(string value)
        {
        }

        private void OnAlbumNameChanging(string value)
        {
        }

        private void OnParentAlbumIDChanging(int value)
        {
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
        /// There are no comments for Property AlbumCreateDate in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public DateTime AlbumCreateDate
        {
            get
            {
                return this._AlbumCreateDate;
            }
            set
            {
                this.OnAlbumCreateDateChanging(value);
                this.ReportPropertyChanging("AlbumCreateDate");
                this._AlbumCreateDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AlbumCreateDate");
            }
        }

        /// <summary>
        /// There are no comments for Property AlbumDesc in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string AlbumDesc
        {
            get
            {
                return this._AlbumDesc;
            }
            set
            {
                this.OnAlbumDescChanging(value);
                this.ReportPropertyChanging("AlbumDesc");
                this._AlbumDesc = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("AlbumDesc");
            }
        }

        /// <summary>
        /// There are no comments for Property AlbumID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int AlbumID
        {
            get
            {
                return this._AlbumID;
            }
            set
            {
                this.ReportPropertyChanging("AlbumID");
                this._AlbumID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AlbumID");
            }
        }

        /// <summary>
        /// There are no comments for Property AlbumName in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string AlbumName
        {
            get
            {
                return this._AlbumName;
            }
            set
            {
                this.OnAlbumNameChanging(value);
                this.ReportPropertyChanging("AlbumName");
                this._AlbumName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("AlbumName");
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
                return (!string.IsNullOrEmpty(this.AlbumName) & !string.IsNullOrEmpty(this.AlbumDesc));
            }
        }

        /// <summary>
        /// There are no comments for Property ParentAlbumID in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int ParentAlbumID
        {
            get
            {
                return this._ParentAlbumID;
            }
            set
            {
                this.OnParentAlbumIDChanging(value);
                this.ReportPropertyChanging("ParentAlbumID");
                this._ParentAlbumID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ParentAlbumID");
            }
        }

        /// <summary>
        /// There are no comments for Pictures in the schema.
        /// </summary>
        [DataMember, SoapIgnore, EdmRelationshipNavigationProperty("TheBeerHouseVBModel", "FK_Pictures_Album", "tbh_Pictures"), XmlIgnore]
        public EntityCollection<Picture> Pictures
        {
            get
            {
                return this.RelationshipManager.GetRelatedCollection<Picture>("TheBeerHouseVBModel.FK_Pictures_Album", "tbh_Pictures");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedCollection<Picture>("TheBeerHouseVBModel.FK_Pictures_Album", "tbh_Pictures", value);
                }
            }
        }

        public Picture SamplePicture
        {
            get
            {
                return this._SamplePicture;
            }
            set
            {
                this._SamplePicture = value;
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
                return (!string.IsNullOrEmpty(this.AlbumName) & !string.IsNullOrEmpty(this.AlbumDesc));
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
        [EdmScalarProperty(IsNullable=false), DataMember]
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

