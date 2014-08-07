
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using BBICMS.Articles;

namespace BBICMS.BLL.Articles
{

    public class CategoryRepository : BaseArticleRepository
    {

        public List<Category> GetActiveCategories()
        {
            string key = CacheKey + "_Active";

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Category>)Cache[key];
            }

            Articlesctx.Categories.MergeOption = MergeOption.NoTracking;
            List<Category> lCategories = (from lCategory in Articlesctx.Categories //.Include("Category")
                                       where lCategory.Active
                                       //orderby lCategory.ReleaseDate descending
                                       select lCategory).ToList();

            if (EnableCaching)
            {
                CacheData(key, lCategories, CacheDuration);
            }

            return lCategories;
        }

        public List<Category> GetCategories()
        {
            string key = CacheKey;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Category>)Cache[key];
            }

            Articlesctx.Categories.MergeOption = MergeOption.NoTracking;
            List<Category> lCategories = (from lCategory in Articlesctx.Categories.Include("Category")
//                                       orderby lCategory.ReleaseDate descending
                                       select lCategory).ToList();

            if (EnableCaching)
            {
                CacheData(key, lCategories, CacheDuration);
            }


            return lCategories;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Category GetCategoryById(int CategoryId)
        {
            return (from lCategory in Articlesctx.Categories
                    where lCategory.CategoryID == CategoryId
                        select lCategory).FirstOrDefault();

//            return GetCategoryById(CategoryId, false, false);
        }

        public Category AddCategory(int CategoryID
                ,DateTime AddedDate
                ,String AddedBy
                ,String Title
                ,int Importance
                ,String Description
                ,String ImageUrl
                ,DateTime UpdatedDate
                ,String UpdatedBy
                ,bool Active)
        {

            Category Category;

            if (CategoryID > 0)
            {

                Category = GetCategoryById(CategoryID);

                Category.CategoryID = CategoryID;
                Category.AddedDate = AddedDate;
                Category.AddedBy = AddedBy;
                Category.Title = Title;
                Category.Importance = Importance;
                Category.Description = Description;
                Category.ImageUrl = ImageUrl;
                Category.UpdatedDate = UpdatedDate;
                Category.UpdatedBy = UpdatedBy;
                Category.Active = Active;

            }
            else
            {
                Category = Category.CreateCategory(CategoryID
                ,AddedDate, AddedBy,Title,Importance
                ,UpdatedDate, Active);
            }

            return AddCategory(Category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Category AddCategory(Category vCategory)
        {

            try
            {
                if (vCategory.EntityState == EntityState.Detached)
                {
                    Articlesctx.AddToCategories(vCategory);
                }
                base.PurgeCacheItems(CacheKey);

                return Articlesctx.SaveChanges() > 0 ? vCategory : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vCategory.CategoryID, ex);
                return null;

            }
        }

        #region " Delete Operations "

        public bool DeleteCategory(int vCategoryId)
        {
            return ChangeDeletedState(this.GetCategoryById(vCategoryId), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteCategory(Category vCategory)
        {
            return ChangeDeletedState(vCategory, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeleteCategory(Category vCategory)
        {
            return ChangeDeletedState(vCategory, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vCategory"></param>
        /// <param name="vState"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ChangeDeletedState(Category vCategory, bool vState)
        {
            vCategory.Active = vState;
            vCategory.UpdatedDate = DateTime.Now;
            vCategory.UpdatedBy = CurrentUserName;

            try
            {
                Articlesctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vCategory.CategoryID.ToString(), ex);
                return false;

            }
        }

        #endregion

    }
}
