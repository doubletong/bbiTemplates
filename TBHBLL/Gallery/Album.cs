using System;
using BLL;

namespace BBICMS.Gallery
{

    public partial class Album : IBaseEntity
    {

        private Picture _SamplePicture;
        public Picture SamplePicture
        {
            get { return _SamplePicture; }
            set { _SamplePicture = value; }
        }

        private string _setName = "Albums";
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.AlbumName) == false & string.IsNullOrEmpty(this.AlbumDesc) == false)
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

