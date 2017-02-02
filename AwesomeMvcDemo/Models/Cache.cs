using System;
using System.Collections.Concurrent;

namespace AwesomeMvcDemo.Models
{
    public static class Cache
    {
        private class CacheItem
        {
            public object Value { get; set; }

            public DateTime Date { get; set; }
        }

        private static readonly ConcurrentDictionary<string, CacheItem> Items = new ConcurrentDictionary<string, CacheItem>();

        public static object Get(string key)
        {
            CacheItem item;
            if (Items.TryGetValue(key, out item))
            {
                item.Date = DateTime.Now;
                return item.Value;
            }

            return null;
        }

        public static void Set(string key, object value)
        {
            var newTuple = new CacheItem { Value = value, Date = DateTime.Now };
            Items.AddOrUpdate(key, newTuple, (k, v) => newTuple);
        }

        public static object Remove(string key)
        {
            CacheItem tuple;
            Items.TryRemove(key, out tuple);
            return tuple.Value;
        }

        public static void RemoveExpired(int milliseconds = 0)
        {
            if (milliseconds == 0) milliseconds = 60 * 1000 * 10;

            foreach (var item in Items)
            {
                if (item.Value.Date < DateTime.Now.AddMilliseconds(-milliseconds))
                {
                    Remove(item.Key);
                }
            }
        }
    }
}