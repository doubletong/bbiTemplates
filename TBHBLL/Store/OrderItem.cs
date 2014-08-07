using System;
using System.Data;
using BLL;

namespace BBICMS.Store
{

    public partial class OrderItem : IBaseEntity
    {

        private string _setName = "OrderItems";
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Title) == false & string.IsNullOrEmpty(this.SKU) == false & this.Quantity > 0 & this.ProductID > 0 & this.UnitPrice > 0)
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

        public int OrderId
        {
            get
            {
                if ((this.OrderReference.EntityKey != null))
                {
                    return int.Parse(this.OrderReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if ((this.OrderReference.EntityKey != null))
                {
                    this.OrderReference = null;
                }
                this.OrderReference.EntityKey = new EntityKey("ShoppingEntities.Orders", "OrderID", value);
            }
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