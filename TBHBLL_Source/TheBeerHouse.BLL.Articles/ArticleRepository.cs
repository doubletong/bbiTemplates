namespace TheBeerHouse.BLL.Articles
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

    public class ArticleRepository : BaseArticleRepository
    {
        [CompilerGenerated, DebuggerStepThrough]
        private static object _Lambda$__1(ObjectStateEntry e1)
        {
            return e1.Entity;
        }

        /// <summary>
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Article AddArticle(Article vArticle)
        {
            Article AddArticle;
            try
            {
                if (vArticle.EntityState == EntityState.Detached)
                {
                    this.Articlesctx.AddToArticles(vArticle);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddArticle = (this.Articlesctx.SaveChanges() > 0) ? vArticle : null;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(this.CacheKey + "_" + Conversions.ToString(vArticle.ArticleID), ex);
                AddArticle = null;
                ProjectData.ClearProjectError();
                return AddArticle;
                ProjectData.ClearProjectError();
            }
            return AddArticle;
        }

        public Article AddArticle(int articleID, string title, string body, bool approved, bool listed, bool commentsEnabled, bool onlyForMembers, int viewCount, int votes, int totalRating)
        {
            Article article;
            if (articleID > 0)
            {
                article = this.GetArticleById(articleID);
                article.ArticleID = articleID;
                article.UpdatedDate = DateAndTime.Now;
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
                article = Article.CreateArticle(articleID, DateAndTime.Now, Helpers.CurrentUserName, DateAndTime.Now, true, title, body, approved, listed, commentsEnabled, onlyForMembers, viewCount, votes, totalRating);
            }
            return this.AddArticle(article);
        }

        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ApproveArticle(int ArticleId)
        {
            Article lArticle = this.GetArticleById(ArticleId);
            if (ArticleId > 0)
            {
                lArticle.Approved = true;
                try
                {
                    this.Articlesctx.SaveChanges();
                    base.PurgeCacheItems(this.CacheKey);
                    return true;
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Exception ex = exception1;
                    bool ApproveArticle = false;
                    ProjectData.ClearProjectError();
                    return ApproveArticle;
                    ProjectData.ClearProjectError();
                }
            }
            throw new ArgumentException("The ArticleId is not valid.");
        }

        /// <summary>
        /// </summary>
        /// <param name="vArticle"></param>
        /// <param name="vState"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ChangeDeletedState(Article vArticle, bool vState)
        {
            bool ChangeDeletedState;
            vArticle.Active = vState;
            vArticle.UpdatedDate = DateAndTime.Now;
            vArticle.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Articlesctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vArticle.ArticleID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        public bool DeleteArticle(int vArticleId)
        {
            return this.ChangeDeletedState(this.GetArticleById(vArticleId), false);
        }

        /// <summary>
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteArticle(Article vArticle)
        {
            return this.ChangeDeletedState(vArticle, false);
        }

        public List<Article> GetActiveArticles()
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            string key = "Active_ " + this.CacheKey;
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Article>) BaseRepository.Cache[key];
            }
            this.Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            List<Article> lArticles = this.Articlesctx.Articles.Include("Category").Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Active)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Article>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lArticles);
            }
            return lArticles;
        }

        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetArticleBodyById(int ArticleId)
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            _Closure$__5 $VB$Closure_ClosureVariable_159_C = new _Closure$__5();
            $VB$Closure_ClosureVariable_159_C.$VB$Local_ArticleId = ArticleId;
            return this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lai"), (MethodInfo) methodof(Article.get_ArticleID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_159_C, typeof(_Closure$__5)), fieldof(_Closure$__5.$VB$Local_ArticleId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).Select<Article, string>(Expression.Lambda<Func<Article, string>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Article), "lai"), (MethodInfo) methodof(Article.get_Body)), new ParameterExpression[] { VB$t_ref$S1 })).First<string>();
        }

        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Article GetArticleById(int ArticleId)
        {
            return this.GetArticleById(ArticleId, false, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="bIncludeCategories"></param>
        /// <param name="bIncludeComments"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Article GetArticleById(int ArticleId, bool bIncludeCategories, bool bIncludeComments)
        {
            _Closure$__4 $VB$Closure_ClosureVariable_12E_C = new _Closure$__4();
            $VB$Closure_ClosureVariable_12E_C.$VB$Local_ArticleId = ArticleId;
            if ($VB$Closure_ClosureVariable_12E_C.$VB$Local_ArticleId > 0)
            {
                if (bIncludeCategories & bIncludeComments)
                {
                    ParameterExpression VB$t_ref$S0;
                    return this.Articlesctx.Articles.Include("Categories").Include("Comments").Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lai"), (MethodInfo) methodof(Article.get_ArticleID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_12E_C, typeof(_Closure$__4)), fieldof(_Closure$__4.$VB$Local_ArticleId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Article>();
                }
                if (bIncludeCategories & !bIncludeComments)
                {
                    ParameterExpression VB$t_ref$S1;
                    return this.Articlesctx.Articles.Include("Categories").Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Article), "lai"), (MethodInfo) methodof(Article.get_ArticleID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_12E_C, typeof(_Closure$__4)), fieldof(_Closure$__4.$VB$Local_ArticleId)), true, null), new ParameterExpression[] { VB$t_ref$S1 })).FirstOrDefault<Article>();
                }
                if (!bIncludeCategories & !bIncludeComments)
                {
                    ParameterExpression VB$t_ref$S2;
                    return this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(Article), "lai"), (MethodInfo) methodof(Article.get_ArticleID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_12E_C, typeof(_Closure$__4)), fieldof(_Closure$__4.$VB$Local_ArticleId)), true, null), new ParameterExpression[] { VB$t_ref$S2 })).FirstOrDefault<Article>();
                }
            }
            throw new ArgumentException("The ArticleId is not valid.");
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetArticleCount()
        {
            return this.Articlesctx.Articles.Count<Article>();
        }

        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetArticleCountByCategory(int CategoryId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__3 $VB$Closure_ClosureVariable_120_C = new _Closure$__3();
            $VB$Closure_ClosureVariable_120_C.$VB$Local_CategoryId = CategoryId;
            return this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lai"), (MethodInfo) methodof(Article.get_Category)), (MethodInfo) methodof(Category.get_CategoryID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_120_C, typeof(_Closure$__3)), fieldof(_Closure$__3.$VB$Local_CategoryId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).Count<Article>();
        }

        public List<Article> GetArticles()
        {
            ParameterExpression VB$t_ref$S0;
            string key = this.CacheKey + "_FullList";
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Article>) BaseRepository.Cache[key];
            }
            List<Article> lArticles = this.Articlesctx.Articles.OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Article>();
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lArticles);
            }
            return lArticles;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetArticles(bool PublishedOnly)
        {
            List<Article> lArticles;
            string key = string.Format(this.CacheKey + "_PublishedOnly_{0}", PublishedOnly.ToString());
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Article>) BaseRepository.Cache[key];
            }
            this.Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            if (PublishedOnly)
            {
                ParameterExpression VB$t_ref$S0;
                ParameterExpression VB$t_ref$S1;
                lArticles = this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Published)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S1 })).Skip<Article>(10).Take<Article>(15).ToList<Article>();
            }
            else
            {
                ParameterExpression VB$t_ref$S2;
                lArticles = this.Articlesctx.Articles.OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S2 })).ToList<Article>();
            }
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lArticles);
            }
            return lArticles;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetArticles(bool PublishedOnly, int PageIndex, int PageSize)
        {
            List<Article> lArticles;
            string key = string.Format(this.CacheKey + "_{0}_{1}_{2}", PageIndex.ToString(), PageSize.ToString(), PublishedOnly.ToString());
            if (((this.EnableCaching && !Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key]))) ? 1 : 0) != 0)
            {
                return (List<Article>) BaseRepository.Cache[key];
            }
            this.Articlesctx.Articles.MergeOption = MergeOption.NoTracking;
            if (PublishedOnly)
            {
                ParameterExpression VB$t_ref$S0;
                ParameterExpression VB$t_ref$S1;
                lArticles = this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Published)), Expression.Constant(true, typeof(bool)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S1 })).Skip<Article>((PageIndex * PageSize)).Take<Article>(PageSize).ToList<Article>();
            }
            else
            {
                ParameterExpression VB$t_ref$S2;
                lArticles = this.Articlesctx.Articles.OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S2 })).Skip<Article>((PageIndex * PageSize)).Take<Article>(PageSize).ToList<Article>();
            }
            if (this.EnableCaching)
            {
                BaseRepository.CacheData(key, lArticles);
            }
            return lArticles;
        }

        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetArticlesByCategory(int CategoryId)
        {
            return this.GetArticlesByCategory(CategoryId, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="PublishedOnly"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetArticlesByCategory(int CategoryId, bool PublishedOnly)
        {
            List<Article> lArticles;
            _Closure$__2 $VB$Closure_ClosureVariable_F8_C = new _Closure$__2();
            $VB$Closure_ClosureVariable_F8_C.$VB$Local_CategoryId = CategoryId;
            string key = this.CacheKey + "ByCategory_" + Conversions.ToString($VB$Closure_ClosureVariable_F8_C.$VB$Local_CategoryId) + "_" + Conversions.ToString(PublishedOnly);
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key])))
            {
                return (List<Article>) BaseRepository.Cache[key];
            }
            if (PublishedOnly)
            {
                ParameterExpression VB$t_ref$S0;
                ParameterExpression VB$t_ref$S1;
                lArticles = this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Coalesce(Expression.And(Expression.And(Expression.And(Expression.Convert(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Approved)), typeof(bool?)), Expression.LessThanOrEqual(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ReleaseDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_LessThanOrEqual))), Expression.GreaterThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ExpireDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_GreaterThan))), Expression.Convert(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_Category)), (MethodInfo) methodof(Category.get_CategoryID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_F8_C, typeof(_Closure$__2)), fieldof(_Closure$__2.$VB$Local_CategoryId)), true, null), typeof(bool?))), Expression.Constant(false, typeof(bool))), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S1 })).ToList<Article>();
            }
            else
            {
                ParameterExpression VB$t_ref$S2;
                ParameterExpression VB$t_ref$S3;
                lArticles = this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S2 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Category)), (MethodInfo) methodof(Category.get_CategoryID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_F8_C, typeof(_Closure$__2)), fieldof(_Closure$__2.$VB$Local_CategoryId)), true, null), new ParameterExpression[] { VB$t_ref$S2 })).OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S3 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S3 })).ToList<Article>();
            }
            BaseRepository.CacheData(key, lArticles);
            return lArticles;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetHomePageArticles()
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            string key = this.CacheKey + "_HomePage";
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(BaseRepository.Cache[key])))
            {
                return (List<Article>) BaseRepository.Cache[key];
            }
            List<Article> lArticles = this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Coalesce(Expression.And(Expression.And(Expression.Convert(Expression.And(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Active)), Expression.Constant(true, typeof(bool)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_Approved)), Expression.Constant(true, typeof(bool)), true, null)), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_Listed)), Expression.Constant(true, typeof(bool)), true, null)), typeof(bool?)), Expression.LessThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ReleaseDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_LessThan))), Expression.GreaterThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ExpireDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_GreaterThan))), Expression.Constant(false, typeof(bool))), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Article, DateTime>(Expression.Lambda<Func<Article, DateTime>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_AddedDate)), new ParameterExpression[] { VB$t_ref$S1 })).Take<Article>(3).ToList<Article>();
            BaseRepository.CacheData(key, lArticles);
            return lArticles;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetPublishedArticles()
        {
            return this.GetArticles(true);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetPublishedArticles(int categoryId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__1 $VB$Closure_ClosureVariable_DD_C = new _Closure$__1();
            $VB$Closure_ClosureVariable_DD_C.$VB$Local_categoryId = categoryId;
            return this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Coalesce(Expression.And(Expression.And(Expression.Convert(Expression.And(Expression.And(Expression.Equal(Expression.Property(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Category)), (MethodInfo) methodof(Category.get_CategoryID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_DD_C, typeof(_Closure$__1)), fieldof(_Closure$__1.$VB$Local_categoryId)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_Approved)), Expression.Constant(true, typeof(bool)), true, null)), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_Listed)), Expression.Constant(true, typeof(bool)), true, null)), typeof(bool?)), Expression.LessThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ReleaseDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_LessThan))), Expression.GreaterThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ExpireDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_GreaterThan))), Expression.Constant(false, typeof(bool))), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Article>();
        }

        /// <summary>
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Article> GetPublishedArticles(int pageIndex, int pageSize)
        {
            ParameterExpression VB$t_ref$S0;
            return this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Coalesce(Expression.And(Expression.And(Expression.Convert(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Approved)), Expression.Constant(true, typeof(bool)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_Listed)), Expression.Constant(true, typeof(bool)), true, null)), typeof(bool?)), Expression.LessThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ReleaseDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_LessThan))), Expression.GreaterThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ExpireDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_GreaterThan))), Expression.Constant(false, typeof(bool))), new ParameterExpression[] { VB$t_ref$S0 })).Skip<Article>((pageIndex * pageSize)).Take<Article>(pageSize).ToList<Article>();
        }

        public IEnumerable<Article> GetRSSArticles()
        {
            ParameterExpression VB$t_ref$S0;
            ParameterExpression VB$t_ref$S1;
            string key = this.CacheKey + "_IEnumerable";
            return this.Articlesctx.Articles.Where<Article>(Expression.Lambda<Func<Article, bool>>(Expression.Coalesce(Expression.And(Expression.And(Expression.Convert(Expression.And(Expression.And(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_Active)), Expression.Constant(true, typeof(bool)), true, null), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_Approved)), Expression.Constant(true, typeof(bool)), true, null)), Expression.Equal(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_Listed)), Expression.Constant(true, typeof(bool)), true, null)), typeof(bool?)), Expression.LessThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ReleaseDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_LessThan))), Expression.GreaterThan(Expression.Property(VB$t_ref$S0, (MethodInfo) methodof(Article.get_ExpireDate)), Expression.Convert(Expression.Property(null, (MethodInfo) methodof(DateAndTime.get_Now)), typeof(DateTime?)), true, (MethodInfo) methodof(DateTime.op_GreaterThan))), Expression.Constant(false, typeof(bool))), new ParameterExpression[] { VB$t_ref$S0 })).OrderByDescending<Article, DateTime?>(Expression.Lambda<Func<Article, DateTime?>>(Expression.Property(VB$t_ref$S1 = Expression.Parameter(typeof(Article), "lArticle"), (MethodInfo) methodof(Article.get_ReleaseDate)), new ParameterExpression[] { VB$t_ref$S1 })).AsEnumerable<Article>();
        }

        public ObjectQuery<Article> GetSpecialArticles()
        {
            string queryString = "SELECT VALUE Article FROM TheBeerHouse.Articles AS Article";
            return this.Articlesctx.CreateQuery<Article>(queryString, new ObjectParameter[0]);
        }

        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IncrementArticleViewCount(int ArticleId)
        {
            bool IncrementArticleViewCount;
            Article VB$t_ref$S0 = this.GetArticleById(ArticleId);
            VB$t_ref$S0.ViewCount++;
            try
            {
                this.Articlesctx.SaveChanges();
                IncrementArticleViewCount = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                IncrementArticleViewCount = false;
                ProjectData.ClearProjectError();
                return IncrementArticleViewCount;
                ProjectData.ClearProjectError();
            }
            return IncrementArticleViewCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool RateArticle(int ArticleId, int rating)
        {
            bool RateArticle;
            Article lArticle = this.GetArticleById(ArticleId);
            Article VB$t_ref$S0 = lArticle;
            VB$t_ref$S0.TotalRating += rating;
            VB$t_ref$S0 = lArticle;
            VB$t_ref$S0.Votes++;
            try
            {
                this.Articlesctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                RateArticle = true;
            }
            catch (OptimisticConcurrencyException exception1)
            {
                ProjectData.SetProjectError(exception1);
                OptimisticConcurrencyException ex = exception1;
                IEnumerable<object> failedEntities = ex.StateEntries.Select<ObjectStateEntry, object>(new Func<ObjectStateEntry, object>(ArticleRepository._Lambda$__1));
                this.Articlesctx.Refresh(RefreshMode.ClientWins, (IEnumerable) failedEntities.ToList<object>());
                this.Articlesctx.SaveChanges();
                ProjectData.ClearProjectError();
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                Exception ex = exception2;
                RateArticle = false;
                ProjectData.ClearProjectError();
                return RateArticle;
                ProjectData.ClearProjectError();
            }
            return RateArticle;
        }

        /// <summary>
        /// </summary>
        /// <param name="vArticle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UnDeleteArticle(Article vArticle)
        {
            return this.ChangeDeletedState(vArticle, true);
        }

        [CompilerGenerated]
        internal class _Closure$__1
        {
            public int $VB$Local_categoryId;

            [DebuggerNonUserCode]
            public _Closure$__1()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__1(ArticleRepository._Closure$__1 other)
            {
                if (other != null)
                {
                    this.$VB$Local_categoryId = other.$VB$Local_categoryId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__2
        {
            public int $VB$Local_CategoryId;

            [DebuggerNonUserCode]
            public _Closure$__2()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__2(ArticleRepository._Closure$__2 other)
            {
                if (other != null)
                {
                    this.$VB$Local_CategoryId = other.$VB$Local_CategoryId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__3
        {
            public int $VB$Local_CategoryId;

            [DebuggerNonUserCode]
            public _Closure$__3()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__3(ArticleRepository._Closure$__3 other)
            {
                if (other != null)
                {
                    this.$VB$Local_CategoryId = other.$VB$Local_CategoryId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__4
        {
            public int $VB$Local_ArticleId;

            [DebuggerNonUserCode]
            public _Closure$__4()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__4(ArticleRepository._Closure$__4 other)
            {
                if (other != null)
                {
                    this.$VB$Local_ArticleId = other.$VB$Local_ArticleId;
                }
            }
        }

        [CompilerGenerated]
        internal class _Closure$__5
        {
            public int $VB$Local_ArticleId;

            [DebuggerNonUserCode]
            public _Closure$__5()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__5(ArticleRepository._Closure$__5 other)
            {
                if (other != null)
                {
                    this.$VB$Local_ArticleId = other.$VB$Local_ArticleId;
                }
            }
        }
    }
}

