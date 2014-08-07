using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Principal;
using System.Web;

namespace BBICMS.BLL
{
    /// <summary>
    /// Serves as a base class to derive 
    /// </summary>
    /// <remarks></remarks>
    public abstract class BaseRepository : IDisposable
    {
        private int _cacheDuration = 0;
        private string _cacheKey = "CacheKey";
        private string _connectionString = "Set the ConnectionString";
        private bool _enableCaching = true;
        public const int DefPageSize = 50;
        protected const int MAXROWS = 0x7fffffff;

        protected static void CacheData(string key, object data)
        {
            CacheData(key, data, 120);
        }

        protected static void CacheData(string key, object data, int vCacheDuration)
        {
            if (null != data)
            {
                Cache.Insert(key, data, null, DateTime.Now.AddSeconds(vCacheDuration), TimeSpan.Zero);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static string ConvertNullToEmptyString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return input;
        }

        public abstract void Dispose();
        protected abstract void Dispose(bool disposing);
        /// <summary>
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static string EncodeText(string content)
        {
            content = HttpUtility.HtmlEncode(content);
            content = content.Replace("  ", " &nbsp;&nbsp;").Replace(@"\n", "<br>");
            return content;
        }

        protected string GetActualConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[this.ConnectionString].ConnectionString;
        }

        /// <summary>
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static int GetPageIndex(int startRowIndex, int maximumRows)
        {
            if (maximumRows <= 0)
            {
                return 0;
            }
            return (int) Math.Round(Math.Floor((double) (((double) startRowIndex) / ((double) maximumRows))));
        }

        public int GetRandItem(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }

        /// <summary>
        /// </summary>
        /// <param name="prefix"></param>
        /// <remarks></remarks>
        protected void PurgeCacheItems(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> itemsToRemove = new List<string>();

            IDictionaryEnumerator enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                {
                    itemsToRemove.Add(enumerator.Key.ToString());
                }
            }

            foreach (string itemToRemove in itemsToRemove)
            {
                Cache.Remove(itemToRemove);
            }
        }

        private Dictionary<string, Exception> _activeExceptions;
        public Dictionary<string, Exception> ActiveExceptions
        {
            get
            {
                if ((_activeExceptions == null))
                {
                    _activeExceptions = new Dictionary<string, Exception>();
                }
                return _activeExceptions;
            }
            set { _activeExceptions = value; }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static System.Web.Caching.Cache Cache
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }

        protected int CacheDuration
        {
            get
            {
                return this._cacheDuration;
            }
            set
            {
                this._cacheDuration = value;
            }
        }

        public string CacheKey
        {
            get
            {
                return this._cacheKey;
            }
            set
            {
                this._cacheKey = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static IPrincipal CurrentUser
        {
            get
            {
                return Helpers.CurrentUser;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static string CurrentUserIP
        {
            get
            {
                return Helpers.CurrentUserIP;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static string CurrentUserName
        {
            get
            {
                return Helpers.CurrentUserName;
            }
        }

        protected bool EnableCaching
        {
            get
            {
                return this._enableCaching;
            }
            set
            {
                this._enableCaching = value;
            }
        }
    }
}

