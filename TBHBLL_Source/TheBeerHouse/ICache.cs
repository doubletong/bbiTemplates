namespace TheBeerHouse
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Caching;

    internal interface ICache
    {
        object Add(string key, object value, CacheDependency dependencies);
        object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);
        object Get(string key);
        object Get(string key, TheBeerHouse.CacheGetOptions getOptions);
        IDictionaryEnumerator GetEnumerator();
        void Insert(string key, object value);
        void Insert(string key, object value, CacheDependency dependencies);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);
        void Invalidate(IEnumerable<string> entitySets);
        object Remove(string key);

        int Count { get; }

        long EffectivePercentagePhysicalMemoryLimit { get; }

        long EffectivePrivateBytesLimit { get; }

        object this[string key] { get; set; }
    }
}

