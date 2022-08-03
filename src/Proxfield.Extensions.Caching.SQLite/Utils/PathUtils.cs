using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Proxfield.Extensions.Caching.SQLite.Utils
{
    [ExcludeFromCodeCoverage]
    public static class PathUtils
    {
        public static string ConvertToCurrentLocation(string semiPath) => 
            $@"{Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)}\{semiPath}";
    }
}
