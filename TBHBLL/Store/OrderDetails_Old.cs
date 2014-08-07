namespace BBICMS.BLL.Store
{
    using System;

    public class OrderDetails_Old
    {
        private string _addedBy;
        private DateTime _addedDate;
        private string _customerEmail;
        private string _customerFax;
        private string _customerPhone;
        private int _id;
        private DateTime _shippedDate;
        private decimal _shipping;
        private string _shippingCity;
        private string _shippingCountry;
        private string _shippingFirstName;
        private string _shippingLastName;
        private string _shippingMethod;
        private string _shippingPostalCode;
        private string _shippingState;
        private string _shippingStreet;
        private int _statusID;
        private string _statusTitle;
        private decimal _subTotal;
        private string _trackingID;
        private string _transactionID;

        public OrderDetails_Old()
        {
            this._id = 0;
            this._addedDate = DateTime.Now;
            this._addedBy = string.Empty;
            this._statusID = 0;
            this._statusTitle = string.Empty;
            this._shippingMethod = string.Empty;
            this._subTotal = new decimal();
            this._shipping = new decimal();
            this._shippingFirstName = string.Empty;
            this._shippingLastName = string.Empty;
            this._shippingStreet = string.Empty;
            this._shippingPostalCode = string.Empty;
            this._shippingCity = string.Empty;
            this._shippingState = string.Empty;
            this._shippingCountry = string.Empty;
            this._customerEmail = string.Empty;
            this._customerPhone = string.Empty;
            this._customerFax = string.Empty;
            this._transactionID = string.Empty;
            this._shippedDate = DateTime.MinValue;
            this._trackingID = string.Empty;
        }

        public OrderDetails_Old(int id, DateTime addedDate, string addedBy, int statusID, string statusTitle, string shippingMethod, decimal subTotal, decimal shipping, string shippingFirstName, string shippingLastName, string shippingStreet, string shippingPostalCode, string shippingCity, string shippingState, string shippingCountry, string customerEmail, string customerPhone, string customerFax, DateTime shippedDate, string transactionID, string trackingID)
        {
            this._id = 0;
            this._addedDate = DateTime.Now;
            this._addedBy = string.Empty;
            this._statusID = 0;
            this._statusTitle = string.Empty;
            this._shippingMethod = string.Empty;
            this._subTotal = new decimal();
            this._shipping = new decimal();
            this._shippingFirstName = string.Empty;
            this._shippingLastName = string.Empty;
            this._shippingStreet = string.Empty;
            this._shippingPostalCode = string.Empty;
            this._shippingCity = string.Empty;
            this._shippingState = string.Empty;
            this._shippingCountry = string.Empty;
            this._customerEmail = string.Empty;
            this._customerPhone = string.Empty;
            this._customerFax = string.Empty;
            this._transactionID = string.Empty;
            this._shippedDate = DateTime.MinValue;
            this._trackingID = string.Empty;
            this.ID = id;
            this.AddedDate = addedDate;
            this.AddedBy = addedBy;
            this.StatusID = statusID;
            this.StatusTitle = statusTitle;
            this.ShippingMethod = shippingMethod;
            this.SubTotal = subTotal;
            this.Shipping = shipping;
            this.ShippingFirstName = shippingFirstName;
            this.ShippingLastName = shippingLastName;
            this.ShippingStreet = shippingStreet;
            this.ShippingPostalCode = shippingPostalCode;
            this.ShippingCity = shippingCity;
            this.ShippingState = shippingState;
            this.ShippingCountry = shippingCountry;
            this.CustomerEmail = customerEmail;
            this.CustomerPhone = customerPhone;
            this.CustomerFax = customerFax;
            this.ShippedDate = shippedDate;
            this.TransactionID = transactionID;
            this.TrackingID = trackingID;
        }

        public string AddedBy
        {
            get
            {
                return this._addedBy;
            }
            set
            {
                this._addedBy = value;
            }
        }

        public DateTime AddedDate
        {
            get
            {
                return this._addedDate;
            }
            set
            {
                this._addedDate = value;
            }
        }

        public string CustomerEmail
        {
            get
            {
                return this._customerEmail;
            }
            set
            {
                this._customerEmail = value;
            }
        }

        public string CustomerFax
        {
            get
            {
                return this._customerFax;
            }
            set
            {
                this._customerFax = value;
            }
        }

        public string CustomerPhone
        {
            get
            {
                return this._customerPhone;
            }
            set
            {
                this._customerPhone = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public DateTime ShippedDate
        {
            get
            {
                return this._shippedDate;
            }
            set
            {
                this._shippedDate = value;
            }
        }

        public decimal Shipping
        {
            get
            {
                return this._shipping;
            }
            set
            {
                this._shipping = value;
            }
        }

        public string ShippingCity
        {
            get
            {
                return this._shippingCity;
            }
            set
            {
                this._shippingCity = value;
            }
        }

        public string ShippingCountry
        {
            get
            {
                return this._shippingCountry;
            }
            set
            {
                this._shippingCountry = value;
            }
        }

        public string ShippingFirstName
        {
            get
            {
                return this._shippingFirstName;
            }
            set
            {
                this._shippingFirstName = value;
            }
        }

        public string ShippingLastName
        {
            get
            {
                return this._shippingLastName;
            }
            set
            {
                this._shippingLastName = value;
            }
        }

        public string ShippingMethod
        {
            get
            {
                return this._shippingMethod;
            }
            set
            {
                this._shippingMethod = value;
            }
        }

        public string ShippingPostalCode
        {
            get
            {
                return this._shippingPostalCode;
            }
            set
            {
                this._shippingPostalCode = value;
            }
        }

        public string ShippingState
        {
            get
            {
                return this._shippingState;
            }
            set
            {
                this._shippingState = value;
            }
        }

        public string ShippingStreet
        {
            get
            {
                return this._shippingStreet;
            }
            set
            {
                this._shippingStreet = value;
            }
        }

        public int StatusID
        {
            get
            {
                return this._statusID;
            }
            set
            {
                this._statusID = value;
            }
        }

        public string StatusTitle
        {
            get
            {
                return this._statusTitle;
            }
            set
            {
                this._statusTitle = value;
            }
        }

        public decimal SubTotal
        {
            get
            {
                return this._subTotal;
            }
            set
            {
                this._subTotal = value;
            }
        }

        public string TrackingID
        {
            get
            {
                return this._trackingID;
            }
            set
            {
                this._trackingID = value;
            }
        }

        public string TransactionID
        {
            get
            {
                return this._transactionID;
            }
            set
            {
                this._transactionID = value;
            }
        }
    }
}

