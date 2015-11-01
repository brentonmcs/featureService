using System;
using ServiceStack.Redis;

namespace WH.FeatureService.Api.Common
{
    public class RedisCache : ICache
    {
        IRedisClientsManager _redisCacheManager;

        public RedisCache(IRedisClientsManager redisCacheManager)
        {
            _redisCacheManager = redisCacheManager;
        }

        public T Get<T>(string key)
        {
            using (var redis = _redisCacheManager.GetClient())
            {
                return redis.Get<T>(key);
            }
        }

        public void Set<T>(string key, T value)
        {
            using (var redis = _redisCacheManager.GetClient())
            {
                redis.Set<T>(key, value);
            }
        }
    }

}