using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;

namespace BBICMS.Store
{
    public class ProductsRepository : BaseShoppingCartRepository
    {
        #region " BLL/DAL Methods "

        public IEnumerable<Product> GetRSSProducts(int vDepartmentId)
        {
            string key = CacheKey + "_IEnumerable";

            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            IEnumerable<Product> lProducts = default(IEnumerable<Product>);

            if (vDepartmentId == 0)
            {
                lProducts = (from lProduct in Shoppingctx.Products
                             orderby lProduct.UnitPrice descending
                             select lProduct).AsEnumerable();
            }
            else
            {
                lProducts = (from lProduct in Shoppingctx.Products
                             where lProduct.Department.DepartmentID == vDepartmentId
                             orderby lProduct.UnitPrice descending
                             select lProduct).AsEnumerable();
            }

            return lProducts;
        }

        public List<Product> GetProducts()
        {
            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            return (from lProduct in Shoppingctx.Products.Include("Department")
                    orderby lProduct.AddedDate descending
                    select lProduct).ToList();
        }

        public List<Product> GetProducts(int StartRowIndex, int PageSize)
        {
            string key = CacheKey + "_All_Products_" + StartRowIndex.ToString() + "_" + PageSize.ToString();

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = (from lProduct in Shoppingctx.Products.Include("Department").Include("Photos")
                                       orderby lProduct.AddedDate descending
                                       select lProduct).Skip(StartRowIndex).Take(PageSize).ToList();

            if (EnableCaching)
            {
                CacheData(key, lProducts, CacheDuration);
            }

            return lProducts;
        }

        /// <summary>
        /// 产品总数
        /// </summary>
        /// <returns></returns>
        public int GetProductsCount()
        {
            string key = CacheKey + "_AllProductsCount";

            if (EnableCaching && Cache[key] != null)
            {
                return (int)Cache[key];
            }

            int OrderCount = (from lProduct in Shoppingctx.Products
                              select lProduct).Count();

            if (EnableCaching)
            {
                CacheData(key, OrderCount, CacheDuration);
            }


            return OrderCount;
        }



        public List<Product> GetProductsByDepartment(int vDepartmentId)
        {
            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            return (from lProduct in Shoppingctx.Products
                    where lProduct.Department.DepartmentID == vDepartmentId
                    select lProduct).ToList();
        }


        public List<Product> GetProducts(int vDepartmentId, int StartRowIndex, int PageSize)
        {
            if (vDepartmentId <= 0)
                return GetProducts(StartRowIndex, PageSize);

            string key = CacheKey + "_Products_" + vDepartmentId.ToString() + "_" + StartRowIndex.ToString() + "_" + PageSize.ToString();

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = (from lProduct in Shoppingctx.Products.Include("Department").Include("Photos")
                                           .Where(p => p.DepartmentID == vDepartmentId)
                                       orderby lProduct.AddedDate descending
                                       select lProduct).Skip(StartRowIndex).Take(PageSize).ToList();

            if (EnableCaching)
            {
                CacheData(key, lProducts, CacheDuration);
            }


            return lProducts;
        }

        /// <summary>
        /// 按分类获取产品
        /// </summary>
        /// <returns></returns>
        public int GetProductsCount(int vCategoryID)
        {
            if (vCategoryID <= 0)
                return GetProductsCount();

            string key = CacheKey + "_ProductsCount_" + vCategoryID.ToString();

            if (EnableCaching && Cache[key] != null)
            {
                return (int)Cache[key];
            }

            int ProductCount = (from lProduct in Shoppingctx.Products
                                where lProduct.DepartmentID == vCategoryID
                                select lProduct).Count();

            if (EnableCaching)
            {
                CacheData(key, ProductCount, CacheDuration);
            }


            return ProductCount;
        }

        public List<Product> GetActiveProducts(int StartRowIndex, int PageSize)
        {
           
            string key = CacheKey + "_ActiveAllProducts_" + StartRowIndex.ToString() + "_" + PageSize.ToString();

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = (from lProduct in Shoppingctx.Products.Include("Department").Include("Photos")
                                       where lProduct.Active
                                       orderby lProduct.AddedDate descending
                                       select lProduct).Skip(StartRowIndex).Take(PageSize).ToList();

            if (EnableCaching)
            {
                CacheData(key, lProducts, CacheDuration);
            }


            return lProducts;
        }

        /// <summary>
        /// 按分类获取产品
        /// </summary>
        /// <returns></returns>
        public int GetActiveProductsCount()
        {
            
            string key = CacheKey + "_ActiveAllProductsCount";

            if (EnableCaching && Cache[key] != null)
            {
                return (int)Cache[key];
            }

            int ProductCount = (from lProduct in Shoppingctx.Products
                                where lProduct.Active
                                select lProduct).Count();

            if (EnableCaching)
            {
                CacheData(key, ProductCount, CacheDuration);
            }


            return ProductCount;
        }



        public List<Product> GetActiveProducts(int vDepartmentId, int StartRowIndex, int PageSize)
        {
            if (vDepartmentId <= 0)
                return GetProducts(StartRowIndex, PageSize);

            string key = CacheKey + "_ActiveProducts_" + vDepartmentId.ToString() + "_" + StartRowIndex.ToString() + "_" + PageSize.ToString();

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = (from lProduct in Shoppingctx.Products.Include("Department").Include("Photos")
                                       where lProduct.Active && (lProduct.DepartmentID == vDepartmentId)
                                       orderby lProduct.AddedDate descending
                                       select lProduct).Skip(StartRowIndex).Take(PageSize).ToList();

            if (EnableCaching)
            {
                CacheData(key, lProducts, CacheDuration);
            }


            return lProducts;
        }

        /// <summary>
        /// 按分类获取产品
        /// </summary>
        /// <returns></returns>
        public int GetActiveProductsCount(int vDepartmentId)
        {
            if (vDepartmentId <= 0)
                return GetProductsCount();

            string key = CacheKey + "_ActiveProductsCount_" + vDepartmentId.ToString();

            if (EnableCaching && Cache[key] != null)
            {
                return (int)Cache[key];
            }

            int ProductCount = (from lProduct in Shoppingctx.Products
                                where lProduct.Active && (lProduct.DepartmentID == vDepartmentId)
                                select lProduct).Count();

            if (EnableCaching)
            {
                CacheData(key, ProductCount, CacheDuration);
            }


            return ProductCount;
        }


        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>

        public List<Product> GetSearchProducts(string keyword)
        {
            string key = CacheKey + "_Search_" + keyword;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = (from lProduct in Shoppingctx.Products.Include("Department").Include("Photos")
                                       where lProduct.Active && (lProduct.Title.Contains(keyword) || lProduct.Description.Contains(keyword))
                                       orderby lProduct.AddedDate descending
                                       select lProduct).ToList();

            if (EnableCaching)
            {
                CacheData(key, lProducts, CacheDuration);
            }


            return lProducts;
        }

        public List<Product> GetLasterProducts(int intCount)
        {
            string key = CacheKey + "_LasterProducts_" + intCount.ToString();

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Shoppingctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = (from lProduct in Shoppingctx.Products
                                       where lProduct.Active
                                       orderby lProduct.AddedDate descending
                                       select lProduct).Take(intCount).ToList();

            if (EnableCaching)
            {
                CacheData(key, lProducts, CacheDuration);
            }


            return lProducts;
        }



        public Product GetProductById(int ProductId)
        {
            return (from lProduct in Shoppingctx.Products.Include("Department").Include("Photos")
                    where lProduct.ProductID == ProductId
                    select lProduct).FirstOrDefault();
        }

        public Product GetRandomProduct()
        {
            List<Product> lProductList = GetProducts();
            int lRandProdIndex = base.GetRandItem(0, lProductList.Count - 1);

            return lProductList[lRandProdIndex];
        }

        public int GetProductCount()
        {
            return (from lProduct in Shoppingctx.Products
                    select lProduct).Count();
        }

        public Product AddProduct(int vProductID, DateTime vAddedDate, int vDepartmentID, string vTitle,
                                  string vDescription, string vSKU, decimal vUnitPrice, int vDiscountPercentage,
                                  int vUnitsInStock, string vSmallImageUrl,
                                  string vFullImageUrl, int vVotes, int vTotalRating, DateTime vUpdatedDate)
        {
            Product lProduct;

            if (vProductID > 0)
            {
                lProduct = GetProductById(vProductID);

                lProduct.ProductID = vProductID;
                lProduct.AddedDate = vAddedDate;
                lProduct.DepartmentID = vDepartmentID;
                lProduct.Title = vTitle;
                lProduct.Description = vDescription;
                lProduct.SKU = vSKU;
                lProduct.UnitPrice = vUnitPrice;
                lProduct.DiscountPercentage = vDiscountPercentage;
                lProduct.UnitsInStock = vUnitsInStock;
                lProduct.SmallImageUrl = vSmallImageUrl;
                lProduct.FullImageUrl = vFullImageUrl;
                lProduct.Votes = vVotes;
                lProduct.TotalRating = vTotalRating;
                lProduct.UpdatedDate = vUpdatedDate;

                lProduct.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lProduct = Product.CreateProduct(vProductID, vAddedDate, Helpers.CurrentUserName,vDepartmentID, vTitle, vDescription,
                                                 vSKU, vUnitPrice, vDiscountPercentage, vUnitsInStock, vVotes,
                                                 vTotalRating, vUpdatedDate, true);

                // lProduct.DepartmentID = vDepartmentID;
                lProduct.SmallImageUrl = vSmallImageUrl;

                lProduct.FullImageUrl = vFullImageUrl;
            }

            return AddProduct(lProduct);
        }

        public Product AddProduct(Product vProduct)
        {
            try
            {
                if (vProduct.EntityState == EntityState.Detached)
                {
                    Shoppingctx.AddToProducts(vProduct);
                }
                base.PurgeCacheItems(CacheKey);

                return Shoppingctx.SaveChanges() > 0 ? vProduct : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteProduct(Product vProduct)
        {
            return ChangeDeletedState(vProduct, false);
        }

        public bool DeleteProduct(int vProductId)
        {
            return DeleteProduct(GetProductById(vProductId));
        }

        public bool UnDeleteProduct(Product vProduct)
        {
            return ChangeDeletedState(vProduct, true);
        }


        private bool ChangeDeletedState(Product vProduct, bool vState)
        {
            vProduct.Active = vState;
            vProduct.UpdatedDate = DateTime.Now;
            vProduct.UpdatedBy = CurrentUserName;

            try
            {
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vProduct.ProductID.ToString(), ex);

                return false;
            }
        }

        public void RateProduct(int vProductId, int vRating)
        {
            Product lProduct = GetProductById(vProductId);

            if ((lProduct != null))
            {
                lProduct.Votes += 1;
                lProduct.TotalRating += vRating;

                AddProduct(lProduct);
            }
        }


        public bool RemoveProduct(int ProductID)
        {
            return RemoveProduct(this.GetProductById(ProductID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vLink"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool RemoveProduct(Product vProduct)
        {
            if (vProduct.Photos.Count > 0)
                return false;
            try
            {
                Shoppingctx.DeleteObject(vProduct);
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);

                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vProduct.ProductID.ToString(), ex);
                return false;

            }
        }


        #endregion
    }
}