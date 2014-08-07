    using Joel.Net;
    using System.Data;
    using System.Web;
    using BBICMS;
using BLL;
    
    namespace BBICMS.Articles
{


    /// <summary>
    /// There are no comments for ArticlesModel.Comment in the schema.
    /// </summary>
    /// <KeyProperties>
    /// CommentID
    /// </KeyProperties>
    public partial class Comment : IBaseEntity
    {

        #region " Akismet "

        public string DisplayTitle
        {
            get { return string.IsNullOrEmpty(this.Title) ? (this.Article != null) ? this.Article.Title : "Not Supplied" : this.Title; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string EMailHash
        {
            get { return GravatarHelper.GetGravatarHash(this.AddedByEmail); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string EncodedBody
        {
            get { return HttpUtility.HtmlEncode(this.Body); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public AkismetComment GetAkismetComment()
        {

            AkismetComment lComment = new AkismetComment();
            lComment.Blog = "http://BBICMSBook.com";
            lComment.CommentAuthor = this.AddedBy;
            lComment.CommentAuthorEmail = this.AddedByEmail;
            lComment.CommentContent = this.Body;
            lComment.CommentType = "comment";
            lComment.UserIp = this.AddedByIP;
            lComment.CommentAuthorUrl = this.CommenterURL;


            return lComment;
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
                if ((this.ArticleReference.EntityKey != null))
                {
                    return (int)this.ArticleReference.EntityKey.EntityKeyValues[0].Value;
                }
                return 0;
            }
            set
            {
                if ((this.ArticleReference.EntityKey != null))
                {
                    this.ArticleReference = null;
                }
                this.ArticleReference.EntityKey = new EntityKey("ArticlesEntities.Articles", "ArticleID", value);
            }
        }

        #endregion


        private string _setName = "Comments";
        /// <summary>
        /// Returns the name of the Data Set the Entity belongs to. Needs to be set
        /// in the derived class.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        bool bIsDirty = false;
        public bool IsDirty
        {
            get { return bIsDirty; }
            set { bIsDirty = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Body) == false & string.IsNullOrEmpty(this.AddedByEmail) == false &&
                    string.IsNullOrEmpty(this.AddedByIP) == false)
                {
                    return true;
                }
                return false;
            }
        }

        #region " Authorization "

        public bool CanAdd
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanDelete
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanEdit
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanRead
        {
            get { return true; }
        }

        #endregion

    }
}

