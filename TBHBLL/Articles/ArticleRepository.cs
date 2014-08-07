using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using BBICMS.Articles;

namespace BBICMS.BLL.Articles
{

    public class ArticleRepository : BaseArticleRepository
    {

        public ObjectQuery<Article> GetSpecialArticles()
        {
            string queryString = "SELECT VALUE Article FROM BBICMS.Articles AS Article";

            return Articlesctx.CreateQuery<Article>(queryString);
        }

        public List<Article> GetActiveArticles()
        {
            string key = CacheKey + "_Active";

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Article>)Cache[key];
            }

            Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            List<Article> lArticles = (from lArticle in Articlesctx.Articles.Include("Category")
                                       where lArticle.Active
                                       orderby lArticle.ReleaseDate descending
                                       select lArticle).ToList();

            if (EnableCaching)
            {
                CacheData(key, lArticles, CacheDuration);
            }


            return lArticles;
        }

        public List<Article> GetArticles()
        {
            string key = CacheKey;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Article>)Cache[key];
            }

            Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            List<Article> lArticles = (from lArticle in Articlesctx.Articles.Include("Category")
                                       orderby lArticle.ReleaseDate descending
                                       select lArticle).ToList();

            if (EnableCaching)
            {
                CacheData(key, lArticles, CacheDuration);
            }


            return lArticles;
        }

        public List<Article> GetArticles(bool PublishedOnly)
        {
            string key = String.Format(CacheKey + "_PublishedOnly_{0}", PublishedOnly.ToString());

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Article>)Cache[key];
            }

            Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            List<Article> lArticles = null;

            if (PublishedOnly)
            {
                lArticles = (from lArticle in Articlesctx.Articles.Include("Category")
                             where lArticle.Published
                             orderby lArticle.ReleaseDate descending
                             select lArticle).ToList();
            }
            else
            {
                lArticles = (from lArticle in Articlesctx.Articles.Include("Category")
                             orderby lArticle.ReleaseDate descending
                             select lArticle).ToList();
            }

            if (EnableCaching)
            {
                CacheData(key, lArticles, CacheDuration);
            }

            return lArticles;
        }

        public List<Article> GetArticles(bool PublishedOnly, int PageIndex, int PageSize)
        {
            string key = String.Format(
                CacheKey + "_{0}_{1}_{2}", PageIndex.ToString(), PageSize.ToString(), PublishedOnly.ToString());

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Article>)Cache[key];
            }

            Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            List<Article> lArticles = null;

            if (PublishedOnly)
            {
                lArticles = (from lArticle in Articlesctx.Articles.Include("Category")
                             where lArticle.Published
                             orderby lArticle.ReleaseDate descending
                             select lArticle).Skip(PageSize * PageIndex).Take(PageSize).ToList();
            }
            else
            {
                lArticles = (from lArticle in Articlesctx.Articles.Include("Category")
                             orderby lArticle.ReleaseDate descending
                             select lArticle).Skip(PageSize * PageIndex).Take(PageSize).ToList();
            }

            if (EnableCaching)
            {
                CacheData(key, lArticles, CacheDuration);
            }

            return lArticles;
        }

        public IEnumerable<Article> GetRSSArticles()
        {

            string key = CacheKey + "_IEnumerable";

            if (EnableCaching && (Cache[key] != null))
            {
                return (IEnumerable<Article>)Cache[key];
            }

            Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            IEnumerable<Article> lArticles = (from lArticle in Articlesctx.Articles
                                              where lArticle.Active
                                              orderby lArticle.ReleaseDate
                                              select lArticle).AsEnumerable();

            if (EnableCaching)
            {
                CacheData(key, lArticles, CacheDuration);
            }

            return lArticles;
        }

        public List<Article> GetHomePageArticles()
        {
            string key = CacheKey + "_HomePage";

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Article>)Cache[key];
            }

            Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            List<Article> lArticles = (Articlesctx.Articles.Include("Category")
                .Where(p => p.Active).Where(p => p.Approved).Where(p => p.Listed)
                .Where(p => p.ReleaseDate < DateTime.Now).OrderByDescending(lArticle => lArticle.AddedDate)).ToList();

            if (EnableCaching)
            {
                CacheData(key, lArticles, CacheDuration);
            }


            return lArticles;
        }

        public List<Article> GetPublishedArticles(int categoryId)
        {
            string key = String.Format(CacheKey + "_Published_CategoryId_{0}", categoryId.ToString());

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Article>)Cache[key];
            }

            Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            List<Article> lArticles = (from lArticle in Articlesctx.Articles.Include("Category")
                                      .Where(p => p.Active).Where(p => p.Approved).Where(p => p.Listed)
                                      .Where(p => p.CategoryId == categoryId)
                                       select lArticle).ToList();

            if (EnableCaching)
            {
                CacheData(key, lArticles, CacheDuration);
            }

            return lArticles;
        }

        public List<Article> GetPublishedArticles(int pageIndex, int pageSize)
        {
            Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            return (from lArticle in Articlesctx.Articles.Include("Category")
                                      .Where(p => p.Active).Where(p => p.Approved).Where(p => p.Listed)
                                      .Where(p => p.ReleaseDate <= DateTime.Now)
                    select lArticle).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetArticlesByCategory(int CategoryId)
        {
            return GetArticlesByCategory(CategoryId, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="PublishedOnly"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetArticlesByCategory(int CategoryId, bool PublishedOnly)
        {

            List<Article> lArticles = default(List<Article>);

            string key = CacheKey + "ByCategory_" + CategoryId + "_" + PublishedOnly;

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Article>)Cache[key];
            }

            //TODO: Change this to an IQueryable expression tree
            if (PublishedOnly)
            {

                lArticles = (Articlesctx.Articles
                    .Where(p => p.Active).Where(p => p.Approved).Where(p => p.ExpireDate > DateTime.Now)
                    .Where(p => p.ReleaseDate <= DateTime.Now).Where(p => p.CategoryId == CategoryId).OrderByDescending(
                    p => p.ReleaseDate).Select(lArticle => lArticle)).ToList();
            }
            else
            {
                lArticles = (from lArticle in Articlesctx.Articles
                             where lArticle.Category.CategoryID == CategoryId
                             orderby lArticle.ReleaseDate descending
                             select lArticle).ToList();
            }

            if (EnableCaching)
            {
                CacheData(key, lArticles, CacheDuration);
            }


            return lArticles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetArticleCountByCategory(int CategoryId)
        {
            return (Articlesctx.Articles.Where(lai => lai.Category.CategoryID == CategoryId)).Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="bIncludeCategories"></param>
        /// <param name="bIncludeComments"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Article GetArticleById(int ArticleId, bool bIncludeCategories, bool bIncludeComments)
        {

            if (ArticleId > 0)
            {

                if (bIncludeCategories & bIncludeComments)
                {

                    return (Articlesctx.Articles.Include("Categories").Include("Comments").Where(
                        lai => lai.ArticleID == ArticleId)).FirstOrDefault();
                }
                else if (bIncludeCategories & !bIncludeComments)
                {

                    return (Articlesctx.Articles.Include("Categories").Where(lai => lai.ArticleID == ArticleId)).FirstOrDefault();
                }
                else if (!bIncludeCategories & !bIncludeComments)
                {

                    return (Articlesctx.Articles.Where(
                        lai => lai.ArticleID == ArticleId)).FirstOrDefault();

                }
            }

            throw new ArgumentException("The ArticleId is not valid.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Article GetArticleById(int ArticleId)
        {
            return GetArticleById(ArticleId, false, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetArticleBodyById(int ArticleId)
        {
            return (Articlesctx.Articles.Where(lai => lai.ArticleID == ArticleId).Select(lai => lai.Body)).First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetArticleCount()
        {
            return Articlesctx.Articles.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool RateArticle(int ArticleId, int rating)
        {

            Article lArticle = GetArticleById(ArticleId);

            lArticle.TotalRating += rating;
            lArticle.Votes += 1;

            try
            {
                Articlesctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (OptimisticConcurrencyException ex)
            {
                // catching this exception allows you to 
                // refresh entities with either store/client wins
                // project the entities into this failed entities.
                var failedEntities = from e1 in ex.StateEntries
                                     select e1.Entity;

                // Note: in future you should be able to just pass the opt.StateEntities in to refresh.
                Articlesctx.Refresh(RefreshMode.ClientWins, failedEntities.ToList());

                Articlesctx.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;

            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IncrementArticleViewCount(int ArticleId)
        {

            Article lArticle = GetArticleById(ArticleId);

            lArticle.ViewCount += 1;

            try
            {
                Articlesctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ApproveArticle(int ArticleId)
        {

            Article lArticle = GetArticleById(ArticleId);

            if (ArticleId > 0)
            {
                lArticle.Approved = true;

                try
                {
                    Articlesctx.SaveChanges();
                    base.PurgeCacheItems(CacheKey);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }


            throw new ArgumentException("The ArticleId is not valid.");
        }

        public Article AddArticle(int articleID, string title, string body, bool approved, 
            bool listed, bool commentsEnabled, bool onlyForMembers, int viewCount, int votes, int totalRating)
        {

            Article article;

            if (articleID > 0)
            {

                article = GetArticleById(articleID);

                article.ArticleID = articleID;
                article.UpdatedDate = DateTime.Now;
                article.UpdatedBy = Helpers.CurrentUserName;
                article.Title = title;
                article.Body = body;
                article.Approved = approved;
                article.Listed = listed;
                article.CommentsEnabled = commentsEnabled;
                article.OnlyForMembers = onlyForMembers;
                article.ViewCount = viewCount;
                article.Votes = votes;

                article.TotalRating = totalRating;
            }
            else
            {
                article = Article.CreateArticle(articleID, DateTime.Now, Helpers.CurrentUserName,
                    title, body, approved, listed, commentsEnabled,
                onlyForMembers, viewCount, votes, totalRating, DateTime.Now, true);
            }

            return AddArticle(article);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Article AddArticle(Article vArticle)
        {

            try
            {
                if (vArticle.EntityState == EntityState.Detached)
                {
                    Articlesctx.AddToArticles(vArticle);
                }
                base.PurgeCacheItems(CacheKey);

                return Articlesctx.SaveChanges() > 0 ? vArticle : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + vArticle.ArticleID, ex);
                return null;

            }
        }

        #region " Delete Operations "

        public bool DeleteArticle(int vArticleId)
        {
            return ChangeDeletedState(this.GetArticleById(vArticleId), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteArticle(Article vArticle)
        {
            return ChangeDeletedState(vArticle, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeleteArticle(Article vArticle)
        {
            return ChangeDeletedState(vArticle, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vArticle"></param>
        /// <param name="vState"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ChangeDeletedState(Article vArticle, bool vState)
        {
            vArticle.Active = vState;
            vArticle.UpdatedDate = DateTime.Now;
            vArticle.UpdatedBy = CurrentUserName;

            try
            {
                Articlesctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vArticle.ArticleID.ToString(), ex);
                return false;

            }
        }

        #endregion

    }
}
