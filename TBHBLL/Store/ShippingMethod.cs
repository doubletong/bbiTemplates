using System;
using BLL;

namespace BBICMS.Store
{
    public partial class ShippingMethod : IBaseEntity
    {
        private string _setName = "ShippingMethods";
        private bool bIsDirty = false;

        public string TitleAndPrice
        {
            get { return string.Format("{0} ({1:N2} {2})", Title, Price, Globals.Settings.Store.CurrencyCode); }
        }

        #region IBaseEntity Members

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Title) == false & Price > 0)
                {
                    return true;
                }
                return false;
            }
        }

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

        #endregion

        partial void OnAddedByChanging(string value)
        {
            if (string.IsNullOrEmpty(AddedBy) == false)
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }
    }
}