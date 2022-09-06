using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Persistence.Extensions;

public static class DistributedCacheExtensions
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string key, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions();

        options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(1);
        options.SlidingExpiration = unusedExpireTime;

        var jsonData = JsonSerializer.Serialize<T>(data);
        await cache.SetStringAsync(key, jsonData, options);
    }

    public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string key)
    {
        var jsonData = await cache.GetStringAsync(key);

        if (jsonData == null)
        {
            return default(T);
        }

        return JsonSerializer.Deserialize<T>(jsonData);
    }
}
