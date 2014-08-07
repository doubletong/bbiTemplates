using System;
using System.Data;
using BLL;

namespace BBICMS.Gallery
{

    public partial class Picture : IBaseEntity
    {

        public string AlbumName
        {
            get
            {
                if ((Album != null))
                {
                    return Album.AlbumName;
                }
                return "Not Set";
            }
        }

        public int AlbumId
        {
            get
            {
                if ((this.AlbumReference.EntityKey != null))
                {
                    return int.Parse( this.AlbumReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if ((this.AlbumReference.EntityKey != null))
                {
                    this.AlbumReference = null;
                }
                this.AlbumReference.EntityKey = new EntityKey("GalleryEntities.Albums", "AlbumID", value);
            }
        }

        private string _setName = "Pictures";
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.PictureTitle) == false & string.IsNullOrEmpty(this.PictureCaption) == false & string.IsNullOrEmpty(this.PictureFileName) == false & this.ThumbHeight > 0 & this.ThumbWidth > 0 & this.AlbumId > 0)
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

