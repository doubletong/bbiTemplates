namespace TheBeerHouse
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Caching;

    public class TBHCache : ICache
    {
        public void Add(string key, object value)
        {
            this.Add(key, RuntimeHelpers.GetObjectValue(value), null);
        }

        public object Add(string key, object value, CacheDependency dependencies)
        {
            TimeSpan VB$t_struct$N0;
            return Cache.Add(key, RuntimeHelpers.GetObjectValue(value), dependencies, DateTime.MinValue, VB$t_struct$N0, (CacheItemPriority) 0, null);
        }

        public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            object Add;
            return Add;
        }

        public object Get(string key)
        {
            object Get;
            return Get;
        }

        public object Get(string key, TheBeerHouse.CacheGetOptions getOptions)
        {
            object Get;
            return Get;
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            IDictionaryEnumerator GetEnumerator;
            return GetEnumerator;
        }

        public void Insert(string key, object value)
        {
        }

        public void Insert(string key, object value, CacheDependency dependencies)
        {
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
        }

        public void Invalidate(IEnumerable<string> entitySets)
        {
        }

        public object Remove(string key)
        {
            object Remove;
            return Remove;
        }

        private static void ValidateKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("The Key Name is Empty, there must be a valid name used to work with Cache.", "key");
            }
        }

        protected static System.Web.Caching.Cache Cache
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }

        public int Count
        {
            get
            {
                int Count;
                return Count;
            }
        }

        public long EffectivePercentagePhysicalMemoryLimit
        {
            get
            {
                long EffectivePercentagePhysicalMemoryLimit;
                return EffectivePercentagePhysicalMemoryLimit;
            }
        }

        public long EffectivePrivateBytesLimit
        {
            get
            {
                long EffectivePrivateBytesLimit;
                return EffectivePrivateBytesLimit;
            }
        }

        public object this[string key]
        {
            get
            {
                object Item;
                return Item;
            }
            set
            {
            }
        }

        public int TheBeerHouse.ICache.Count
        {
            get
            {
                int Count;
                return Count;
            }
        }

        public long TheBeerHouse.ICache.EffectivePercentagePhysicalMemoryLimit
        {
            get
            {
                long EffectivePercentagePhysicalMemoryLimit;
                return EffectivePercentagePhysicalMemoryLimit;
            }
        }

        public long TheBeerHouse.ICache.EffectivePrivateBytesLimit
        {
            get
            {
                long EffectivePrivateBytesLimit;
                return EffectivePrivateBytesLimit;
            }
        }

        public object this[string key]
        {
            get
            {
                object Item;
                return Item;
            }
            set
            {
            }
        }
    }
}

