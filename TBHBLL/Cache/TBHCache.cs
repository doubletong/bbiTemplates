using System;
using System.Web;
using System.Web.Caching;

public class TBHCache : ICache
{

    protected static Cache Cache
    {
        get { return HttpContext.Current.Cache; }
    }

    public void Invalidate(System.Collections.Generic.IEnumerable<string> entitySets)
    {

    }

    public void Add(string key, object value)
    {
        this.Add(key, value, null);
    }


    private static void ValidateKey(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The Key Name is Empty, there must be a valid name used to work with Cache.", "key");
        }
    }

    public object Add(string key, object value, System.Web.Caching.CacheDependency dependencies)
    {
        return Cache.Add(key, value, dependencies, DateTime.Now.AddMinutes( 20), 
            new TimeSpan(0, 20, 0), CacheItemPriority.Normal, null);
    }

    public object Add(string key, object value, System.Web.Caching.CacheDependency dependencies, System.DateTime absoluteExpiration, System.TimeSpan slidingExpiration, System.Web.Caching.CacheItemPriority priority, System.Web.Caching.CacheItemRemovedCallback onRemoveCallback)
    {
        throw new NotImplementedException();
    }

    public int Count
    {
        get
        {
            throw new NotImplementedException();
        }

    }

    public long EffectivePercentagePhysicalMemoryLimit
    {
        get
        {
            throw new NotImplementedException();
        }

    }

    public long EffectivePrivateBytesLimit
    {
        get
        {
            throw new NotImplementedException();
        }

    }

    public object Get(string key)
    {
        throw new NotImplementedException();
    }

    public object Get(string key, CacheGetOptions getOptions)
    {
        throw new NotImplementedException();
    }

    public System.Collections.IDictionaryEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public void Insert(string key, object value)
    {
        throw new NotImplementedException();
    }

    public void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies)
    {
        throw new NotImplementedException();
    }

    public void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies, System.DateTime absoluteExpiration, System.TimeSpan slidingExpiration)
    {
        throw new NotImplementedException();
    }

    public void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies, System.DateTime absoluteExpiration, System.TimeSpan slidingExpiration, System.Web.Caching.CacheItemPriority priority, System.Web.Caching.CacheItemRemovedCallback onRemoveCallback)
    {
        throw new NotImplementedException();
    }

    public void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies, System.DateTime absoluteExpiration, System.TimeSpan slidingExpiration, System.Web.Caching.CacheItemUpdateCallback onUpdateCallback)
    {
        throw new NotImplementedException();
    }

    public object Item
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }

    }

    public object Remove(string key)
    {
        throw new NotImplementedException();
    }

}