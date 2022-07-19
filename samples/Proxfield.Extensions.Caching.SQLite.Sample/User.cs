namespace Proxfield.Extensions.Caching.SQLite.Sample
{
    public class User : Cacheable
    {
        public override string? Key { get; set; }
        public string Name { get; set; }
    }
}
