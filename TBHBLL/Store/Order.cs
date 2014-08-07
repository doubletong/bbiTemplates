using System;
using BLL;

namespace BBICMS.Store
{

    public partial class Order : IBaseEntity
    {

        public string StatusTitle
        {
            get
            {
                switch (this.StatusID)
                {
                    case 1:
                        return "Waiting for Payment";
                    case 2:
                        return "Confirmed";
                    case 3:
                        return "Processing";
                    case 4:
                        return "Shipped";
                    case 5:
                        return "Cancelled";
                    default:

                        //Did not match any of the defaults, so now go and retrieve from the database

                        using (OrderStatusesRepository lOrderStatusrpt = new OrderStatusesRepository())
                        {

                            OrderStatus lOrderStatus = lOrderStatusrpt.GetOrderStatusById(StatusID);

                            if ((lOrderStatus != null))
                            {
                                return lOrderStatus.Title;
                            }
                            return "Unknown Status";
                        }

                }
            }
        }

        public double GrandTotal
        {
            get { return (double)(this.SubTotal + this.Shipping); }
        }


        private string _setName = "Orders";
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.CustomerEmail) == false & string.IsNullOrEmpty(this.ShippingCity) == false & string.IsNullOrEmpty(this.ShippingFirstName) == false & string.IsNullOrEmpty(this.ShippingLastName) == false & string.IsNullOrEmpty(this.ShippingState) == false & string.IsNullOrEmpty(this.ShippingStreet) == false & 0 < this.OrderItems.Count & 0 < this.GrandTotal)
                {
                    return true;
                }
                return false;
            }
        }

        bool bIsDirty = false;
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

        partial void OnAddedByChanging(string value)
        {
            if (string.IsNullOrEmpty(this.AddedBy) == false)
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

    }
}