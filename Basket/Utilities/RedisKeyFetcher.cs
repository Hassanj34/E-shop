using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Basket.Utilities
{
    public class RedisKeyFetcher
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisKeyFetcher(ConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public IEnumerable<RedisKey> GetAllKeys(string pattern = "*")
        {
            var server = _redis.GetServer(_redis.GetEndPoints()[0]);
            return server.Keys(pattern: pattern);
        }
    }
}
