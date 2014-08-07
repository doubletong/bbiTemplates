namespace TheBeerHouse.BLL.Store
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// The Entity Repository Class. Contains methods that utilize the Entity Framework to 
    /// interact with the database.
    /// </summary>
    /// <remarks></remarks>
    public class OrdersRepository : BaseShoppingCartRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vOrder"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Order AddOrder(Order vOrder)
        {
            Order AddOrder;
            try
            {
                if (vOrder.EntityState == EntityState.Detached)
                {
                    this.Shoppingctx.AddToOrders(vOrder);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddOrder = (this.Shoppingctx.SaveChanges() > 0) ? vOrder : null;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                AddOrder = null;
                ProjectData.ClearProjectError();
                return AddOrder;
                ProjectData.ClearProjectError();
            }
            return AddOrder;
        }

        private bool ChangeDeletedState(Order vOrder, bool vState)
        {
            bool ChangeDeletedState;
            vOrder.Active = vState;
            vOrder.UpdatedDate = DateAndTime.Now;
            vOrder.UpdatedBy = BaseRepository.CurrentUserName;
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
                this.ActiveExceptions.Add(Conversions.ToString(vOrder.OrderID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vOrder"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteOrder(Order vOrder)
        {
            return this.ChangeDeletedState(vOrder, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Order GetOrderById(int OrderId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__43 $VB$Closure_ClosureVariable_69_C = new _Closure$__43();
            $VB$Closure_ClosureVariable_69_C.$VB$Local_OrderId = OrderId;
            string key = this.CacheKey + "_" + Conversions.ToString($VB$Closure_ClosureVariable_69_C.$VB$Local_OrderId);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (Order) BaseRepository.Cache[key];
            }
            this.Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            Order lOrder = this.Shoppingctx.Orders.Include("OrderItems").Where<Order>(Expression.Lambda<Func<Order, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Order), "lai"), (MethodInfo) methodof(Order.get_OrderID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_69_C, typeof(_Closure$__43)), fieldof(_Closure$__43.$VB$Local_OrderId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Order>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lOrder);
            }
            return lOrder;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetOrderCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.Orders.Select<Order, Order>(Expression.Lambda<Func<Order, Order>>(VB$t_ref$S0 = Expression.Parameter(typeof(Order), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Order>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Order> GetOrders()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_List";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Order>) BaseRepository.Cache[key];
            }
            this.Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> lOrders = this.Shoppingctx.Orders.Include("OrderItems").Select<Order, Order>(Expression.Lambda<Func<Order, Order>>(VB$t_ref$S0 = Expression.Parameter(typeof(Order), "lOrder"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Order>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lOrders);
            }
            return lOrders;
        }

        public List<Order> GetOrdersByDateRange(DateTime vFromDate, DateTime vToDate)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__42 $VB$Closure_ClosureVariable_53_C = new _Closure$__42();
            $VB$Closure_ClosureVariable_53_C.$VB$Local_vFromDate = vFromDate;
            $VB$Closure_ClosureVariable_53_C.$VB$Local_vToDate = vToDate;
            this.Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            if (!Information.IsNothing($VB$Closure_ClosureVariable_53_C.$VB$Local_vFromDate) | !Information.IsNothing($VB$Closure_ClosureVariable_53_C.$VB$Local_vToDate))
            {
            }
            return this.Shoppingctx.Orders.Include("OrderItems").Where<Order>(Expression.Lambda<Func<Order, bool>>(Expression.And(Expression.GreaterThanOrEqual(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Order), "lOrder"), (MethodInfo) methodof(Order.get_AddedDate)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_53_C, typeof(_Closure$__42)), fieldof(_Closure$__42.$VB$Local_vFromDate)), true, (MethodInfo) methodof(DateTime.op_GreaterThanOrEqual)), Expression.LessThanOrEqual(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Order.get_AddedDate)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_53_C, typeof(_Closure$__42)), fieldof(_Closure$__42.$VB$Local_vToDate)), true, (MethodInfo) methodof(DateTime.op_LessThanOrEqual))), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Order>();
        }

        public List<Order> GetOrdersByOrderStatusId(int vOrderStatusId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__41 $VB$Closure_ClosureVariable_3F_C = new _Closure$__41();
            $VB$Closure_ClosureVariable_3F_C.$VB$Local_vOrderStatusId = vOrderStatusId;
            string key = this.CacheKey + "_ByOrderStatusId_List_" + Conversions.ToString($VB$Closure_ClosureVariable_3F_C.$VB$Local_vOrderStatusId);
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Order>) BaseRepository.Cache[key];
            }
            this.Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> lOrders = this.Shoppingctx.Orders.Include("OrderItems").Where<Order>(Expression.Lambda<Func<Order, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Order), "lOrder"), (MethodInfo) methodof(Order.get_StatusID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_3F_C, typeof(_Closure$__41)), fieldof(_Closure$__41.$VB$Local_vOrderStatusId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Order>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lOrders);
            }
            return lOrders;
        }

        public List<Order> GetOrdersByUser(string vUserName)
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            _Closure$__40 $VB$Closure_ClosureVariable_11_C = new _Closure$__40();
            $VB$Closure_ClosureVariable_11_C.$VB$Local_vUserName = vUserName;
            string key = this.CacheKey + "_" + $VB$Closure_ClosureVariable_11_C.$VB$Local_vUserName + "_List";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Order>) BaseRepository.Cache[key];
            }
            this.Shoppingctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> lOrders = this.Shoppingctx.Orders.Include("OrderItems").Where<Order>(Expression.Lambda<Func<Order, bool>>(Expression.Equal(Expression.Call(null, (MethodInfo) methodof(Microsoft.VisualBasic.CompilerServices.Operators.CompareString), new Expression[] { Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Order), "lOrder"), (MethodInfo) methodof(Order.get_AddedBy)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_11_C, typeof(_Closure$__40)), fieldof(_Closure$__40.$VB$Local_vUserName)), Expression.Constant(false, typeof(bool)) }), Expression.Constant(0, typeof(int))), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Order, DateTime>(Expression.Lambda<Func<Order, DateTime>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Order), "lOrder"), (MethodInfo) methodof(Order.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Order>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lOrders);
            }
            return lOrders;
        }

        public Order InsertOrder(ShoppingCart vshoppingCart, string shippingMethod, decimal shipping, string shippingFirstName, string shippingLastName, string shippingStreet, string shippingPostalCode, string shippingCity, string shippingState, string shippingCountry, string customerEmail, string customerPhone, string customerFax, string transactionID)
        {
            IEnumerator VB$t_ref$L0;
            string userName = Helpers.CurrentUserName;
            Order lOrder = Order.CreateOrder(0, DateTime.Now, userName, 1, shippingMethod, vshoppingCart.Total, shipping, shippingFirstName, shippingLastName, shippingStreet, shippingPostalCode, shippingCity, shippingState, shippingCountry, customerEmail, customerPhone, customerFax, DateAndTime.Now, true);
            lOrder.TransactionID = transactionID;
            try
            {
                VB$t_ref$L0 = vshoppingCart.Items.GetEnumerator();
                while (VB$t_ref$L0.MoveNext())
                {
                    ShoppingCartItem item = (ShoppingCartItem) VB$t_ref$L0.Current;
                    lOrder.OrderItems.Add(OrderItem.CreateOrderItem(0, DateTime.Now, userName, item.ID, item.Title, item.SKU, item.UnitPrice, item.Quantity, DateTime.Now, true));
                }
            }
            finally
            {
                if (VB$t_ref$L0 is IDisposable)
                {
                    (VB$t_ref$L0 as IDisposable).Dispose();
                }
            }
            return this.AddOrder(lOrder);
        }

        public bool UnDeleteOrder(Order vOrder)
        {
            return this.ChangeDeletedState(vOrder, true);
        }

        [CompilerGenerated]
        internal class _Closure$__40
        {
            public string $VB$Local_vUserName;

            [DebuggerNonUserCode]
            public _Closure$__40()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__40(OrdersRepository._Closure$__40 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vUserName = other.$VB$Local_vUserName;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__41
        {
            public int $VB$Local_vOrderStatusId;

            [DebuggerNonUserCode]
            public _Closure$__41()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__41(OrdersRepository._Closure$__41 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vOrderStatusId = other.$VB$Local_vOrderStatusId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__42
        {
            public DateTime $VB$Local_vFromDate;
            public DateTime $VB$Local_vToDate;

            [DebuggerNonUserCode]
            public _Closure$__42()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__42(OrdersRepository._Closure$__42 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vFromDate = other.$VB$Local_vFromDate;
                    this.$VB$Local_vToDate = other.$VB$Local_vToDate;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__43
        {
            public int $VB$Local_OrderId;

            [DebuggerNonUserCode]
            public _Closure$__43()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__43(OrdersRepository._Closure$__43 other)
            {
                if (other != null)
                {
                    this.$VB$Local_OrderId = other.$VB$Local_OrderId;
                }
            }
        }
    }
}

