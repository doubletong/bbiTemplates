﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM 关系源元数据

[assembly: EdmRelationshipAttribute("BBICMS.BLL.Gallery", "FK_Pictures_Album", "tbh_Album", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(BBICMS.Gallery.Album), "tbh_Pictures", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(BBICMS.Gallery.Picture))]

#endregion

namespace BBICMS.Gallery
{
    #region 上下文
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    public partial class GalleryEntities : ObjectContext
    {
        #region 构造函数
    
        /// <summary>
        /// 请使用应用程序配置文件的“GalleryEntities”部分中的连接字符串初始化新 GalleryEntities 对象。
        /// </summary>
        public GalleryEntities() : base("name=GalleryEntities", "GalleryEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 GalleryEntities 对象。
        /// </summary>
        public GalleryEntities(string connectionString) : base(connectionString, "GalleryEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 GalleryEntities 对象。
        /// </summary>
        public GalleryEntities(EntityConnection connection) : base(connection, "GalleryEntities")
        {
            OnContextCreated();
        }
    
        #endregion
    
        #region 分部方法
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet 属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<Album> Albums
        {
            get
            {
                if ((_Albums == null))
                {
                    _Albums = base.CreateObjectSet<Album>("Albums");
                }
                return _Albums;
            }
        }
        private ObjectSet<Album> _Albums;
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<Picture> Pictures
        {
            get
            {
                if ((_Pictures == null))
                {
                    _Pictures = base.CreateObjectSet<Picture>("Pictures");
                }
                return _Pictures;
            }
        }
        private ObjectSet<Picture> _Pictures;

        #endregion

        #region AddTo 方法
    
        /// <summary>
        /// 用于向 Albums EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddToAlbums(Album album)
        {
            base.AddObject("Albums", album);
        }
    
        /// <summary>
        /// 用于向 Pictures EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddToPictures(Picture picture)
        {
            base.AddObject("Pictures", picture);
        }

        #endregion

    }

    #endregion

    #region 实体
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="BBICMS.BLL.Gallery", Name="Album")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Album : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 Album 对象。
        /// </summary>
        /// <param name="albumID">AlbumID 属性的初始值。</param>
        /// <param name="albumCreateDate">AlbumCreateDate 属性的初始值。</param>
        /// <param name="parentAlbumID">ParentAlbumID 属性的初始值。</param>
        /// <param name="active">Active 属性的初始值。</param>
        /// <param name="dateAdded">DateAdded 属性的初始值。</param>
        /// <param name="dateUpdated">DateUpdated 属性的初始值。</param>
        /// <param name="addedBy">AddedBy 属性的初始值。</param>
        /// <param name="updatedBy">UpdatedBy 属性的初始值。</param>
        public static Album CreateAlbum(global::System.Int32 albumID, global::System.DateTime albumCreateDate, global::System.Int32 parentAlbumID, global::System.Boolean active, global::System.DateTime dateAdded, global::System.DateTime dateUpdated, global::System.String addedBy, global::System.String updatedBy)
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

        #endregion

        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 AlbumID
        {
            get
            {
                return _AlbumID;
            }
            set
            {
                if (_AlbumID != value)
                {
                    OnAlbumIDChanging(value);
                    ReportPropertyChanging("AlbumID");
                    _AlbumID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("AlbumID");
                    OnAlbumIDChanged();
                }
            }
        }
        private global::System.Int32 _AlbumID;
        partial void OnAlbumIDChanging(global::System.Int32 value);
        partial void OnAlbumIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String AlbumName
        {
            get
            {
                return _AlbumName;
            }
            set
            {
                OnAlbumNameChanging(value);
                ReportPropertyChanging("AlbumName");
                _AlbumName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("AlbumName");
                OnAlbumNameChanged();
            }
        }
        private global::System.String _AlbumName;
        partial void OnAlbumNameChanging(global::System.String value);
        partial void OnAlbumNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String AlbumDesc
        {
            get
            {
                return _AlbumDesc;
            }
            set
            {
                OnAlbumDescChanging(value);
                ReportPropertyChanging("AlbumDesc");
                _AlbumDesc = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("AlbumDesc");
                OnAlbumDescChanged();
            }
        }
        private global::System.String _AlbumDesc;
        partial void OnAlbumDescChanging(global::System.String value);
        partial void OnAlbumDescChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime AlbumCreateDate
        {
            get
            {
                return _AlbumCreateDate;
            }
            set
            {
                OnAlbumCreateDateChanging(value);
                ReportPropertyChanging("AlbumCreateDate");
                _AlbumCreateDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("AlbumCreateDate");
                OnAlbumCreateDateChanged();
            }
        }
        private global::System.DateTime _AlbumCreateDate;
        partial void OnAlbumCreateDateChanging(global::System.DateTime value);
        partial void OnAlbumCreateDateChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ParentAlbumID
        {
            get
            {
                return _ParentAlbumID;
            }
            set
            {
                OnParentAlbumIDChanging(value);
                ReportPropertyChanging("ParentAlbumID");
                _ParentAlbumID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ParentAlbumID");
                OnParentAlbumIDChanged();
            }
        }
        private global::System.Int32 _ParentAlbumID;
        partial void OnParentAlbumIDChanging(global::System.Int32 value);
        partial void OnParentAlbumIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean Active
        {
            get
            {
                return _Active;
            }
            set
            {
                OnActiveChanging(value);
                ReportPropertyChanging("Active");
                _Active = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Active");
                OnActiveChanged();
            }
        }
        private global::System.Boolean _Active;
        partial void OnActiveChanging(global::System.Boolean value);
        partial void OnActiveChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateAdded
        {
            get
            {
                return _DateAdded;
            }
            set
            {
                OnDateAddedChanging(value);
                ReportPropertyChanging("DateAdded");
                _DateAdded = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DateAdded");
                OnDateAddedChanged();
            }
        }
        private global::System.DateTime _DateAdded;
        partial void OnDateAddedChanging(global::System.DateTime value);
        partial void OnDateAddedChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateUpdated
        {
            get
            {
                return _DateUpdated;
            }
            set
            {
                OnDateUpdatedChanging(value);
                ReportPropertyChanging("DateUpdated");
                _DateUpdated = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DateUpdated");
                OnDateUpdatedChanged();
            }
        }
        private global::System.DateTime _DateUpdated;
        partial void OnDateUpdatedChanging(global::System.DateTime value);
        partial void OnDateUpdatedChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String AddedBy
        {
            get
            {
                return _AddedBy;
            }
            set
            {
                OnAddedByChanging(value);
                ReportPropertyChanging("AddedBy");
                _AddedBy = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("AddedBy");
                OnAddedByChanged();
            }
        }
        private global::System.String _AddedBy;
        partial void OnAddedByChanging(global::System.String value);
        partial void OnAddedByChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }
            set
            {
                OnUpdatedByChanging(value);
                ReportPropertyChanging("UpdatedBy");
                _UpdatedBy = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("UpdatedBy");
                OnUpdatedByChanged();
            }
        }
        private global::System.String _UpdatedBy;
        partial void OnUpdatedByChanging(global::System.String value);
        partial void OnUpdatedByChanged();

        #endregion

    
        #region 导航属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("BBICMS.BLL.Gallery", "FK_Pictures_Album", "tbh_Pictures")]
        public EntityCollection<Picture> Pictures
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Picture>("BBICMS.BLL.Gallery.FK_Pictures_Album", "tbh_Pictures");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Picture>("BBICMS.BLL.Gallery.FK_Pictures_Album", "tbh_Pictures", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="BBICMS.BLL.Gallery", Name="Picture")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Picture : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 Picture 对象。
        /// </summary>
        /// <param name="pictureID">PictureID 属性的初始值。</param>
        /// <param name="pictureCreateDate">PictureCreateDate 属性的初始值。</param>
        /// <param name="pictureViewCount">PictureViewCount 属性的初始值。</param>
        /// <param name="albumOrder">AlbumOrder 属性的初始值。</param>
        /// <param name="thumbWidth">ThumbWidth 属性的初始值。</param>
        /// <param name="thumbHeight">ThumbHeight 属性的初始值。</param>
        /// <param name="active">Active 属性的初始值。</param>
        /// <param name="dateUpdated">DateUpdated 属性的初始值。</param>
        /// <param name="updatedBy">UpdatedBy 属性的初始值。</param>
        /// <param name="dateAdded">DateAdded 属性的初始值。</param>
        /// <param name="addedBy">AddedBy 属性的初始值。</param>
        public static Picture CreatePicture(global::System.Int32 pictureID, global::System.DateTime pictureCreateDate, global::System.Int32 pictureViewCount, global::System.Int32 albumOrder, global::System.Int32 thumbWidth, global::System.Int32 thumbHeight, global::System.Boolean active, global::System.DateTime dateUpdated, global::System.String updatedBy, global::System.DateTime dateAdded, global::System.String addedBy)
        {
            Picture picture = new Picture();
            picture.PictureID = pictureID;
            picture.PictureCreateDate = pictureCreateDate;
            picture.PictureViewCount = pictureViewCount;
            picture.AlbumOrder = albumOrder;
            picture.ThumbWidth = thumbWidth;
            picture.ThumbHeight = thumbHeight;
            picture.Active = active;
            picture.DateUpdated = dateUpdated;
            picture.UpdatedBy = updatedBy;
            picture.DateAdded = dateAdded;
            picture.AddedBy = addedBy;
            return picture;
        }

        #endregion

        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 PictureID
        {
            get
            {
                return _PictureID;
            }
            set
            {
                if (_PictureID != value)
                {
                    OnPictureIDChanging(value);
                    ReportPropertyChanging("PictureID");
                    _PictureID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("PictureID");
                    OnPictureIDChanged();
                }
            }
        }
        private global::System.Int32 _PictureID;
        partial void OnPictureIDChanging(global::System.Int32 value);
        partial void OnPictureIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PictureTitle
        {
            get
            {
                return _PictureTitle;
            }
            set
            {
                OnPictureTitleChanging(value);
                ReportPropertyChanging("PictureTitle");
                _PictureTitle = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("PictureTitle");
                OnPictureTitleChanged();
            }
        }
        private global::System.String _PictureTitle;
        partial void OnPictureTitleChanging(global::System.String value);
        partial void OnPictureTitleChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PictureCaption
        {
            get
            {
                return _PictureCaption;
            }
            set
            {
                OnPictureCaptionChanging(value);
                ReportPropertyChanging("PictureCaption");
                _PictureCaption = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("PictureCaption");
                OnPictureCaptionChanged();
            }
        }
        private global::System.String _PictureCaption;
        partial void OnPictureCaptionChanging(global::System.String value);
        partial void OnPictureCaptionChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime PictureCreateDate
        {
            get
            {
                return _PictureCreateDate;
            }
            set
            {
                OnPictureCreateDateChanging(value);
                ReportPropertyChanging("PictureCreateDate");
                _PictureCreateDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PictureCreateDate");
                OnPictureCreateDateChanged();
            }
        }
        private global::System.DateTime _PictureCreateDate;
        partial void OnPictureCreateDateChanging(global::System.DateTime value);
        partial void OnPictureCreateDateChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PictureFileName
        {
            get
            {
                return _PictureFileName;
            }
            set
            {
                OnPictureFileNameChanging(value);
                ReportPropertyChanging("PictureFileName");
                _PictureFileName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("PictureFileName");
                OnPictureFileNameChanged();
            }
        }
        private global::System.String _PictureFileName;
        partial void OnPictureFileNameChanging(global::System.String value);
        partial void OnPictureFileNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> PictureHighlight
        {
            get
            {
                return _PictureHighlight;
            }
            set
            {
                OnPictureHighlightChanging(value);
                ReportPropertyChanging("PictureHighlight");
                _PictureHighlight = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PictureHighlight");
                OnPictureHighlightChanged();
            }
        }
        private Nullable<global::System.Boolean> _PictureHighlight;
        partial void OnPictureHighlightChanging(Nullable<global::System.Boolean> value);
        partial void OnPictureHighlightChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 PictureViewCount
        {
            get
            {
                return _PictureViewCount;
            }
            set
            {
                OnPictureViewCountChanging(value);
                ReportPropertyChanging("PictureViewCount");
                _PictureViewCount = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PictureViewCount");
                OnPictureViewCountChanged();
            }
        }
        private global::System.Int32 _PictureViewCount;
        partial void OnPictureViewCountChanging(global::System.Int32 value);
        partial void OnPictureViewCountChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 AlbumOrder
        {
            get
            {
                return _AlbumOrder;
            }
            set
            {
                OnAlbumOrderChanging(value);
                ReportPropertyChanging("AlbumOrder");
                _AlbumOrder = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("AlbumOrder");
                OnAlbumOrderChanged();
            }
        }
        private global::System.Int32 _AlbumOrder;
        partial void OnAlbumOrderChanging(global::System.Int32 value);
        partial void OnAlbumOrderChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ThumbWidth
        {
            get
            {
                return _ThumbWidth;
            }
            set
            {
                OnThumbWidthChanging(value);
                ReportPropertyChanging("ThumbWidth");
                _ThumbWidth = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ThumbWidth");
                OnThumbWidthChanged();
            }
        }
        private global::System.Int32 _ThumbWidth;
        partial void OnThumbWidthChanging(global::System.Int32 value);
        partial void OnThumbWidthChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ThumbHeight
        {
            get
            {
                return _ThumbHeight;
            }
            set
            {
                OnThumbHeightChanging(value);
                ReportPropertyChanging("ThumbHeight");
                _ThumbHeight = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ThumbHeight");
                OnThumbHeightChanged();
            }
        }
        private global::System.Int32 _ThumbHeight;
        partial void OnThumbHeightChanging(global::System.Int32 value);
        partial void OnThumbHeightChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean Active
        {
            get
            {
                return _Active;
            }
            set
            {
                OnActiveChanging(value);
                ReportPropertyChanging("Active");
                _Active = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Active");
                OnActiveChanged();
            }
        }
        private global::System.Boolean _Active;
        partial void OnActiveChanging(global::System.Boolean value);
        partial void OnActiveChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateUpdated
        {
            get
            {
                return _DateUpdated;
            }
            set
            {
                OnDateUpdatedChanging(value);
                ReportPropertyChanging("DateUpdated");
                _DateUpdated = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DateUpdated");
                OnDateUpdatedChanged();
            }
        }
        private global::System.DateTime _DateUpdated;
        partial void OnDateUpdatedChanging(global::System.DateTime value);
        partial void OnDateUpdatedChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }
            set
            {
                OnUpdatedByChanging(value);
                ReportPropertyChanging("UpdatedBy");
                _UpdatedBy = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("UpdatedBy");
                OnUpdatedByChanged();
            }
        }
        private global::System.String _UpdatedBy;
        partial void OnUpdatedByChanging(global::System.String value);
        partial void OnUpdatedByChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateAdded
        {
            get
            {
                return _DateAdded;
            }
            set
            {
                OnDateAddedChanging(value);
                ReportPropertyChanging("DateAdded");
                _DateAdded = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DateAdded");
                OnDateAddedChanged();
            }
        }
        private global::System.DateTime _DateAdded;
        partial void OnDateAddedChanging(global::System.DateTime value);
        partial void OnDateAddedChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String AddedBy
        {
            get
            {
                return _AddedBy;
            }
            set
            {
                OnAddedByChanging(value);
                ReportPropertyChanging("AddedBy");
                _AddedBy = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("AddedBy");
                OnAddedByChanged();
            }
        }
        private global::System.String _AddedBy;
        partial void OnAddedByChanging(global::System.String value);
        partial void OnAddedByChanged();

        #endregion

    
        #region 导航属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("BBICMS.BLL.Gallery", "FK_Pictures_Album", "tbh_Album")]
        public Album Album
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Album>("BBICMS.BLL.Gallery.FK_Pictures_Album", "tbh_Album").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Album>("BBICMS.BLL.Gallery.FK_Pictures_Album", "tbh_Album").Value = value;
            }
        }
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Album> AlbumReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Album>("BBICMS.BLL.Gallery.FK_Pictures_Album", "tbh_Album");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Album>("BBICMS.BLL.Gallery.FK_Pictures_Album", "tbh_Album", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
