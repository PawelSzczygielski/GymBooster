using System.Collections.Generic;
using System.Linq;

namespace GymBooster.Common.Utils
{
    public static class CollectionExtensionMethods
    {
        public static bool NullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}