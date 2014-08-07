namespace TheBeerHouse.BLL.Store
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for ShoppingCart.OrderItem in the schema.
    /// </summary>
    /// <KeyProperties>
    /// OrderItemID
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ShoppingCart", Name="OrderItem")]
    public class OrderItem : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private int _OrderItemID;
        private int _ProductID;
        private int _Quantity;
        private string _setName = "OrderItems";
        private string _SKU;
        private string _Title;
        private decimal _UnitPrice;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new OrderItem object.
        /// </summary>
        /// <param name="orderItemID">Initial value of OrderItemID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="productID">Initial value of ProductID.</param>
        /// <param name="title">Initial value of Title.</param>
        /// <param name="sKU">Initial value of SKU.</param>
        /// <param name="unitPrice">Initial value of UnitPrice.</param>
        /// <param name="quantity">Initial value of Quantity.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static OrderItem CreateOrderItem(int orderItemID, DateTime addedDate, string addedBy, int productID, string title, string sKU, decimal unitPrice, int quantity, DateTime updatedDate, bool active)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderItemID = orderItemID;
            orderItem.AddedDate = addedDate;
            orderItem.AddedBy = addedBy;
            orderItem.ProductID = productID;
            orderItem.Title = title;
            orderItem.SKU = sKU;
            orderItem.UnitPrice = unitPrice;
            orderItem.Quantity = quantity;
            orderItem.UpdatedDate = updatedDate;
            orderItem.Active = active;
            return orderItem;
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

        private void OnOrderIDChanging(int value)
        {
        }

        private void OnProductIDChanging(int value)
        {
        }

        private void OnQuantityChanging(int value)
        {
        }

        private void OnSKUChanging(string value)
        {
        }

        private void OnTitleChanging(string value)
        {
        }

        private void OnUnitPriceChanging(decimal value)
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
                return ((((!string.IsNullOrEmpty(this.Title) & !string.IsNullOrEmpty(this.SKU)) & (this.Quantity > 0)) & (this.ProductID > 0)) & (decimal.Compare(this.UnitPrice, decimal.Zero) > 0));
            }
        }

        /// <summary>
        /// There are no comments for Order in the schema.
        /// </summary>
        [SoapIgnore, DataMember, EdmRelationshipNavigationProperty("ShoppingCart", "FK_tbh_OrderItems_tbh_Orders", "tbh_Orders"), XmlIgnore]
        public TheBeerHouse.BLL.Store.Order Order
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Store.Order>("ShoppingCart.FK_tbh_OrderItems_tbh_Orders", "tbh_Orders").Value;
            }
            set
            {
                this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Store.Order>("ShoppingCart.FK_tbh_OrderItems_tbh_Orders", "tbh_Orders").Value = value;
            }
        }

        public int OrderId
        {
            get
            {
                if (!Information.IsNothing(this.OrderReference.EntityKey))
                {
                    return Conversions.ToInteger(this.OrderReference.EntityKey.EntityKeyValues[0].Value);
                }
                return 0;
            }
            set
            {
                if (!Information.IsNothing(this.OrderReference.EntityKey))
                {
                    this.OrderReference = null;
                }
                this.OrderReference.EntityKey = new EntityKey("ShoppingEntities.Orders", "OrderID", value);
            }
        }

        /// <summary>
        /// There are no comments for Property OrderItemID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int OrderItemID
        {
            get
            {
                return this._OrderItemID;
            }
            set
            {
                this.ReportPropertyChanging("OrderItemID");
                this._OrderItemID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OrderItemID");
            }
        }

        /// <summary>
        /// There are no comments for Order in the schema.
        /// </summary>
        [DataMember, Browsable(false)]
        public EntityReference<TheBeerHouse.BLL.Store.Order> OrderReference
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Store.Order>("ShoppingCart.FK_tbh_OrderItems_tbh_Orders", "tbh_Orders");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedReference<TheBeerHouse.BLL.Store.Order>("ShoppingCart.FK_tbh_OrderItems_tbh_Orders", "tbh_Orders", value);
                }
            }
        }

        /// <summary>
        /// There are no comments for Property ProductID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int ProductID
        {
            get
            {
                return this._ProductID;
            }
            set
            {
                this.OnProductIDChanging(value);
                this.ReportPropertyChanging("ProductID");
                this._ProductID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProductID");
            }
        }

        /// <summary>
        /// There are no comments for Property Quantity in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this.OnQuantityChanging(value);
                this.ReportPropertyChanging("Quantity");
                this._Quantity = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Quantity");
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
        /// There are no comments for Property SKU in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string SKU
        {
            get
            {
                return this._SKU;
            }
            set
            {
                this.OnSKUChanging(value);
                this.ReportPropertyChanging("SKU");
                this._SKU = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("SKU");
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
                return ((((!string.IsNullOrEmpty(this.Title) & !string.IsNullOrEmpty(this.SKU)) & (this.Quantity > 0)) & (this.ProductID > 0)) & (decimal.Compare(this.UnitPrice, decimal.Zero) > 0));
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
        /// There are no comments for Property Title in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this.OnTitleChanging(value);
                this.ReportPropertyChanging("Title");
                this._Title = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Title");
            }
        }

        /// <summary>
        /// There are no comments for Property UnitPrice in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public decimal UnitPrice
        {
            get
            {
                return this._UnitPrice;
            }
            set
            {
                this.OnUnitPriceChanging(value);
                this.ReportPropertyChanging("UnitPrice");
                this._UnitPrice = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UnitPrice");
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
        [DataMember, EdmScalarProperty(IsNullable=false)]
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

