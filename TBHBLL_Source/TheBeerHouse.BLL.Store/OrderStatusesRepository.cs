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
    public class OrderStatusesRepository : BaseShoppingCartRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vOrderStatus"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddOrderStatus(OrderStatus vOrderStatus)
        {
            bool AddOrderStatus;
            try
            {
                if (vOrderStatus.EntityState == EntityState.Detached)
                {
                    this.Shoppingctx.AddToOrderStatuses(vOrderStatus);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddOrderStatus = this.Shoppingctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                AddOrderStatus = false;
                ProjectData.ClearProjectError();
                return AddOrderStatus;
                ProjectData.ClearProjectError();
            }
            return AddOrderStatus;
        }

        private bool ChangeDeletedState(OrderStatus vOrderStatus, bool vState)
        {
            bool ChangeDeletedState;
            vOrderStatus.Active = vState;
            vOrderStatus.UpdatedDate = DateAndTime.Now;
            vOrderStatus.UpdatedBy = BaseRepository.CurrentUserName;
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
                this.ActiveExceptions.Add(Conversions.ToString(vOrderStatus.OrderStatusID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vOrderStatus"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteOrderStatus(OrderStatus vOrderStatus)
        {
            return this.ChangeDeletedState(vOrderStatus, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="OrderStatusId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public OrderStatus GetOrderStatusById(int OrderStatusId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__44 $VB$Closure_ClosureVariable_1F_C = new _Closure$__44();
            $VB$Closure_ClosureVariable_1F_C.$VB$Local_OrderStatusId = OrderStatusId;
            return this.Shoppingctx.OrderStatuses.Where<OrderStatus>(Expression.Lambda<Func<OrderStatus, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(OrderStatus), "lai"), (MethodInfo) methodof(OrderStatus.get_OrderStatusID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_1F_C, typeof(_Closure$__44)), fieldof(_Closure$__44.$VB$Local_OrderStatusId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<OrderStatus>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetOrderStatusCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.OrderStatuses.Select<OrderStatus, OrderStatus>(Expression.Lambda<Func<OrderStatus, OrderStatus>>(VB$t_ref$S0 = Expression.Parameter(typeof(OrderStatus), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<OrderStatus>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<OrderStatus> GetOrderStatuses()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.OrderStatuses.Select<OrderStatus, OrderStatus>(Expression.Lambda<Func<OrderStatus, OrderStatus>>(VB$t_ref$S0 = Expression.Parameter(typeof(OrderStatus), "lOrderStatus"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<OrderStatus>();
        }

        public bool UnDeleteOrderStatus(OrderStatus vOrderStatus)
        {
            return this.ChangeDeletedState(vOrderStatus, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vOrderStatus"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateOrderStatus(OrderStatus vOrderStatus)
        {
            return this.AddOrderStatus(vOrderStatus);
        }

        [CompilerGenerated]
        internal class _Closure$__44
        {
            public int $VB$Local_OrderStatusId;

            [DebuggerNonUserCode]
            public _Closure$__44()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__44(OrderStatusesRepository._Closure$__44 other)
            {
                if (other != null)
                {
                    this.$VB$Local_OrderStatusId = other.$VB$Local_OrderStatusId;
                }
            }
        }
    }
}

