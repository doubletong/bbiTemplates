using System;

namespace BLL
{

    public partial class SiteMapInfo : IBaseEntity
    {

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(URL) == false & string.IsNullOrEmpty(RealURL) == false & string.IsNullOrEmpty(Title) == false)
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

        private string _SetName = "SiteMap";
        public string SetName
        {
            get { return _SetName; }
            set { _SetName = value; }
        }

        partial void OnSiteMapIdChanging(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("The SiteMapId cannot be less than 0.");
            }
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

    }
}