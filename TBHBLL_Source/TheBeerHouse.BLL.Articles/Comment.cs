namespace TheBeerHouse.BLL.Articles
{
    using Joel.Net;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Web;
    using System.Xml.Serialization;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for ArticlesModel.Comment in the schema.
    /// </summary>
    /// <KeyProperties>
    /// CommentID
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ArticlesModel", Name="Comment")]
    public class Comment : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private string _AddedByEmail;
        private string _AddedByIP;
        private DateTime _AddedDate;
        private bool _Approved;
        private string _Body;
        private string _CommenterURL;
        private int _CommentID;
        private string _setName = "Comments";
        private string _Title;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new Comment object.
        /// </summary>
        /// <param name="commentID">Initial value of CommentID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="addedByEmail">Initial value of AddedByEmail.</param>
        /// <param name="addedByIP">Initial value of AddedByIP.</param>
        /// <param name="body">Initial value of Body.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        /// <param name="approved">Initial value of Approved.</param>
        public static Comment CreateComment(int commentID, DateTime addedDate, string addedBy, string addedByEmail, string addedByIP, string body, DateTime updatedDate, bool active, bool approved)
        {
            Comment comment = new Comment();
            comment.CommentID = commentID;
            comment.AddedDate = addedDate;
            comment.AddedBy = addedBy;
            comment.AddedByEmail = addedByEmail;
            comment.AddedByIP = addedByIP;
            comment.Body = body;
            comment.UpdatedDate = updatedDate;
            comment.Active = active;
            comment.Approved = approved;
            return comment;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public AkismetComment GetAkismetComment()
        {
            AkismetComment lComment = new AkismetComment();
            lComment.Blog = "http://TheBeerHouseBook.com";
            lComment.CommentAuthor = this.AddedBy;
            lComment.CommentAuthorEmail = this.AddedByEmail;
            lComment.CommentContent = this.Body;
            lComment.CommentType = "comment";
            lComment.UserIp = this.AddedByIP;
            lComment.CommentAuthorUrl = this.CommenterURL;
            return lComment;
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
        /// There are no comments for Property AddedByEmail in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string AddedByEmail
        {
            get
            {
                return this._AddedByEmail;
            }
            set
            {
                this.ReportPropertyChanging("AddedByEmail");
                this._AddedByEmail = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AddedByEmail");
            }
        }

        /// <summary>
        /// There are no comments for Property AddedByIP in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string AddedByIP
        {
            get
            {
                return this._AddedByIP;
            }
            set
            {
                this.ReportPropertyChanging("AddedByIP");
                this._AddedByIP = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AddedByIP");
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
        /// There are no comments for Property Approved in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public bool Approved
        {
            get
            {
                return this._Approved;
            }
            set
            {
                this.ReportPropertyChanging("Approved");
                this._Approved = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Approved");
            }
        }

        /// <summary>
        /// There are no comments for Article in the schema.
        /// </summary>
        [DataMember, EdmRelationshipNavigationProperty("ArticlesModel", "FK_tbh_Comments_tbh_Articles", "tbh_Articles"), SoapIgnore, XmlIgnore]
        public TheBeerHouse.BLL.Articles.Article Article
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Articles.Article>("ArticlesModel.FK_tbh_Comments_tbh_Articles", "tbh_Articles").Value;
            }
            set
            {
                this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Articles.Article>("ArticlesModel.FK_tbh_Comments_tbh_Articles", "tbh_Articles").Value = value;
            }
        }

        /// <summary>
        /// http://architectmuse.blogspot.com/2008/09/having-navigation-properties-and.html
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int ArticleId
        {
            get
            {
                if (!Information.IsNothing(this.ArticleReference.EntityKey))
                {
                    return Conversions.ToInteger(this.ArticleReference.EntityKey.EntityKeyValues[0].Value);
                }
                return 0;
            }
            set
            {
                if (!Information.IsNothing(this.ArticleReference.EntityKey))
                {
                    this.ArticleReference = null;
                }
                this.ArticleReference.EntityKey = new EntityKey("ArticlesEntities.Articles", "ArticleID", value);
            }
        }

        /// <summary>
        /// There are no comments for Article in the schema.
        /// </summary>
        [Browsable(false), DataMember]
        public EntityReference<TheBeerHouse.BLL.Articles.Article> ArticleReference
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Articles.Article>("ArticlesModel.FK_tbh_Comments_tbh_Articles", "tbh_Articles");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedReference<TheBeerHouse.BLL.Articles.Article>("ArticlesModel.FK_tbh_Comments_tbh_Articles", "tbh_Articles", value);
                }
            }
        }

        /// <summary>
        /// There are no comments for Property Body in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string Body
        {
            get
            {
                return this._Body;
            }
            set
            {
                this.ReportPropertyChanging("Body");
                this._Body = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Body");
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
        /// There are no comments for Property CommenterURL in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string CommenterURL
        {
            get
            {
                return this._CommenterURL;
            }
            set
            {
                this.ReportPropertyChanging("CommenterURL");
                this._CommenterURL = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CommenterURL");
            }
        }

        /// <summary>
        /// There are no comments for Property CommentID in the schema.
        /// </summary>
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int CommentID
        {
            get
            {
                return this._CommentID;
            }
            set
            {
                this.ReportPropertyChanging("CommentID");
                this._CommentID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CommentID");
            }
        }

        public string DisplayTitle
        {
            get
            {
                return (string.IsNullOrEmpty(this.Title) ? (!Information.IsNothing(this.Article) ? this.Article.Title : "Not Supplied") : this.Title);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string EMailHash
        {
            get
            {
                return GravatarHelper.GetGravatarHash(this.AddedByEmail);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string EncodedBody
        {
            get
            {
                return HttpUtility.HtmlEncode(this.Body);
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
                return ((!string.IsNullOrEmpty(this.Body) & !string.IsNullOrEmpty(this.AddedByEmail)) & !string.IsNullOrEmpty(this.AddedByIP));
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
                return ((!string.IsNullOrEmpty(this.Body) & !string.IsNullOrEmpty(this.AddedByEmail)) & !string.IsNullOrEmpty(this.AddedByIP));
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
        [EdmScalarProperty, DataMember]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this.ReportPropertyChanging("Title");
                this._Title = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Title");
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
    }
}

