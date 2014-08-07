using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using BBICMS;
using BLL;
namespace BBICMS.Forums
{

    public partial class Post : IBaseEntity
    {


        public bool IsThreadPost
        {
            get { return ParentPostID == 0 ? true : false; }
        }

        public string TitleBody
        {
            get
            {
                string lBody = Body;

                lBody = Helpers.RemoveHTML(lBody);

                Regex lwordRegEx = new Regex("\\b\\w*\\b");
                StringBuilder lwordArray = new StringBuilder();

                int i = 0;
                foreach (Match lword in lwordRegEx.Matches(lBody))
                {
                    lwordArray.Append(lword.Value + " ");
                    i += 1;
                    if (i == 50)
                    {
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

                return string.Format("{0}...", lwordArray.ToString());
            }
        }

        public bool IsFirstPost
        {
            get { return (this.ParentPostID == 0); }
        }

        public string ForumTitle
        {
            get
            {
                if ((this.Forum != null))
                {
                    return this.Forum.Title;
                }
                return "Forum Not Retrieved";
            }
        }

        private Post _parentPost;
        public Post ParentPost
        {
            get
            {
                if ((_parentPost == null))
                {
                    using (PostsRepository lPostrpt = new PostsRepository())
                    {
                        _parentPost = lPostrpt.GetPostById(this.ParentPostID);
                    }
                }
                return _parentPost;
            }
            private set { _parentPost = value; }
        }

        public int ForumId
        {
            get
            {
                if ((this.ForumReference.EntityKey != null))
                {
                    return int.Parse( this.ForumReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if ((this.ForumReference.EntityKey != null))
                {
                    this.ForumReference = null;
                }
                this.ForumReference.EntityKey = new EntityKey("ForumModel.Forums", "ForumID", value);
            }
        }

        private string _setName = "Posts";
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Title) == false & string.IsNullOrEmpty(this.Body) == false)
                {
                    return true;
                }
                return false;
            }
        }

        bool bIsDirty = false;
        public bool IsDirty
        {
            get { return bIsDirty; }
            set { bIsDirty = value; }
        }


        public bool CanAdd
        {
            get { return true; }
        }

        public bool CanDelete
        {
            get { return true; }
        }

        public bool CanEdit
        {
            get { return true; }
        }

        public bool CanRead
        {
            get { return true; }
        }

        partial void OnAddedByChanging(string value)
        {
            if (string.IsNullOrEmpty(this.AddedBy) == false)
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

    }
}