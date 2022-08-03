using System.Diagnostics.CodeAnalysis;

namespace Proxfield.Extensions.Caching.SQLite.Sql.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseModel
    {
        internal abstract string _table { get; }
    }
}
