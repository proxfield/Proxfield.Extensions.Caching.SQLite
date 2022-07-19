using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxfield.Extensions.Caching.SQLite.Extensions
{
    public static class StringExtension
    {
        private static string[] SpecialChars = new[] { "|", "$" };
        public static string RemoveSpecialChars(this string? value)
        {
            if (value == null) return string.Empty;

            SpecialChars.ToList().ForEach(p =>
            {
                value = value.Replace(p, string.Empty);
            });

            return value;
        }


    }
}
