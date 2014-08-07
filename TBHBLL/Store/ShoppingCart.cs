using System;
using System.Collections;
using System.Collections.Generic;

namespace BBICMS.Store
{
    [Serializable()]
    public class ShoppingCartItem
    {
        // ==========
        // Private Variables
        // ==========

        private int _id = 0;
        private int _quantity = 1;
        private string _sku = string.Empty;
        private string _title = string.Empty;
        private decimal _unitPrice;

        public ShoppingCartItem(int id, string title, string sku, decimal unitPrice)
        {
            ID = id;
            Title = title;
            SKU = sku;
            UnitPrice = unitPrice;
           
        }

        // ==========
        // Properties
        // ==========

        public int ID
        {
            get { return _id; }
            private set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            private set { _title = value; }
        }

        public string SKU
        {
            get { return _sku; }
            private set { _sku = value; }
        }

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            private set { _unitPrice = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        // ==========
        // Constructor
        // ==========
    }

    [Serializable()]
    public class ShoppingCart
    {
        private Dictionary<int, ShoppingCartItem> _items = new Dictionary<int, ShoppingCartItem>();

        public ICollection Items
        {
            get { return _items.Values; }
        }

        // Gets the sum total of the items' prices
        public decimal Total
        {
            get
            {
                decimal sum = 0.0M;
                foreach (ShoppingCartItem item in _items.Values)
                {
                    sum += item.UnitPrice*item.Quantity;
                }
                return sum;
            }
        }

        // Adds a new item to the shopping cart
        public void InsertItem(int id, string title, string sku, decimal unitPrice)
        {
            if (_items.ContainsKey(id))
            {
                _items[id].Quantity += 1;
            }
            else
            {
                _items.Add(id, new ShoppingCartItem(id, title, sku, unitPrice));
            }
        }

        // Removes an item from the shopping cart
        public void DeleteItem(int id)
        {
            if (_items.ContainsKey(id))
            {
                ShoppingCartItem item = _items[id];
                item.Quantity -= 1;
                if (item.Quantity == 0) _items.Remove(id);
            }
        }

        // Removes all items of a specified product from the shopping cart
        public void DeleteProduct(int id)
        {
            if (_items.ContainsKey(id))
            {
                _items.Remove(id);
            }
        }

        // Updates the quantity for an item
        public void UpdateItemQuantity(int id, int quantity)
        {
            if (_items.ContainsKey(id))
            {
                ShoppingCartItem item = _items[id];
                item.Quantity = quantity;
                if (item.Quantity <= 0) _items.Remove(id);
            }
        }

        // Clears the cart
        public void Clear()
        {
            _items.Clear();
        }
    }
}