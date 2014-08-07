namespace TheBeerHouse.BLL.Store
{
    using Microsoft.VisualBasic;
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for ShoppingCart.Order in the schema.
    /// </summary>
    /// <KeyProperties>
    /// OrderID
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ShoppingCart", Name="Order")]
    public class Order : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private string _CustomerEmail;
        private string _CustomerFax;
        private string _CustomerPhone;
        private int _OrderID;
        private string _setName = "Orders";
        private DateTime? _ShippedDate;
        private decimal _Shipping;
        private string _ShippingCity;
        private string _ShippingCountry;
        private string _ShippingFirstName;
        private string _ShippingLastName;
        private string _ShippingMethod;
        private string _ShippingPostalCode;
        private string _ShippingState;
        private string _ShippingStreet;
        private int _StatusID;
        private decimal _SubTotal;
        private string _TrackingID;
        private string _TransactionID;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new Order object.
        /// </summary>
        /// <param name="orderID">Initial value of OrderID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="statusID">Initial value of StatusID.</param>
        /// <param name="shippingMethod">Initial value of ShippingMethod.</param>
        /// <param name="subTotal">Initial value of SubTotal.</param>
        /// <param name="shipping">Initial value of Shipping.</param>
        /// <param name="shippingFirstName">Initial value of ShippingFirstName.</param>
        /// <param name="shippingLastName">Initial value of ShippingLastName.</param>
        /// <param name="shippingStreet">Initial value of ShippingStreet.</param>
        /// <param name="shippingPostalCode">Initial value of ShippingPostalCode.</param>
        /// <param name="shippingCity">Initial value of ShippingCity.</param>
        /// <param name="shippingState">Initial value of ShippingState.</param>
        /// <param name="shippingCountry">Initial value of ShippingCountry.</param>
        /// <param name="customerEmail">Initial value of CustomerEmail.</param>
        /// <param name="customerPhone">Initial value of CustomerPhone.</param>
        /// <param name="customerFax">Initial value of CustomerFax.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static Order CreateOrder(int orderID, DateTime addedDate, string addedBy, int statusID, string shippingMethod, decimal subTotal, decimal shipping, string shippingFirstName, string shippingLastName, string shippingStreet, string shippingPostalCode, string shippingCity, string shippingState, string shippingCountry, string customerEmail, string customerPhone, string customerFax, DateTime updatedDate, bool active)
        {
            Order order = new Order();
            order.OrderID = orderID;
            order.AddedDate = addedDate;
            order.AddedBy = addedBy;
            order.StatusID = statusID;
            order.ShippingMethod = shippingMethod;
            order.SubTotal = subTotal;
            order.Shipping = shipping;
            order.ShippingFirstName = shippingFirstName;
            order.ShippingLastName = shippingLastName;
            order.ShippingStreet = shippingStreet;
            order.ShippingPostalCode = shippingPostalCode;
            order.ShippingCity = shippingCity;
            order.ShippingState = shippingState;
            order.ShippingCountry = shippingCountry;
            order.CustomerEmail = customerEmail;
            order.CustomerPhone = customerPhone;
            order.CustomerFax = customerFax;
            order.UpdatedDate = updatedDate;
            order.Active = active;
            return order;
        }

        private void OnAddedByChanging(string value)
        {
            if (!string.IsNullOrEmpty(this.AddedBy))
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

        private void OnAddedDateChanging(DateTime value)
        {
        }

        private void OnCustomerEmailChanging(string value)
        {
        }

        private void OnCustomerFaxChanging(string value)
        {
        }

        private void OnCustomerPhoneChanging(string value)
        {
        }

        private void OnShippedDateChanging(DateTime value)
        {
        }

        private void OnShippingChanging(decimal value)
        {
        }

        private void OnShippingCityChanging(string value)
        {
        }

        private void OnShippingCountryChanging(string value)
        {
        }

        private void OnShippingFirstNameChanging(string value)
        {
        }

        private void OnShippingLastNameChanging(string value)
        {
        }

        private void OnShippingMethodChanging(string value)
        {
        }

        private void OnShippingPostalCodeChanging(string value)
        {
        }

        private void OnShippingStateChanging(string value)
        {
        }

        private void OnShippingStreetChanging(string value)
        {
        }

        private void OnStatusIDChanging(int value)
        {
        }

        private void OnSubTotalChanging(decimal value)
        {
        }

        private void OnTrackingIDChanging(string value)
        {
        }

        private void OnTransactionIDChanging(string value)
        {
        }

        private void OnUpdatedDateChanging(DateTime value)
        {
        }

        /// <summary>
        /// There are no comments for Property Active in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                this.ReportPropertyChanging("Active");
                this._Active = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Active");
            }
        }

        /// <summary>
        /// There are no comments for Property AddedBy in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string AddedBy
        {
            get
            {
                return this._AddedBy;
            }
            set
            {
                this.OnAddedByChanging(value);
                this.ReportPropertyChanging("AddedBy");
                this._AddedBy = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AddedBy");
            }
        }

        /// <summary>
        /// There are no comments for Property AddedDate in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public DateTime AddedDate
        {
            get
            {
                return this._AddedDate;
            }
            set
            {
                this.OnAddedDateChanging(value);
                this.ReportPropertyChanging("AddedDate");
                this._AddedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AddedDate");
            }
        }

        public bool CanAdd
        {
            get
            {
                return true;
            }
        }

        public bool CanDelete
        {
            get
            {
                return true;
            }
        }

        public bool CanEdit
        {
            get
            {
                return true;
            }
        }

        public bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// There are no comments for Property CustomerEmail in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string CustomerEmail
        {
            get
            {
                return this._CustomerEmail;
            }
            set
            {
                this.OnCustomerEmailChanging(value);
                this.ReportPropertyChanging("CustomerEmail");
                this._CustomerEmail = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CustomerEmail");
            }
        }

        /// <summary>
        /// There are no comments for Property CustomerFax in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string CustomerFax
        {
            get
            {
                return this._CustomerFax;
            }
            set
            {
                this.OnCustomerFaxChanging(value);
                this.ReportPropertyChanging("CustomerFax");
                this._CustomerFax = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CustomerFax");
            }
        }

        /// <summary>
        /// There are no comments for Property CustomerPhone in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string CustomerPhone
        {
            get
            {
                return this._CustomerPhone;
            }
            set
            {
                this.OnCustomerPhoneChanging(value);
                this.ReportPropertyChanging("CustomerPhone");
                this._CustomerPhone = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CustomerPhone");
            }
        }

        public double GrandTotal
        {
            get
            {
                return Convert.ToDouble(decimal.Add(this.SubTotal, this.Shipping));
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsDirty
        {
            get
            {
                return this.bIsDirty;
            }
            set
            {
                this.bIsDirty = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsValid
        {
            get
            {
                return (((((((!string.IsNullOrEmpty(this.CustomerEmail) & !string.IsNullOrEmpty(this.ShippingCity)) & !string.IsNullOrEmpty(this.ShippingFirstName)) & !string.IsNullOrEmpty(this.ShippingLastName)) & !string.IsNullOrEmpty(this.ShippingState)) & !string.IsNullOrEmpty(this.ShippingStreet)) & (0 < this.OrderItems.Count)) & (0.0 < this.GrandTotal));
            }
        }

        /// <summary>
        /// There are no comments for Property OrderID in the schema.
        /// </summary>
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int OrderID
        {
            get
            {
                return this._OrderID;
            }
            set
            {
                this.ReportPropertyChanging("OrderID");
                this._OrderID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OrderID");
            }
        }

        /// <summary>
        /// There are no comments for OrderItems in the schema.
        /// </summary>
        [XmlIgnore, SoapIgnore, DataMember, EdmRelationshipNavigationProperty("ShoppingCart", "FK_tbh_OrderItems_tbh_Orders", "tbh_OrderItems")]
        public EntityCollection<OrderItem> OrderItems
        {
            get
            {
                return this.RelationshipManager.GetRelatedCollection<OrderItem>("ShoppingCart.FK_tbh_OrderItems_tbh_Orders", "tbh_OrderItems");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedCollection<OrderItem>("ShoppingCart.FK_tbh_OrderItems_tbh_Orders", "tbh_OrderItems", value);
                }
            }
        }

        /// <summary>
        /// Returns the name of the Data Set the Entity belongs to. Needs to be set
        /// in the derived class.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SetName
        {
            get
            {
                return this._setName;
            }
            set
            {
                this._setName = value;
            }
        }

        /// <summary>
        /// There are no comments for Property ShippedDate in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public DateTime? ShippedDate
        {
            get
            {
                return this._ShippedDate;
            }
            set
            {
                this.ReportPropertyChanging("ShippedDate");
                this._ShippedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ShippedDate");
            }
        }

        /// <summary>
        /// There are no comments for Property Shipping in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public decimal Shipping
        {
            get
            {
                return this._Shipping;
            }
            set
            {
                this.OnShippingChanging(value);
                this.ReportPropertyChanging("Shipping");
                this._Shipping = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Shipping");
            }
        }

        /// <summary>
        /// There are no comments for Property ShippingCity in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string ShippingCity
        {
            get
            {
                return this._ShippingCity;
            }
            set
            {
                this.OnShippingCityChanging(value);
                this.ReportPropertyChanging("ShippingCity");
                this._ShippingCity = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShippingCity");
            }
        }

        /// <summary>
        /// There are no comments for Property ShippingCountry in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string ShippingCountry
        {
            get
            {
                return this._ShippingCountry;
            }
            set
            {
                this.OnShippingCountryChanging(value);
                this.ReportPropertyChanging("ShippingCountry");
                this._ShippingCountry = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShippingCountry");
            }
        }

        /// <summary>
        /// There are no comments for Property ShippingFirstName in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string ShippingFirstName
        {
            get
            {
                return this._ShippingFirstName;
            }
            set
            {
                this.OnShippingFirstNameChanging(value);
                this.ReportPropertyChanging("ShippingFirstName");
                this._ShippingFirstName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShippingFirstName");
            }
        }

        /// <summary>
        /// There are no comments for Property ShippingLastName in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string ShippingLastName
        {
            get
            {
                return this._ShippingLastName;
            }
            set
            {
                this.OnShippingLastNameChanging(value);
                this.ReportPropertyChanging("ShippingLastName");
                this._ShippingLastName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShippingLastName");
            }
        }

        /// <summary>
        /// There are no comments for Property ShippingMethod in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string ShippingMethod
        {
            get
            {
                return this._ShippingMethod;
            }
            set
            {
                this.OnShippingMethodChanging(value);
                this.ReportPropertyChanging("ShippingMethod");
                this._ShippingMethod = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShippingMethod");
            }
        }

        /// <summary>
        /// There are no comments for Property ShippingPostalCode in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string ShippingPostalCode
        {
            get
            {
                return this._ShippingPostalCode;
            }
            set
            {
                this.OnShippingPostalCodeChanging(value);
                this.ReportPropertyChanging("ShippingPostalCode");
                this._ShippingPostalCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShippingPostalCode");
            }
        }

        /// <summary>
        /// There are no comments for Property ShippingState in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string ShippingState
        {
            get
            {
                return this._ShippingState;
            }
            set
            {
                this.OnShippingStateChanging(value);
                this.ReportPropertyChanging("ShippingState");
                this._ShippingState = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShippingState");
            }
        }

        /// <summary>
        /// There are no comments for Property ShippingStreet in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string ShippingStreet
        {
            get
            {
                return this._ShippingStreet;
            }
            set
            {
                this.OnShippingStreetChanging(value);
                this.ReportPropertyChanging("ShippingStreet");
                this._ShippingStreet = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShippingStreet");
            }
        }

        /// <summary>
        /// There are no comments for Property StatusID in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int StatusID
        {
            get
            {
                return this._StatusID;
            }
            set
            {
                this.OnStatusIDChanging(value);
                this.ReportPropertyChanging("StatusID");
                this._StatusID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("StatusID");
            }
        }

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
                }
                using (OrderStatusesRepository lOrderStatusrpt = new OrderStatusesRepository())
                {
                    OrderStatus lOrderStatus = lOrderStatusrpt.GetOrderStatusById(this.StatusID);
                    if (!Information.IsNothing(lOrderStatus))
                    {
                        return lOrderStatus.Title;
                    }
                    return "Unknown Status";
                }
            }
        }

        /// <summary>
        /// There are no comments for Property SubTotal in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public decimal SubTotal
        {
            get
            {
                return this._SubTotal;
            }
            set
            {
                this.OnSubTotalChanging(value);
                this.ReportPropertyChanging("SubTotal");
                this._SubTotal = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SubTotal");
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanAdd
        {
            get
            {
                return true;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanDelete
        {
            get
            {
                return true;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanEdit
        {
            get
            {
                return true;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.CanRead
        {
            get
            {
                return true;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.IsDirty
        {
            get
            {
                return this.bIsDirty;
            }
            set
            {
                this.bIsDirty = value;
            }
        }

        public bool TheBeerHouse.BLL.IBaseEntity.IsValid
        {
            get
            {
                return (((((((!string.IsNullOrEmpty(this.CustomerEmail) & !string.IsNullOrEmpty(this.ShippingCity)) & !string.IsNullOrEmpty(this.ShippingFirstName)) & !string.IsNullOrEmpty(this.ShippingLastName)) & !string.IsNullOrEmpty(this.ShippingState)) & !string.IsNullOrEmpty(this.ShippingStreet)) & (0 < this.OrderItems.Count)) & (0.0 < this.GrandTotal));
            }
        }

        public string TheBeerHouse.BLL.IBaseEntity.SetName
        {
            get
            {
                return this._setName;
            }
            set
            {
                this._setName = value;
            }
        }

        /// <summary>
        /// There are no comments for Property TrackingID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string TrackingID
        {
            get
            {
                return this._TrackingID;
            }
            set
            {
                this.OnTrackingIDChanging(value);
                this.ReportPropertyChanging("TrackingID");
                this._TrackingID = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TrackingID");
            }
        }

        /// <summary>
        /// There are no comments for Property TransactionID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string TransactionID
        {
            get
            {
                return this._TransactionID;
            }
            set
            {
                this.OnTransactionIDChanging(value);
                this.ReportPropertyChanging("TransactionID");
                this._TransactionID = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TransactionID");
            }
        }

        /// <summary>
        /// There are no comments for Property UpdatedBy in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string UpdatedBy
        {
            get
            {
                return this._UpdatedBy;
            }
            set
            {
                this.ReportPropertyChanging("UpdatedBy");
                this._UpdatedBy = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UpdatedBy");
            }
        }

        /// <summary>
        /// There are no comments for Property UpdatedDate in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public DateTime UpdatedDate
        {
            get
            {
                return this._UpdatedDate;
            }
            set
            {
                this.OnUpdatedDateChanging(value);
                this.ReportPropertyChanging("UpdatedDate");
                this._UpdatedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UpdatedDate");
            }
        }
    }
}

