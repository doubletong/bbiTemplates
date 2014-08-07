using System;
using System.Data;
using BLL;

namespace BBICMS.Store
{
    public partial class Product : IBaseEntity
    {
        #region " On Changing Methods "

        //public int DepartmentId
        //{
        //    get
        //    {
        //        if ((DepartmentReference.EntityKey != null))
        //        {
        //            return int.Parse(DepartmentReference.EntityKey.EntityKeyValues[0].Value.ToString());
        //        }
        //        return 0;
        //    }
        //    set
        //    {
        //        if ((DepartmentReference.EntityKey != null))
        //        {
        //            DepartmentReference = null;
        //        }
        //        DepartmentReference.EntityKey = new EntityKey("ShoppingEntities.Department", "DepartmentID", value);
        //    }
        //}

        #endregion

        private string _setName = "Products";
        private bool bIsDirty = false;

        public decimal AverageRating
        {
            get
            {
                if (Votes > 0)
                {
                    return TotalRating/Votes;
                }

                return 0m;
            }
        }


        public string DepartmentName
        {
            get
            {
                if ((Department != null))
                {
                    return Department.Title;
                }
                return string.Empty;
            }
        }


        public decimal FinalUnitPrice
        {
            get
            {
                if (DiscountPercentage > 0)
                {
                    return UnitPrice - (UnitPrice*DiscountPercentage/100);
                }
                else
                {
                    return UnitPrice;
                }
            }
        }

        #region IBaseEntity Members

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
                if (string.IsNullOrEmpty(Title) == false & string.IsNullOrEmpty(SKU) == false &
                    string.IsNullOrEmpty(Description) == false)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
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