namespace TheBeerHouse.BLL.Store
{
    using System;

    [Serializable]
    public class ShoppingCartItem
    {
        private int _id = 0;
        private int _quantity = 1;
        private string _sku = string.Empty;
        private string _title = string.Empty;
        private decimal _unitPrice;

        public ShoppingCartItem(int id, string title, string sku, decimal unitPrice)
        {
            this.ID = id;
            this.Title = title;
            this.SKU = sku;
            this.UnitPrice = unitPrice;
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            private set
            {
                this._id = value;
            }
        }

        public int Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
            }
        }

        public string SKU
        {
            get
            {
                return this._sku;
            }
            private set
            {
                this._sku = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            private set
            {
                this._title = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this._unitPrice;
            }
            private set
            {
                this._unitPrice = value;
            }
        }
    }
}

