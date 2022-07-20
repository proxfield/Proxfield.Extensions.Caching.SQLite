using System.Diagnostics.CodeAnalysis;

namespace Proxfield.Extensions.Caching.SQLite.Sample
{
    [ExcludeFromCodeCoverage]
    public class User : Cacheable
    {
        public override string? Key { get; set; }
        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
        }
    }
}
