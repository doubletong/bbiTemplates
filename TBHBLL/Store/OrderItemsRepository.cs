using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace BBICMS.Store
{

    public class OrderItemsRepository : BaseShoppingCartRepository
    {

        #region " BLL/DAL Methods "

        public List<OrderItem> GetOrderItems()
        {
            return (from lOrderItem in Shoppingctx.OrderItems
                    select lOrderItem).ToList();
        }

        public OrderItem GetOrderItemById(int OrderItemId)
        {
            return (from lOrderItem in Shoppingctx.OrderItems
                    where lOrderItem.OrderItemID == OrderItemId
                    select lOrderItem).FirstOrDefault();
        }

        public int GetOrderItemCount()
        {
            return (from lOrderItem in Shoppingctx.OrderItems
                    select lOrderItem).Count();
        }

        public OrderItem AddOrderItem(int vOrderItemID, System.DateTime vAddedDate, int vOrderID, int vProductID, string vTitle, string vSKU, decimal vUnitPrice, int vQuantity, System.DateTime vUpdatedDate)
        {

            OrderItem lOrderItem;

            if (vOrderItemID > 0)
            {

                lOrderItem = GetOrderItemById(vOrderItemID);

                lOrderItem.OrderItemID = vOrderItemID;
                lOrderItem.AddedDate = vAddedDate;
                lOrderItem.OrderId = vOrderID;
                lOrderItem.ProductID = vProductID;
                lOrderItem.Title = vTitle;
                lOrderItem.SKU = vSKU;
                lOrderItem.UnitPrice = vUnitPrice;
                lOrderItem.Quantity = vQuantity;
                lOrderItem.UpdatedDate = vUpdatedDate;


                lOrderItem.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lOrderItem = OrderItem.CreateOrderItem(vOrderItemID, vAddedDate, 
                    Helpers.CurrentUserName, vProductID, vTitle, vSKU, vUnitPrice, 
                    vQuantity, vUpdatedDate);

                lOrderItem.OrderId = vOrderID;
            }

            return AddOrderItem(lOrderItem);
        }

        public OrderItem AddOrderItem(OrderItem vOrderItem)
        {

            try
            {
                if (vOrderItem.EntityState == EntityState.Detached)
                {
                    Shoppingctx.AddToOrderItems(vOrderItem);
                }
                base.PurgeCacheItems(CacheKey);
                if (Shoppingctx.SaveChanges() > 0)
                {
                    return vOrderItem;
                }
                else
                {
                    return null;

                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public bool DeleteOrderItem(OrderItem vOrderItem)
        {
            return ChangeDeletedState(vOrderItem, false);
        }

        public bool UnDeleteOrderItem(OrderItem vOrderItem)
        {
            return ChangeDeletedState(vOrderItem, true);
        }


        private bool ChangeDeletedState(OrderItem vOrderItem, bool vState)
        {
            vOrderItem.Active = vState;
            vOrderItem.UpdatedDate = DateTime.Now;
            vOrderItem.UpdatedBy = CurrentUserName;

            try
            {
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vOrderItem.OrderItemID.ToString(), ex);

                return false;

            }
        }


        #endregion

    }
}