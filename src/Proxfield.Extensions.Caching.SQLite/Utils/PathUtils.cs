using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proxfield.Extensions.Caching.SQLite.Utils
{
    public static class PathUtils
    {
        public static string ConvertToCurrentLocation(string semiPath) => 
            $@"{Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)}\{semiPath}";
    }
}
