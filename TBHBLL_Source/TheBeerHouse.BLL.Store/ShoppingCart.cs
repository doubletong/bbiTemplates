namespace TheBeerHouse.BLL.Store
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    [Serializable]
    public class ShoppingCart
    {
        private Dictionary<int, ShoppingCartItem> _items = new Dictionary<int, ShoppingCartItem>();

        public void Clear()
        {
            this._items.Clear();
        }

        public void DeleteItem(int id)
        {
            if (this._items.ContainsKey(id))
            {
                ShoppingCartItem item = this._items[id];
                ShoppingCartItem VB$t_ref$S0 = item;
                VB$t_ref$S0.Quantity--;
                if (item.Quantity == 0)
                {
                    this._items.Remove(id);
                }
            }
        }

        public void DeleteProduct(int id)
        {
            if (this._items.ContainsKey(id))
            {
                this._items.Remove(id);
            }
        }

        public void InsertItem(int id, string title, string sku, decimal unitPrice)
        {
            if (this._items.ContainsKey(id))
            {
                ShoppingCartItem VB$t_ref$S0 = this._items[id];
                VB$t_ref$S0.Quantity++;
            }
            else
            {
                this._items.Add(id, new ShoppingCartItem(id, title, sku, unitPrice));
            }
        }

        public void UpdateItemQuantity(int id, int quantity)
        {
            if (this._items.ContainsKey(id))
            {
                ShoppingCartItem item = this._items[id];
                item.Quantity = quantity;
                if (item.Quantity <= 0)
                {
                    this._items.Remove(id);
                }
            }
        }

        public ICollection Items
        {
            get
            {
                return this._items.Values;
            }
        }

        public decimal Total
        {
            get
            {
                decimal sum = new decimal();
                foreach (ShoppingCartItem item in this._items.Values)
                {
                    sum = decimal.Add(sum, decimal.Multiply(item.UnitPrice, new decimal(item.Quantity)));
                }
                return sum;
            }
        }
    }
}

