using System;
using System.Collections.Generic;

namespace IS4UI.Backend.Api
{
    public static class Extensions
    {
        public static string ToString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        public static string FirstCharacterToLower(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return source;
            return Char.ToLowerInvariant(source[0]) + source.Substring(1);
        }
    }
}
