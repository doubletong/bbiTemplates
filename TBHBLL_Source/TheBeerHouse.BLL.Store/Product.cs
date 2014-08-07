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
    /// There are no comments for ShoppingCart.Product in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ProductID
    /// </KeyProperties>
    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ShoppingCart", Name="Product")]
    public class Product : EntityObject, IBaseEntity
    {
        private bool _Active;
        private string _AddedBy;
        private DateTime _AddedDate;
        private string _Description;
        private int _DiscountPercentage;
        private string _FullImageUrl;
        private int _ProductID;
        private string _setName = "Products";
        private string _SKU;
        private string _SmallImageUrl;
        private string _Title;
        private int _TotalRating;
        private decimal _UnitPrice;
        private int _UnitsInStock;
        private string _UpdatedBy;
        private DateTime _UpdatedDate;
        private int _Votes;
        private bool bIsDirty = false;

        /// <summary>
        /// Create a new Product object.
        /// </summary>
        /// <param name="productID">Initial value of ProductID.</param>
        /// <param name="addedDate">Initial value of AddedDate.</param>
        /// <param name="addedBy">Initial value of AddedBy.</param>
        /// <param name="title">Initial value of Title.</param>
        /// <param name="description">Initial value of Description.</param>
        /// <param name="sKU">Initial value of SKU.</param>
        /// <param name="unitPrice">Initial value of UnitPrice.</param>
        /// <param name="discountPercentage">Initial value of DiscountPercentage.</param>
        /// <param name="unitsInStock">Initial value of UnitsInStock.</param>
        /// <param name="votes">Initial value of Votes.</param>
        /// <param name="totalRating">Initial value of TotalRating.</param>
        /// <param name="updatedDate">Initial value of UpdatedDate.</param>
        /// <param name="active">Initial value of Active.</param>
        public static Product CreateProduct(int productID, DateTime addedDate, string addedBy, string title, string description, string sKU, decimal unitPrice, int discountPercentage, int unitsInStock, int votes, int totalRating, DateTime updatedDate, bool active)
        {
            Product product = new Product();
            product.ProductID = productID;
            product.AddedDate = addedDate;
            product.AddedBy = addedBy;
            product.Title = title;
            product.Description = description;
            product.SKU = sKU;
            product.UnitPrice = unitPrice;
            product.DiscountPercentage = discountPercentage;
            product.UnitsInStock = unitsInStock;
            product.Votes = votes;
            product.TotalRating = totalRating;
            product.UpdatedDate = updatedDate;
            product.Active = active;
            return product;
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

        private void OnDepartmentIDChanging(int value)
        {
        }

        private void OnDescriptionChanging(string value)
        {
        }

        private void OnDiscountPercentageChanging(int value)
        {
        }

        private void OnFullImageUrlChanging(string value)
        {
        }

        private void OnSKUChanging(string value)
        {
        }

        private void OnSmallImageUrlChanging(string value)
        {
        }

        private void OnTitleChanging(string value)
        {
        }

        private void OnTotalRatingChanging(int value)
        {
        }

        private void OnUnitPriceChanging(decimal value)
        {
        }

        private void OnUnitsInStockChanging(int value)
        {
        }

        private void OnUpdatedDateChanging(DateTime value)
        {
        }

        private void OnVotesChanging(int value)
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

        public decimal AverageRating
        {
            get
            {
                if (this.Votes > 0)
                {
                    return new decimal(((double) this.TotalRating) / ((double) this.Votes));
                }
                return decimal.Zero;
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
        /// There are no comments for Department in the schema.
        /// </summary>
        [XmlIgnore, EdmRelationshipNavigationProperty("ShoppingCart", "FK_tbh_Products_tbh_Departments", "tbh_Departments"), SoapIgnore, DataMember]
        public TheBeerHouse.BLL.Store.Department Department
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Store.Department>("ShoppingCart.FK_tbh_Products_tbh_Departments", "tbh_Departments").Value;
            }
            set
            {
                this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Store.Department>("ShoppingCart.FK_tbh_Products_tbh_Departments", "tbh_Departments").Value = value;
            }
        }

        public int DepartmentId
        {
            get
            {
                if (!Information.IsNothing(this.DepartmentReference.EntityKey))
                {
                    return Conversions.ToInteger(this.DepartmentReference.EntityKey.EntityKeyValues[0].Value);
                }
                return 0;
            }
            set
            {
                if (!Information.IsNothing(this.DepartmentReference.EntityKey))
                {
                    this.DepartmentReference = null;
                }
                this.DepartmentReference.EntityKey = new EntityKey("ShoppingEntities.Department", "DepartmentID", value);
            }
        }

        public string DepartmentName
        {
            get
            {
                if (!Information.IsNothing(this.Department))
                {
                    return this.Department.Title;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// There are no comments for Department in the schema.
        /// </summary>
        [DataMember, Browsable(false)]
        public EntityReference<TheBeerHouse.BLL.Store.Department> DepartmentReference
        {
            get
            {
                return this.RelationshipManager.GetRelatedReference<TheBeerHouse.BLL.Store.Department>("ShoppingCart.FK_tbh_Products_tbh_Departments", "tbh_Departments");
            }
            set
            {
                if (value != null)
                {
                    this.RelationshipManager.InitializeRelatedReference<TheBeerHouse.BLL.Store.Department>("ShoppingCart.FK_tbh_Products_tbh_Departments", "tbh_Departments", value);
                }
            }
        }

        /// <summary>
        /// There are no comments for Property Description in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.OnDescriptionChanging(value);
                this.ReportPropertyChanging("Description");
                this._Description = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Description");
            }
        }

        /// <summary>
        /// There are no comments for Property DiscountPercentage in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int DiscountPercentage
        {
            get
            {
                return this._DiscountPercentage;
            }
            set
            {
                this.OnDiscountPercentageChanging(value);
                this.ReportPropertyChanging("DiscountPercentage");
                this._DiscountPercentage = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("DiscountPercentage");
            }
        }

        public decimal FinalUnitPrice
        {
            get
            {
                if (this.DiscountPercentage > 0)
                {
                    return decimal.Subtract(this.UnitPrice, decimal.Divide(decimal.Multiply(this.UnitPrice, new decimal(this.DiscountPercentage)), 100M));
                }
                return this.UnitPrice;
            }
        }

        /// <summary>
        /// There are no comments for Property FullImageUrl in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty]
        public string FullImageUrl
        {
            get
            {
                return this._FullImageUrl;
            }
            set
            {
                this.OnFullImageUrlChanging(value);
                this.ReportPropertyChanging("FullImageUrl");
                this._FullImageUrl = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("FullImageUrl");
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
                return (((!string.IsNullOrEmpty(this.Title) & !string.IsNullOrEmpty(this.SKU)) & !string.IsNullOrEmpty(this.Description)) & (this.DepartmentId > 0));
            }
        }

        /// <summary>
        /// There are no comments for Property ProductID in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int ProductID
        {
            get
            {
                return this._ProductID;
            }
            set
            {
                this.ReportPropertyChanging("ProductID");
                this._ProductID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProductID");
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

        /// <summary>
        /// There are no comments for Property SmallImageUrl in the schema.
        /// </summary>
        [EdmScalarProperty, DataMember]
        public string SmallImageUrl
        {
            get
            {
                return this._SmallImageUrl;
            }
            set
            {
                this.OnSmallImageUrlChanging(value);
                this.ReportPropertyChanging("SmallImageUrl");
                this._SmallImageUrl = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("SmallImageUrl");
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
                return (((!string.IsNullOrEmpty(this.Title) & !string.IsNullOrEmpty(this.SKU)) & !string.IsNullOrEmpty(this.Description)) & (this.DepartmentId > 0));
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
        [DataMember, EdmScalarProperty(IsNullable=false)]
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
        /// There are no comments for Property TotalRating in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int TotalRating
        {
            get
            {
                return this._TotalRating;
            }
            set
            {
                this.OnTotalRatingChanging(value);
                this.ReportPropertyChanging("TotalRating");
                this._TotalRating = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("TotalRating");
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
        /// There are no comments for Property UnitsInStock in the schema.
        /// </summary>
        [DataMember, EdmScalarProperty(IsNullable=false)]
        public int UnitsInStock
        {
            get
            {
                return this._UnitsInStock;
            }
            set
            {
                this.OnUnitsInStockChanging(value);
                this.ReportPropertyChanging("UnitsInStock");
                this._UnitsInStock = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UnitsInStock");
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

        /// <summary>
        /// There are no comments for Property Votes in the schema.
        /// </summary>
        [EdmScalarProperty(IsNullable=false), DataMember]
        public int Votes
        {
            get
            {
                return this._Votes;
            }
            set
            {
                this.OnVotesChanging(value);
                this.ReportPropertyChanging("Votes");
                this._Votes = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Votes");
            }
        }
    }
}

