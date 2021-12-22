using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace TinyBrowser.Components
{
    /// <summary>
    /// 缓存辅助类，可用于WinForm
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>如果缓存中存在指定的key，则优先从缓存中返回</summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="key">缓存关键字</param>
        /// <param name="cachePopulate">需要执行的方法、Lambda表达式、(匿名)代理等。
        /// <code>例如：() => "测试",  或者 
        /// delegate () { return new aaa("测试"); },
        /// </code>
        /// </param>
        /// <param name="slidingExpiration">滑动窗口模式的使用过期时间</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        /// <returns></returns>
        public static T GetCacheItem<T>(String key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            if (MemoryCache.Default[key] == null)
            {
                if (MemoryCache.Default[key] == null)
                {
                    var item = new CacheItem(key, cachePopulate());
                    var policy = CreatePolicy(slidingExpiration, absoluteExpiration);
                    MemoryCache.Default.Add(item, policy);
                }
            }
            return (T)((MemoryCache.Default[key] is T) ? MemoryCache.Default[key] : default(T));
        }

        /// <summary>构造缓存过期时间和优先级</summary>
        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();
            if (slidingExpiration.HasValue)
                policy.SlidingExpiration = slidingExpiration.Value;
            else if (absoluteExpiration.HasValue)
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            policy.Priority = CacheItemPriority.Default;
            return policy;
        }

    }
}
