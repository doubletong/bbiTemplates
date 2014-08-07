namespace TheBeerHouse.BLL.Articles
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

    public class CategoryRepository : BaseArticleRepository
    {
        public CategoryRepository()
        {
            this.CacheKey = "Categories";
        }

        /// <summary>
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddCategory(Category vCategory)
        {
            bool AddCategory;
            try
            {
                if (vCategory.EntityState == EntityState.Detached)
                {
                    this.Articlesctx.AddToCategories(vCategory);
                }
                base.PurgeCacheItems(this.CacheKey);
                return (this.Articlesctx.SaveChanges() > 0);
                AddCategory = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vCategory.CategoryID), ex);
                AddCategory = false;
                ProjectData.ClearProjectError();
                return AddCategory;
                ProjectData.ClearProjectError();
            }
            return AddCategory;
        }

        /// <summary>
        /// </summary>
        /// <param name="vCategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteCategory(int vCategoryId)
        {
            return this.DeleteCategory(this.GetCategoryById(vCategoryId));
        }

        /// <summary>
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteCategory(Category vCategory)
        {
            bool DeleteCategory;
            try
            {
                vCategory.Active = false;
                this.UpdateCategory(vCategory);
                base.PurgeCacheItems(this.CacheKey);
                DeleteCategory = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                DeleteCategory = false;
                ProjectData.ClearProjectError();
                return DeleteCategory;
                ProjectData.ClearProjectError();
            }
            return DeleteCategory;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Category> GetActiveCategories()
        {
            ParameterExpression VB$t_ref$S0;
            string key = "Active" + this.CacheKey;
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key])))
            {
                return (List<Category>) BaseRepository.Cache[key];
            }
            this.Articlesctx.Categories.MergeOption = MergeOption.NoTracking;
            List<Category> lCategories = this.Articlesctx.Categories.Where<Category>(Expression.Lambda<Func<Category, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Category), "lCategory"), (MethodInfo) methodof(Category.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Category>();
            BaseRepository.CacheData(key, lCategories);
            return lCategories;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Category> GetCategories()
        {
            ParameterExpression VB$t_ref$S0;
            string key = "Categories";
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key])))
            {
                return (List<Category>) BaseRepository.Cache[key];
            }
            this.Articlesctx.Categories.MergeOption = MergeOption.NoTracking;
            List<Category> lCategories = this.Articlesctx.Categories.Select<Category, Category>(Expression.Lambda<Func<Category, Category>>(VB$t_ref$S0 = Expression.Parameter(typeof(Category), "lCategory"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Category>();
            BaseRepository.CacheData(key, lCategories);
            return lCategories;
        }

        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Category GetCategoryById(int CategoryId)
        {
            return this.GetCategoryById(CategoryId, this.Articlesctx);
        }

        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Category GetCategoryById(int CategoryId, ArticlesEntities vArticlesctx)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__6 $VB$Closure_ClosureVariable_51_C = new _Closure$__6();
            $VB$Closure_ClosureVariable_51_C.$VB$Local_CategoryId = CategoryId;
            vArticlesctx.Categories.MergeOption = MergeOption.AppendOnly;
            return vArticlesctx.Categories.Where<Category>(Expression.Lambda<Func<Category, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Category), "lai"), (MethodInfo) methodof(Category.get_CategoryID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_51_C, typeof(_Closure$__6)), fieldof(_Closure$__6.$VB$Local_CategoryId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Category>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetCategoryCount()
        {
            return this.Articlesctx.Categories.Count<Category>();
        }

        /// <summary>
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateCategory(Category vCategory)
        {
            return this.AddCategory(vCategory);
        }

        [CompilerGenerated]
        internal class _Closure$__6
        {
            public int $VB$Local_CategoryId;

            [DebuggerNonUserCode]
            public _Closure$__6()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__6(CategoryRepository._Closure$__6 other)
            {
                if (other != null)
                {
                    this.$VB$Local_CategoryId = other.$VB$Local_CategoryId;
                }
            }
        }
    }
}

