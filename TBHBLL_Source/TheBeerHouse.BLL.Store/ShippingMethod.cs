namespace TheBeerHouse.BLL.Store
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for ShoppingCart.ShippingMethod in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ShippingMethodID
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ShoppingCart", Name="ShippingMethod")]
    public class ShippingMethod : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private decimal _Price;
        private string _setName = "ShippingMethods";
        private int _ShippingMethodID;
        private string _Title;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new ShippingMethod object.
        /// </summary>
        /// <param name="shippingMethodID">Initial value of ShippingMethodID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="title">Initial value of Title.</param>
        /// <param name="price">Initial value of Price.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static ShippingMethod CreateShippingMethod(int shippingMethodID, DateTime addedDate, string addedBy, string title, decimal price, DateTime updatedDate, bool active)
        {
            ShippingMethod shippingMethod = new ShippingMethod();
            shippingMethod.ShippingMethodID = shippingMethodID;
            shippingMethod.AddedDate = addedDate;
            shippingMethod.AddedBy = addedBy;
            shippingMethod.Title = title;
            shippingMethod.Price = price;
            shippingMethod.UpdatedDate = updatedDate;
            shippingMethod.Active = active;
            return shippingMethod;
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

        private void OnPriceChanging(decimal value)
        {
        }

        private void OnTitleChanging(string value)
        {
        }

        private void OnUpdatedDateChanging(DateTime value)
        {
        }

        /// <summary>
        /// There are no comments for Property Active in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
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
        [EdmScalarProperty(IsNullable=false), DataMember]
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
                return (!string.IsNullOrEmpty(this.Title) & (decimal.Compare(this.Price, decimal.Zero) > 0));
            }
        }

        /// <summary>
        /// There are no comments for Property Price in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public decimal Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this.OnPriceChanging(value);
                this.ReportPropertyChanging("Price");
                this._Price = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Price");
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
        /// There are no comments for Property ShippingMethodID in the schema.
        /// </summary>
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int ShippingMethodID
        {
            get
            {
                return this._ShippingMethodID;
            }
            set
            {
                this.ReportPropertyChanging("ShippingMethodID");
                this._ShippingMethodID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ShippingMethodID");
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
                return (!string.IsNullOrEmpty(this.Title) & (decimal.Compare(this.Price, decimal.Zero) > 0));
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

        public string TitleAndPrice
        {
            get
            {
                return string.Format("{0} ({1:N2} {2})", this.Title, this.Price, TheBeerHouse.Globals.Settings.Store.CurrencyCode);
            }
        }

        /// <summary>
        /// There are no comments for Property UpdatedBy in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
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

