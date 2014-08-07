using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;

namespace BBICMS.Store
{
    public class OrdersRepository : BaseShoppingCartRepository
    {
        #region " BLL/DAL Methods "

        public List<Order> GetOrdersByUser(string vUserName)
        {
            string key = CacheKey + "_" + vUserName + "_List";

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>) Cache[key];
            }

            Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> lOrders = (from lOrder in Shoppingctx.Orders.Include("OrderItems")
                                   where lOrder.AddedBy == vUserName
                                   orderby lOrder.AddedDate descending
                                   select lOrder).ToList();

            if (EnableCaching)
            {
                CacheData(key, lOrders, CacheDuration);
            }

            return lOrders;
        }

        public List<Order> GetOrders()
        {
            string key = CacheKey + "_List";

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>) Cache[key];
            }

            Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> lOrders = (from lOrder in Shoppingctx.Orders.Include("OrderItems")
                                   select lOrder).ToList();

            if (EnableCaching)
            {
                CacheData(key, lOrders, CacheDuration);
            }

            return lOrders;
        }

        public List<Order> GetOrdersByOrderStatusId(int vOrderStatusId)
        {
            string key = CacheKey + "_ByOrderStatusId_List_" + vOrderStatusId;

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>) Cache[key];
            }

            Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> lOrders = (from lOrder in Shoppingctx.Orders.Include("OrderItems")
                                   where lOrder.StatusID == vOrderStatusId
                                   select lOrder).ToList();

            if (EnableCaching)
            {
                CacheData(key, lOrders, CacheDuration);
            }

            return lOrders;
        }

        public List<Order> GetOrdersByCustomerName(string vCustomerName)
        {
            string key = CacheKey + "_ByCustomerName_List_" + vCustomerName;

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>)Cache[key];
            }

            Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> lOrders = (from lOrder in Shoppingctx.Orders.Include("OrderItems")
                                   where lOrder.AddedBy.StartsWith(vCustomerName)
                                   select lOrder).ToList();

            if (EnableCaching)
            {
                CacheData(key, lOrders, CacheDuration);
            }

            return lOrders;
        }

        public List<Order> GetOrdersByDateRange(DateTime vFromDate, DateTime vToDate)
        {
            Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> lOrders = default(List<Order>);

            if ((vFromDate == null) == false | (vToDate == null) == false)
            {
            }

            lOrders = (from lOrder in Shoppingctx.Orders.Include("OrderItems")
                       where lOrder.AddedDate >= vFromDate &&
                             lOrder.AddedDate <= vToDate
                       select lOrder).ToList();

            return lOrders;
        }

        public Order GetOrderById(int OrderId)
        {
            string key = CacheKey + "_" + OrderId;

            if (EnableCaching && (Cache[key] != null))
            {
                return (Order) Cache[key];
            }

            Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            Order lOrder = (from lai in Shoppingctx.Orders.Include("OrderItems")
                            where lai.OrderID == OrderId
                            select lai).FirstOrDefault();

            if (EnableCaching)
            {
                CacheData(key, lOrder, CacheDuration);
            }

            return lOrder;
        }

        public int GetOrderCount()
        {
            return (from lai in Shoppingctx.Orders select lai).Count();
        }

        public Order AddOrder(Order vOrder)
        {
            try
            {
                if (vOrder.EntityState == EntityState.Detached)
                {
                    Shoppingctx.AddToOrders(vOrder);
                }
                base.PurgeCacheItems(CacheKey);

                return Shoppingctx.SaveChanges() > 0 ? vOrder : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Order InsertOrder(ShoppingCart vshoppingCart, string shippingMethod, decimal shipping,
                                 string shippingFirstName, string shippingLastName, string shippingStreet,
                                 string shippingPostalCode, string shippingCity, string shippingState,
                                 string shippingCountry,string customerEmail, string customerPhone, 
                                string customerFax, string transactionID)
        {
            Order lOrder;

            string userName = Helpers.CurrentUserName;

            // insert the master order
            lOrder = Order.CreateOrder(0, DateTime.Now, userName, 1, shippingMethod, vshoppingCart.Total, shipping,
                                       shippingFirstName, shippingLastName, shippingStreet,
                                       shippingPostalCode, shippingCity, shippingState, shippingCountry, customerEmail,
                                       customerPhone, customerFax, DateTime.Now, true);

            lOrder.TransactionID = transactionID;

            //insert the child order items
            foreach (ShoppingCartItem item in vshoppingCart.Items)
            {
                lOrder.OrderItems.Add(OrderItem.CreateOrderItem(0, DateTime.Now, userName, item.ID, 
                    item.Title, item.SKU, item.UnitPrice, item.Quantity, DateTime.Now));
            }

            lOrder = AddOrder(lOrder);


            return lOrder;
        }

        public bool DeleteOrder(Order vOrder)
        {
            return ChangeDeletedState(vOrder, false);
        }

        public bool UnDeleteOrder(Order vOrder)
        {
            return ChangeDeletedState(vOrder, true);
        }

        private bool ChangeDeletedState(Order vOrder, bool vState)
        {
            vOrder.Active = vState;
            vOrder.UpdatedDate = DateTime.Now;
            vOrder.UpdatedBy = CurrentUserName;

            try
            {
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vOrder.OrderID.ToString(), ex);

                return false;
            }
        }

        #endregion
    }
}