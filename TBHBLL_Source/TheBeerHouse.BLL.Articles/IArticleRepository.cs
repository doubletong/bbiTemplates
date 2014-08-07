namespace TheBeerHouse.BLL.Articles
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;

    public interface IArticleRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool AddArticle(Article vArticle);
        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool ApproveArticle(int ArticleId);
        /// <summary>
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool DeleteArticle(Article vArticle);
        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        string GetArticleBodyById(int ArticleId);
        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        Article GetArticleById(int ArticleId);
        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="bIncludeCategories"></param>
        /// <param name="bIncludeComments"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        Article GetArticleById(int ArticleId, bool bIncludeCategories, bool bIncludeComments);
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        int GetArticleCount();
        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        int GetArticleCountByCategory(int CategoryId);
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Article> GetArticles();
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Article> GetArticles(bool PublishedOnly, int PageIndex, int PageSize);
        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Article> GetArticlesByCategory(int CategoryId);
        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="PublishedOnly"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Article> GetArticlesByCategory(int CategoryId, bool PublishedOnly, int PageIndex, int PageSize);
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Article> GetHomePageArticles();
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Article> GetPublishedArticles();
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Article> GetPublishedArticles(int categoryId);
        /// <summary>
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Article> GetPublishedArticles(int pageIndex, int pageSize);
        ObjectQuery<Article> GetSpecialArticles();
        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool IncrementArticleViewCount(int ArticleId);
        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool RateArticle(int ArticleId, int rating);
        /// <summary>
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool UnDeleteArticle(Article vArticle);
        /// <summary>
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool UpdateArticle(Article vArticle);
    }
}

