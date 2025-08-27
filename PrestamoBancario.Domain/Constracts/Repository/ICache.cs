namespace PrestamoBancario.Domain.Constracts.Repository
{
    internal interface ICache
    {
        Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan ttl);
        void Remove(string key);
    }
}
