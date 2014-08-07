namespace TheBeerHouse.BLL.Forums
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for ForumsModel.Post in the schema.
    /// </summary>
    /// <KeyProperties>
    /// PostID
    /// </KeyProperties>
    [Serializable, EdmEntityType(NamespaceName="ForumsModel", Name="Post"), DataContract(IsReference=true)]
    public class Post : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private string _AddedByIP;
        private DateTime _AddedDate;
        private bool _Approved;
        private string _Body;
        private bool _Closed;
        private string _LastPostBy;
        private DateTime _LastPostDate;
        private Post _parentPost;
        private int _ParentPostID;
        private int _PostID;
        private int _ReplyCount;
        private string _setName = "Posts";
        private bool _Sticky;
        private string _Title;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private int _ViewCount;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new Post object.
        /// </summary>
        /// <param name="postID">Initial value of PostID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="addedByIP">Initial value of AddedByIP.</param>
        /// <param name="parentPostID">Initial value of ParentPostID.</param>
        /// <param name="title">Initial value of Title.</param>
        /// <param name="body">Initial value of Body.</param>
        /// <param name="approved">Initial value of Approved.</param>
        /// <param name="closed">Initial value of Closed.</param>
        /// <param name="viewCount">Initial value of ViewCount.</param>
        /// <param name="replyCount">Initial value of ReplyCount.</param>
        /// <param name="lastPostBy">Initial value of LastPostBy.</param>
        /// <param name="lastPostDate">Initial value of LastPostDate.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        /// <param name="sticky">Initial value of Sticky.</param>
        public static Post CreatePost(int postID, DateTime addedDate, string addedBy, string addedByIP, int parentPostID, string title, string body, bool approved, bool closed, int viewCount, int replyCount, string lastPostBy, DateTime lastPostDate, DateTime updatedDate, bool active, bool sticky)
        {
            Post post = new Post();
            post.PostID = postID;
            post.AddedDate = addedDate;
            post.AddedBy = addedBy;
            post.AddedByIP = addedByIP;
            post.ParentPostID = parentPostID;
            post.Title = title;
            post.Body = body;
            post.Approved = approved;
            post.Closed = closed;
            post.ViewCount = viewCount;
            post.ReplyCount = replyCount;
            post.LastPostBy = lastPostBy;
            post.LastPostDate = lastPostDate;
            post.UpdatedDate = updatedDate;
            post.Active = active;
            post.Sticky = sticky;
            return post;
        }

        private void OnAddedByChanging(string value)
        {
            if (!string.IsNullOrEmpty(this.AddedBy))
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

        private void OnAddedByIPChanging(string value)
        {
        }

        private void OnAddedDateChanging(DateTime value)
        {
        }

        private void OnApprovedChanging(bool value)
        {
        }

        private void OnBodyChanging(string value)
        {
        }

        private void OnClosedChanging(bool value)
        {
        }

        private void OnForumIDChanging(int value)
        {
        }

        private void OnLastPostByChanging(string value)
        {
        }

        private void OnLastPostDateChanging(DateTime value)
        {
        }

        private void OnParentPostIDChanging(int value)
        {
        }

        private void OnReplyCountChanging(int value)
        {
        }

        private void OnTitleChanging(string value)
        {
        }

        private void OnUpdatedDateChanging(DateTime value)
        {
        }

        private void OnViewCountChanging(int value)
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
                this.OnAddedByIPChanging(value);
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
                this.OnAddedDateChanging(value);
                this.ReportPropertyChanging("AddedDate");
                this._AddedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AddedDate");
            }
        }

        /// <summary>
        /// There are no comments for Property Approved in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public bool Approved
        {
            get
            {
                return this._Approved;
            }
            set
            {
                this.OnApprovedChanging(value);
                this.ReportPropertyChanging("Approved");
                this._Approved = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Approved");
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
                this.OnBodyChanging(value);
                this.ReportPropertyChanging("Body");
                this._Body = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Body");
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
        /// There are no comments for Property Closed in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public bool Closed
        {
            get
            {
                return this._Closed;
            }
            set
            {
                this.OnClosedChanging(value);
                this.ReportPropertyChanging("Closed");
                this._Closed = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Closed");
            }
        }

        /// <summary>
        /// There are no comments for Forum in the schema.
        /// </summary>
        [DataMember, XmlIgnore, SoapIgnore, EdmRelationshipNavigationProperty("ForumsModel", "FK_tbh_Posts_tbh_Forums", "tbh_Forums")]
        public TheBeerHouse.BLL.Forums.Forum Forum
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Forums.Forum>("ForumsModel.FK_tbh_Posts_tbh_Forums", "tbh_Forums").Value;
            }
            set
            {
                this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Forums.Forum>("ForumsModel.FK_tbh_Posts_tbh_Forums", "tbh_Forums").Value = value;
            }
        }

        public int ForumId
        {
            get
            {
                if (!Information.IsNothing(this.ForumReference.EntityKey))
                {
                    return Conversions.ToInteger(this.ForumReference.EntityKey.EntityKeyValues[0].Value);
                }
                return 0;
            }
            set
            {
                if (!Information.IsNothing(this.ForumReference.EntityKey))
                {
                    this.ForumReference = null;
                }
                this.ForumReference.EntityKey = new EntityKey("ForumModel.Forums", "ForumID", value);
            }
        }

        /// <summary>
        /// There are no comments for Forum in the schema.
        /// </summary>
        [Browsable(false), DataMember]
        public EntityReference<TheBeerHouse.BLL.Forums.Forum> ForumReference
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Forums.Forum>("ForumsModel.FK_tbh_Posts_tbh_Forums", "tbh_Forums");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedReference<TheBeerHouse.BLL.Forums.Forum>("ForumsModel.FK_tbh_Posts_tbh_Forums", "tbh_Forums", value);
                }
            }
        }

        public string ForumTitle
        {
            get
            {
                if (!Information.IsNothing(this.Forum))
                {
                    return this.Forum.Title;
                }
                return "Forum Not Retrieved";
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

        public bool IsFirstPost
        {
            get
            {
                return (this.ParentPostID == 0);
            }
        }

        public bool IsThreadPost
        {
            get
            {
                bool IsThreadPost;
                Interaction.IIf(this.ParentPostID == 0, true, false);
                return IsThreadPost;
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
                return (!string.IsNullOrEmpty(this.Title) & !string.IsNullOrEmpty(this.Body));
            }
        }

        /// <summary>
        /// There are no comments for Property LastPostBy in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string LastPostBy
        {
            get
            {
                return this._LastPostBy;
            }
            set
            {
                this.OnLastPostByChanging(value);
                this.ReportPropertyChanging("LastPostBy");
                this._LastPostBy = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("LastPostBy");
            }
        }

        /// <summary>
        /// There are no comments for Property LastPostDate in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public DateTime LastPostDate
        {
            get
            {
                return this._LastPostDate;
            }
            set
            {
                this.OnLastPostDateChanging(value);
                this.ReportPropertyChanging("LastPostDate");
                this._LastPostDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("LastPostDate");
            }
        }

        public Post ParentPost
        {
            get
            {
                if (Information.IsNothing(this._parentPost))
                {
                    using (PostsRepository lPostrpt = new PostsRepository())
                    {
                        this._parentPost = lPostrpt.GetPostById(this.ParentPostID);
                    }
                }
                return this._parentPost;
            }
            private set
            {
                this._parentPost = value;
            }
        }

        /// <summary>
        /// There are no comments for Property ParentPostID in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int ParentPostID
        {
            get
            {
                return this._ParentPostID;
            }
            set
            {
                this.OnParentPostIDChanging(value);
                this.ReportPropertyChanging("ParentPostID");
                this._ParentPostID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ParentPostID");
            }
        }

        /// <summary>
        /// There are no comments for Property PostID in the schema.
        /// </summary>
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int PostID
        {
            get
            {
                return this._PostID;
            }
            set
            {
                this.ReportPropertyChanging("PostID");
                this._PostID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PostID");
            }
        }

        /// <summary>
        /// There are no comments for Property ReplyCount in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int ReplyCount
        {
            get
            {
                return this._ReplyCount;
            }
            set
            {
                this.OnReplyCountChanging(value);
                this.ReportPropertyChanging("ReplyCount");
                this._ReplyCount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ReplyCount");
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
        /// There are no comments for Property Sticky in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public bool Sticky
        {
            get
            {
                return this._Sticky;
            }
            set
            {
                this.ReportPropertyChanging("Sticky");
                this._Sticky = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Sticky");
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
                return (!string.IsNullOrEmpty(this.Title) & !string.IsNullOrEmpty(this.Body));
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
                this.OnTitleChanging(value);
                this.ReportPropertyChanging("Title");
                this._Title = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Title");
            }
        }

        public string TitleBody
        {
            get
            {
                IEnumerator VB$t_ref$L0;
                string lBody = Helpers.RemoveHTML(this.Body);
                Regex lwordRegEx = new Regex(@"\b\w*\b");
                StringBuilder lwordArray = new StringBuilder();
                int i = 0;
                try
                {
                    VB$t_ref$L0 = lwordRegEx.Matches(lBody).GetEnumerator();
                    while (VB$t_ref$L0.MoveNext())
                    {
                        Match lword = (Match) VB$t_ref$L0.Current;
                        lwordArray.Append(lword.Value + " ");
                        i++;
                        if (i == 50)
                        {
                            goto Label_009F;
                        }
                    }
                }
                finally
                {
                    if (VB$t_ref$L0 is IDisposable)
                    {
                        (VB$t_ref$L0 as IDisposable).Dispose();
                    }
                }
            Label_009F:
                return string.Format("{0}...", lwordArray.ToString());
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
        /// There are no comments for Property ViewCount in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int ViewCount
        {
            get
            {
                return this._ViewCount;
            }
            set
            {
                this.OnViewCountChanging(value);
                this.ReportPropertyChanging("ViewCount");
                this._ViewCount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ViewCount");
            }
        }
    }
}

