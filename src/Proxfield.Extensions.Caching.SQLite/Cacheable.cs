namespace Proxfield.Extensions.Caching.SQLite
{
    public abstract class Cacheable
    {
        public virtual string Key { get; set; }

        private readonly ISQLiteCache _cache;
        public Cacheable(ISQLiteCache cache)
        {
            _cache = cache;
        }

        public void Save(string? key = null) => _cache.SetAsObject(key ?? this.Key, this);

        public void Delete(string key) => _cache.Remove(key);

        public T? Get<T>(string key) => _cache.GetAsObject<T>(key);
    }
}
