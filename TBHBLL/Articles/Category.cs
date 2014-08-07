using BBICMS;
using BLL;

namespace BBICMS.Articles
{
    /// <summary>
    /// There are no comments for ArticlesModel.Category in the schema.
    /// </summary>
    /// <KeyProperties>
    /// CategoryID
    /// </KeyProperties>

    public partial class Category : IBaseEntity
    {

        private string _setName = "Categories";
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

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Title) && Importance > -1)
                {
                    return false;
                }
                return true;
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

