namespace TheBeerHouse.BLL.Store
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using TheBeerHouse.BLL;

    /// <summary>
    /// The Entity Repository Class. Contains methods that utilize the Entity Framework to 
    /// interact with the database.
    /// </summary>
    /// <remarks></remarks>
    public class OrderItemsRepository : BaseShoppingCartRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vOrderItem"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddOrderItem(OrderItem vOrderItem)
        {
            bool AddOrderItem;
            try
            {
                if (vOrderItem.EntityState == EntityState.Detached)
                {
                    this.Shoppingctx.AddToOrderItems(vOrderItem);
                }
                base.PurgeCacheItems(this.CacheKey);
                if (this.Shoppingctx.SaveChanges() > 0)
                {
                    return true;
                }
                AddOrderItem = false;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                AddOrderItem = false;
                ProjectData.ClearProjectError();
                return AddOrderItem;
                ProjectData.ClearProjectError();
            }
            return AddOrderItem;
        }

        private bool ChangeDeletedState(OrderItem vOrderItem, bool vState)
        {
            bool ChangeDeletedState;
            vOrderItem.Active = vState;
            vOrderItem.UpdatedDate = DateAndTime.Now;
            vOrderItem.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Shoppingctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vOrderItem.OrderItemID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vOrderItem"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteOrderItem(OrderItem vOrderItem)
        {
            return this.ChangeDeletedState(vOrderItem, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="OrderItemId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public OrderItem GetOrderItemById(int OrderItemId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__39 $VB$Closure_ClosureVariable_1E_C = new _Closure$__39();
            $VB$Closure_ClosureVariable_1E_C.$VB$Local_OrderItemId = OrderItemId;
            return this.Shoppingctx.OrderItems.Where<OrderItem>(Expression.Lambda<Func<OrderItem, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(OrderItem), "lai"), (MethodInfo) methodof(OrderItem.get_OrderItemID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_1E_C, typeof(_Closure$__39)), fieldof(_Closure$__39.$VB$Local_OrderItemId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<OrderItem>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetOrderItemCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.OrderItems.Select<OrderItem, OrderItem>(Expression.Lambda<Func<OrderItem, OrderItem>>(VB$t_ref$S0 = Expression.Parameter(typeof(OrderItem), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<OrderItem>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<OrderItem> GetOrderItems()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.OrderItems.Select<OrderItem, OrderItem>(Expression.Lambda<Func<OrderItem, OrderItem>>(VB$t_ref$S0 = Expression.Parameter(typeof(OrderItem), "lOrderItem"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<OrderItem>();
        }

        public bool UnDeleteOrderItem(OrderItem vOrderItem)
        {
            return this.ChangeDeletedState(vOrderItem, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vOrderItem"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateOrderItem(OrderItem vOrderItem)
        {
            return this.AddOrderItem(vOrderItem);
        }

        [CompilerGenerated]
        internal class _Closure$__39
        {
            public int $VB$Local_OrderItemId;

            [DebuggerNonUserCode]
            public _Closure$__39()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__39(OrderItemsRepository._Closure$__39 other)
            {
                if (other != null)
                {
                    this.$VB$Local_OrderItemId = other.$VB$Local_OrderItemId;
                }
            }
        }
    }
}

