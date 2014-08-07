namespace TheBeerHouse.BLL.Store
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects;
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
    public class ProductsRepository : BaseShoppingCartRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vProduct"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddProduct(Product vProduct)
        {
            bool AddProduct;
            try
            {
                if (vProduct.EntityState == EntityState.Detached)
                {
                    this.Shoppingctx.AddToProducts(vProduct);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddProduct = this.Shoppingctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                AddProduct = false;
                ProjectData.ClearProjectError();
                return AddProduct;
                ProjectData.ClearProjectError();
            }
            return AddProduct;
        }

        private bool ChangeDeletedState(Product vProduct, bool vState)
        {
            bool ChangeDeletedState;
            vProduct.Active = vState;
            vProduct.UpdatedDate = DateAndTime.Now;
            vProduct.UpdatedBy = BaseRepository.CurrentUserName;
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
                this.ActiveExceptions.Add(Conversions.ToString(vProduct.ProductID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        public bool DeleteProduct(int vProductId)
        {
            return this.DeleteProduct(this.GetProductById(vProductId));
        }

        /// <summary>
        /// </summary>
        /// <param name="vProduct"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteProduct(Product vProduct)
        {
            return this.ChangeDeletedState(vProduct, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Product GetProductById(int ProductId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__47 $VB$Closure_ClosureVariable_41_C = new _Closure$__47();
            $VB$Closure_ClosureVariable_41_C.$VB$Local_ProductId = ProductId;
            return this.Shoppingctx.Products.Where<Product>(Expression.Lambda<Func<Product, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Product), "lai"), (MethodInfo) methodof(Product.get_ProductID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_41_C, typeof(_Closure$__47)), fieldof(_Closure$__47.$VB$Local_ProductId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Product>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetProductCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.Products.Select<Product, Product>(Expression.Lambda<Func<Product, Product>>(VB$t_ref$S0 = Expression.Parameter(typeof(Product), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Product>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Product> GetProducts()
        {
            ParameterExpression VB$t_ref$S0;
            this.Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            return this.Shoppingctx.Products.Include("Department").Select<Product, Product>(Expression.Lambda<Func<Product, Product>>(VB$t_ref$S0 = Expression.Parameter(typeof(Product), "lProduct"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Product>();
        }

        public List<Product> GetProductsByDepartment(int vDepartmentId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__46 $VB$Closure_ClosureVariable_33_C = new _Closure$__46();
            $VB$Closure_ClosureVariable_33_C.$VB$Local_vDepartmentId = vDepartmentId;
            this.Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            return this.Shoppingctx.Products.Where<Product>(Expression.Lambda<Func<Product, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Product), "lProduct"), (MethodInfo) methodof(Product.get_Department)), (MethodInfo) methodof(Department.get_DepartmentID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_33_C, typeof(_Closure$__46)), fieldof(_Closure$__46.$VB$Local_vDepartmentId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Product>();
        }

        public Product GetRandomProduct()
        {
            List<Product> lProductList = this.GetProducts();
            int lRandProdIndex = base.GetRandItem(0, lProductList.Count - 1);
            return lProductList[lRandProdIndex];
        }

        public IEnumerable<Product> GetRSSProducts(int vDepartmentId)
        {
            ParameterExpression VB$t_ref$S1;
            ParameterExpression VB$t_ref$S2;
            _Closure$__45 $VB$Closure_ClosureVariable_10_C = new _Closure$__45();
            $VB$Closure_ClosureVariable_10_C.$VB$Local_vDepartmentId = vDepartmentId;
            string key = this.CacheKey + "_IEnumerable";
            this.Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            if ($VB$Closure_ClosureVariable_10_C.$VB$Local_vDepartmentId == 0)
            {
                ParameterExpression VB$t_ref$S0;
                return this.Shoppingctx.Products.OrderByDescending<Product, decimal>(Expression.Lambda<Func<Product, decimal>>(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Product), "lProduct"), (MethodInfo) methodof(Product.get_UnitPrice)), new ParameterExpression[] { VB$t_ref$S0 })).AsEnumerable<Product>();
            }
            return this.Shoppingctx.Products.Where<Product>(Expression.Lambda<Func<Product, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Product), "lProduct"), (MethodInfo) methodof(Product.get_Department)), (MethodInfo) methodof(Department.get_DepartmentID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_10_C, typeof(_Closure$__45)), fieldof(_Closure$__45.$VB$Local_vDepartmentId)), true, null), new ParameterExpression[] { VB$t_ref$S1 })).OrderByDescending<Product, decimal>(Expression.Lambda<Func<Product, decimal>>(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(Product), "lProduct"), (MethodInfo) methodof(Product.get_UnitPrice)), new ParameterExpression[] { VB$t_ref$S2 })).AsEnumerable<Product>();
        }

        public void RateProduct(int vProductId, int vRating)
        {
            Product lProduct = this.GetProductById(vProductId);
            if (!Information.IsNothing(lProduct))
            {
                Product VB$t_ref$S0 = lProduct;
                VB$t_ref$S0.Votes++;
                VB$t_ref$S0 = lProduct;
                VB$t_ref$S0.TotalRating += vRating;
                this.AddProduct(lProduct);
            }
        }

        public bool UnDeleteProduct(Product vProduct)
        {
            return this.ChangeDeletedState(vProduct, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vProduct"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateProduct(Product vProduct)
        {
            return this.AddProduct(vProduct);
        }

        [CompilerGenerated]
        internal class _Closure$__45
        {
            public int $VB$Local_vDepartmentId;

            [DebuggerNonUserCode]
            public _Closure$__45()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__45(ProductsRepository._Closure$__45 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vDepartmentId = other.$VB$Local_vDepartmentId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__46
        {
            public int $VB$Local_vDepartmentId;

            [DebuggerNonUserCode]
            public _Closure$__46()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__46(ProductsRepository._Closure$__46 other)
            {
                if (other != null)
                {
                    this.$VB$Local_vDepartmentId = other.$VB$Local_vDepartmentId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__47
        {
            public int $VB$Local_ProductId;

            [DebuggerNonUserCode]
            public _Closure$__47()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__47(ProductsRepository._Closure$__47 other)
            {
                if (other != null)
                {
                    this.$VB$Local_ProductId = other.$VB$Local_ProductId;
                }
            }
        }
    }
}

