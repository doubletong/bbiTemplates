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
    public class ShippingMethodsRepository : BaseShoppingCartRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vShippingMethod"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddShippingMethod(ShippingMethod vShippingMethod)
        {
            bool AddShippingMethod;
            try
            {
                if (vShippingMethod.EntityState == EntityState.Detached)
                {
                    this.Shoppingctx.AddToShippingMethods(vShippingMethod);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddShippingMethod = this.Shoppingctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                AddShippingMethod = false;
                ProjectData.ClearProjectError();
                return AddShippingMethod;
                ProjectData.ClearProjectError();
            }
            return AddShippingMethod;
        }

        private bool ChangeDeletedState(ShippingMethod vShippingMethod, bool vState)
        {
            bool ChangeDeletedState;
            vShippingMethod.Active = vState;
            vShippingMethod.UpdatedDate = DateAndTime.Now;
            vShippingMethod.UpdatedBy = BaseRepository.CurrentUserName;
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
                this.ActiveExceptions.Add(Conversions.ToString(vShippingMethod.ShippingMethodID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vShippingMethod"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteShippingMethod(ShippingMethod vShippingMethod)
        {
            return this.ChangeDeletedState(vShippingMethod, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="ShippingMethodId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public ShippingMethod GetShippingMethodById(int ShippingMethodId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__49 $VB$Closure_ClosureVariable_1F_C = new _Closure$__49();
            $VB$Closure_ClosureVariable_1F_C.$VB$Local_ShippingMethodId = ShippingMethodId;
            return this.Shoppingctx.ShippingMethods.Where<ShippingMethod>(Expression.Lambda<Func<ShippingMethod, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(ShippingMethod), "lai"), (MethodInfo) methodof(ShippingMethod.get_ShippingMethodID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_1F_C, typeof(_Closure$__49)), fieldof(_Closure$__49.$VB$Local_ShippingMethodId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<ShippingMethod>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetShippingMethodCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.ShippingMethods.Select<ShippingMethod, ShippingMethod>(Expression.Lambda<Func<ShippingMethod, ShippingMethod>>(VB$t_ref$S0 = Expression.Parameter(typeof(ShippingMethod), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<ShippingMethod>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<ShippingMethod> GetShippingMethods()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.ShippingMethods.Select<ShippingMethod, ShippingMethod>(Expression.Lambda<Func<ShippingMethod, ShippingMethod>>(VB$t_ref$S0 = Expression.Parameter(typeof(ShippingMethod), "lShippingMethod"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<ShippingMethod>();
        }

        public bool UnDeleteShippingMethod(ShippingMethod vShippingMethod)
        {
            return this.ChangeDeletedState(vShippingMethod, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vShippingMethod"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateShippingMethod(ShippingMethod vShippingMethod)
        {
            return this.AddShippingMethod(vShippingMethod);
        }

        [CompilerGenerated]
        internal class _Closure$__49
        {
            public int $VB$Local_ShippingMethodId;

            [DebuggerNonUserCode]
            public _Closure$__49()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__49(ShippingMethodsRepository._Closure$__49 other)
            {
                if (other != null)
                {
                    this.$VB$Local_ShippingMethodId = other.$VB$Local_ShippingMethodId;
                }
            }
        }
    }
}

