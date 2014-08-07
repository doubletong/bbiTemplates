namespace TheBeerHouse.BLL
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Runtime.CompilerServices;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Caching;
    using TheBeerHouse;

    /// <summary>
    /// Serves as a base class to derive 
    /// </summary>
    /// <remarks></remarks>
    public abstract class BaseRepository : IDisposable
    {
        private Dictionary<string, Exception> _activeExceptions;
        private int _cacheDuration = 0;
        private string _cacheKey = "CacheKey";
        private string _connectionString = "Set the ConnectionString";
        private bool _enableCaching = true;
        public const int DefPageSize = 50;
        private bool disposedValue = false;
        protected const int MAXROWS = 0x7fffffff;

        protected static void CacheData(string key, object data)
        {
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(data)))
            {
                Cache.Insert(key, RuntimeHelpers.GetObjectValue(data), null, DateTime.Now.AddSeconds(120.0), TimeSpan.Zero);
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
            VBMath.Randomize();
            return (int) Math.Round((double) Conversion.Int((float) ((max - min) * VBMath.Rnd())));
        }

        /// <summary>
        /// </summary>
        /// <param name="prefix"></param>
        /// <remarks></remarks>
        protected void PurgeCacheItems(string prefix)
        {
            List<string>.Enumerator VB$t_struct$L0;
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
            try
            {
                VB$t_struct$L0 = itemsToRemove.GetEnumerator();
                while (VB$t_struct$L0.MoveNext())
                {
                    string itemToRemove = VB$t_struct$L0.Current;
                    Cache.Remove(itemToRemove);
                }
            }
            finally
            {
                VB$t_struct$L0.Dispose();
            }
        }

        public Dictionary<string, Exception> ActiveExceptions
        {
            get
            {
                if (Information.IsNothing(this._activeExceptions))
                {
                    this._activeExceptions = new Dictionary<string, Exception>();
                }
                return this._activeExceptions;
            }
            set
            {
                this._activeExceptions = value;
            }
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

