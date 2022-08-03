using System.Diagnostics.CodeAnalysis;

namespace Proxfield.Extensions.Caching.SQLite
{
    /// <summary>
    /// Defines the object as a cacheable on
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class Cacheable
    {
        /// <summary>
        /// Id of the object on the database, {collection}/{index}, e.g.: "users/1"
        /// </summary>
        public virtual string? Key { get; set; }
    }
}
