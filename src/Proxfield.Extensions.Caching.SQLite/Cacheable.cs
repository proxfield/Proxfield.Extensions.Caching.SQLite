namespace Proxfield.Extensions.Caching.SQLite
{
    public abstract class Cacheable
    {
        public virtual string? Key { get; set; }
    }
}
