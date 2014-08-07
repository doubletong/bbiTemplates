namespace TheBeerHouse.BLL.Gallery
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for TheBeerHouseVBModel.Picture in the schema.
    /// </summary>
    /// <KeyProperties>
    /// PictureID
    /// </KeyProperties>
    [Serializable, EdmEntityType(NamespaceName="TheBeerHouseVBModel", Name="Picture"), DataContract(IsReference=true)]
    public class Picture : EntityObject, IBaseEntity
    {
        private bool _Active = true;
        private string _AddedBy;
        private int _AlbumOrder;
        private DateTime _DateAdded;
        private DateTime _DateUpdated;
        private string _PictureCaption;
        private DateTime _PictureCreateDate;
        private string _PictureFileName;
        private bool? _PictureHighlight;
        private int _PictureID;
        private string _PictureTitle;
        private int _PictureViewCount;
        private string _setName = "Pictures";
        private int _ThumbHeight;
        private int _ThumbWidth;
        private string _UpdatedBy;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new Picture object.
        /// </summary>
        /// <param name="pictureID">Initial value of PictureID.</param>
        /// <param name="pictureCreateDate">Initial value of PictureCreateDate.</param>
        /// <param name="pictureViewCount">Initial value of PictureViewCount.</param>
        /// <param name="albumOrder">Initial value of AlbumOrder.</param>
        /// <param name="thumbWidth">Initial value of ThumbWidth.</param>
        /// <param name="thumbHeight">Initial value of ThumbHeight.</param>
        /// <param name="dateUpdated">Initial value of DateUpdated.</param>
        /// <param name="updatedBy">Initial value of UpdatedBy.</param>
        /// <param name="dateAdded">Initial value of DateAdded.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        public static Picture CreatePicture(int pictureID, DateTime pictureCreateDate, int pictureViewCount, int albumOrder, int thumbWidth, int thumbHeight, DateTime dateUpdated, string updatedBy, DateTime dateAdded, string addedBy)
        {
            Picture picture = new Picture();
            picture.PictureID = pictureID;
            picture.PictureCreateDate = pictureCreateDate;
            picture.PictureViewCount = pictureViewCount;
            picture.AlbumOrder = albumOrder;
            picture.ThumbWidth = thumbWidth;
            picture.ThumbHeight = thumbHeight;
            picture.DateUpdated = dateUpdated;
            picture.UpdatedBy = updatedBy;
            picture.DateAdded = dateAdded;
            picture.AddedBy = addedBy;
            return picture;
        }

        private void OnAddedByChanging(string value)
        {
            if (!string.IsNullOrEmpty(this.AddedBy))
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

        private void OnAlbumOrderChanging(int value)
        {
        }

        private void OnPictureAlbumIDChanging(int value)
        {
        }

        private void OnPictureCaptionChanging(string value)
        {
        }

        private void OnPictureCreateDateChanging(DateTime value)
        {
        }

        private void OnPictureFileNameChanging(string value)
        {
        }

        private void OnPictureHighlightChanging(bool value)
        {
        }

        private void OnPictureTitleChanging(string value)
        {
        }

        private void OnPictureViewCountChanging(int value)
        {
        }

        private void OnThumbHeightChanging(int value)
        {
        }

        private void OnThumbWidthChanging(int value)
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
        /// There are no comments for Album in the schema.
        /// </summary>
        [XmlIgnore, EdmRelationshipNavigationProperty("TheBeerHouseVBModel", "FK_Pictures_Album", "tbh_Album"), SoapIgnore, DataMember]
        public TheBeerHouse.BLL.Gallery.Album Album
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Gallery.Album>("TheBeerHouseVBModel.FK_Pictures_Album", "tbh_Album").Value;
            }
            set
            {
                this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Gallery.Album>("TheBeerHouseVBModel.FK_Pictures_Album", "tbh_Album").Value = value;
            }
        }

        public int AlbumId
        {
            get
            {
                if (!Information.IsNothing(this.AlbumReference.EntityKey))
                {
                    return Conversions.ToInteger(this.AlbumReference.EntityKey.EntityKeyValues[0].Value);
                }
                return 0;
            }
            set
            {
                if (!Information.IsNothing(this.AlbumReference.EntityKey))
                {
                    this.AlbumReference = null;
                }
                this.AlbumReference.EntityKey = new EntityKey("GalleryEntities.Albums", "AlbumID", value);
            }
        }

        public string AlbumName
        {
            get
            {
                if (!Information.IsNothing(this.Album))
                {
                    return this.Album.AlbumName;
                }
                return "Not Set";
            }
        }

        /// <summary>
        /// There are no comments for Property AlbumOrder in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int AlbumOrder
        {
            get
            {
                return this._AlbumOrder;
            }
            set
            {
                this.OnAlbumOrderChanging(value);
                this.ReportPropertyChanging("AlbumOrder");
                this._AlbumOrder = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AlbumOrder");
            }
        }

        /// <summary>
        /// There are no comments for Album in the schema.
        /// </summary>
        [DataMember, Browsable(false)]
        public EntityReference<TheBeerHouse.BLL.Gallery.Album> AlbumReference
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Gallery.Album>("TheBeerHouseVBModel.FK_Pictures_Album", "tbh_Album");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedReference<TheBeerHouse.BLL.Gallery.Album>("TheBeerHouseVBModel.FK_Pictures_Album", "tbh_Album", value);
                }
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
                return (((((!string.IsNullOrEmpty(this.PictureTitle) & !string.IsNullOrEmpty(this.PictureCaption)) & !string.IsNullOrEmpty(this.PictureFileName)) & (this.ThumbHeight > 0)) & (this.ThumbWidth > 0)) & (this.AlbumId > 0));
            }
        }

        /// <summary>
        /// There are no comments for Property PictureCaption in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string PictureCaption
        {
            get
            {
                return this._PictureCaption;
            }
            set
            {
                this.OnPictureCaptionChanging(value);
                this.ReportPropertyChanging("PictureCaption");
                this._PictureCaption = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PictureCaption");
            }
        }

        /// <summary>
        /// There are no comments for Property PictureCreateDate in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public DateTime PictureCreateDate
        {
            get
            {
                return this._PictureCreateDate;
            }
            set
            {
                this.OnPictureCreateDateChanging(value);
                this.ReportPropertyChanging("PictureCreateDate");
                this._PictureCreateDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PictureCreateDate");
            }
        }

        /// <summary>
        /// There are no comments for Property PictureFileName in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string PictureFileName
        {
            get
            {
                return this._PictureFileName;
            }
            set
            {
                this.OnPictureFileNameChanging(value);
                this.ReportPropertyChanging("PictureFileName");
                this._PictureFileName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PictureFileName");
            }
        }

        /// <summary>
        /// There are no comments for Property PictureHighlight in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public bool? PictureHighlight
        {
            get
            {
                return this._PictureHighlight;
            }
            set
            {
                this.ReportPropertyChanging("PictureHighlight");
                this._PictureHighlight = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PictureHighlight");
            }
        }

        /// <summary>
        /// There are no comments for Property PictureID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int PictureID
        {
            get
            {
                return this._PictureID;
            }
            set
            {
                this.ReportPropertyChanging("PictureID");
                this._PictureID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PictureID");
            }
        }

        /// <summary>
        /// There are no comments for Property PictureTitle in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string PictureTitle
        {
            get
            {
                return this._PictureTitle;
            }
            set
            {
                this.OnPictureTitleChanging(value);
                this.ReportPropertyChanging("PictureTitle");
                this._PictureTitle = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PictureTitle");
            }
        }

        /// <summary>
        /// There are no comments for Property PictureViewCount in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int PictureViewCount
        {
            get
            {
                return this._PictureViewCount;
            }
            set
            {
                this.OnPictureViewCountChanging(value);
                this.ReportPropertyChanging("PictureViewCount");
                this._PictureViewCount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PictureViewCount");
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
                return (((((!string.IsNullOrEmpty(this.PictureTitle) & !string.IsNullOrEmpty(this.PictureCaption)) & !string.IsNullOrEmpty(this.PictureFileName)) & (this.ThumbHeight > 0)) & (this.ThumbWidth > 0)) & (this.AlbumId > 0));
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
        /// There are no comments for Property ThumbHeight in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int ThumbHeight
        {
            get
            {
                return this._ThumbHeight;
            }
            set
            {
                this.OnThumbHeightChanging(value);
                this.ReportPropertyChanging("ThumbHeight");
                this._ThumbHeight = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ThumbHeight");
            }
        }

        /// <summary>
        /// There are no comments for Property ThumbWidth in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int ThumbWidth
        {
            get
            {
                return this._ThumbWidth;
            }
            set
            {
                this.OnThumbWidthChanging(value);
                this.ReportPropertyChanging("ThumbWidth");
                this._ThumbWidth = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ThumbWidth");
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

