namespace TheBeerHouse.BLL.Articles
{
    using System;
    using System.Collections.Generic;

    public interface ICategoryRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool AddCategory(Category vCategory);
        /// <summary>
        /// </summary>
        /// <param name="vCategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool DeleteCategory(int vCategoryId);
        /// <summary>
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool DeleteCategory(Category vCategory);
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Category> GetActiveCategories();
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Category> GetCategories();
        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        Category GetCategoryById(int CategoryId);
        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        Category GetCategoryById(int CategoryId, ArticlesEntities vArticlesctx);
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        int GetCategoryCount();
        /// <summary>
        /// </summary>
        /// <param name="vCategory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool UpdateCategory(Category vCategory);
    }
}

