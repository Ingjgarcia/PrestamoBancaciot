
using Microsoft.Extensions.Caching.Memory;

namespace PrestamoBancario.Infrastructure.Cache;

public interface ICache
{
    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan ttl);
    void Remove(string key);
}

public class MemoryCacheAdapter : ICache
{
    private readonly IMemoryCache _cache;
    public MemoryCacheAdapter(IMemoryCache cache) => _cache = cache;

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan ttl)
    {
        if (_cache.TryGetValue(key, out T? value)) return value;
        value = await factory();
        _cache.Set(key, value, ttl);
        return value;
    }

    public void Remove(string key) => _cache.Remove(key);
}
