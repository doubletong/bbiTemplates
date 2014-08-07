using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BBICMS.Store
{
    public class OrderStatusesRepository : BaseShoppingCartRepository
    {
        #region " BLL/DAL Methods "

        public List<OrderStatus> GetOrderStatuses()
        {
            return (from lOrderStatus in Shoppingctx.OrderStatuses
                    select lOrderStatus).ToList();
        }

        public OrderStatus GetOrderStatusById(int OrderStatusId)
        {
            return (from lOrderStatus in Shoppingctx.OrderStatuses
                    where lOrderStatus.OrderStatusID == OrderStatusId
                    select lOrderStatus).FirstOrDefault();
        }

        public int GetOrderStatusCount()
        {
            return (from lOrderStatus in Shoppingctx.OrderStatuses
                    select lOrderStatus).Count();
        }

        public OrderStatus AddOrderStatus(int vOrderStatusID, DateTime vAddedDate, string vTitle,
                                          DateTime vUpdatedDate)
        {
            OrderStatus lOrderStatus = default(OrderStatus);

            if (vOrderStatusID > 0)
            {
                lOrderStatus = GetOrderStatusById(vOrderStatusID);

                lOrderStatus.OrderStatusID = vOrderStatusID;
                lOrderStatus.AddedDate = vAddedDate;
                lOrderStatus.Title = vTitle;
                lOrderStatus.UpdatedDate = vUpdatedDate;

                lOrderStatus.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lOrderStatus = OrderStatus.CreateOrderStatus(vOrderStatusID, vAddedDate, Helpers.CurrentUserName, vTitle,
                                                             vUpdatedDate, true);
            }


            return AddOrderStatus(lOrderStatus);
        }

        public OrderStatus AddOrderStatus(OrderStatus vOrderStatus)
        {
            try
            {
                if (vOrderStatus.EntityState == EntityState.Detached)
                {
                    Shoppingctx.AddToOrderStatuses(vOrderStatus);
                }
                base.PurgeCacheItems(CacheKey);

                return Shoppingctx.SaveChanges() > 0 ? vOrderStatus : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteOrderStatus(OrderStatus vOrderStatus)
        {
            return ChangeDeletedState(vOrderStatus, false);
        }

        public bool UnDeleteOrderStatus(OrderStatus vOrderStatus)
        {
            return ChangeDeletedState(vOrderStatus, true);
        }


        private bool ChangeDeletedState(OrderStatus vOrderStatus, bool vState)
        {
            vOrderStatus.Active = vState;
            vOrderStatus.UpdatedDate = DateTime.Now;
            vOrderStatus.UpdatedBy = CurrentUserName;

            try
            {
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vOrderStatus.OrderStatusID.ToString(), ex);

                return false;
            }
        }

        #endregion
    }
}