using System;
using Microsoft.Extensions.Caching.Memory;

namespace _7PlusCodeChallenge.Repository
{
    public interface ICacheAdapter
    {
        public T Get<T>(object key);
        public void Set(object key, object value);
    }

    public class MemoryCacheAdapter : ICacheAdapter
    {
        private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private static readonly TimeSpan _timeSpan = new TimeSpan(0, 0, 30);

        public T Get<T>(object key)
        {
            return _cache.Get<T>(key);
        }

        public void Set(object key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _cache.Set(key, value, _timeSpan);
        }
    }
}
