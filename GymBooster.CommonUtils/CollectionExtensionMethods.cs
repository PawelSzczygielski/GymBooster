﻿using System.Collections.Generic;
using System.Linq;

namespace GymBooster.CommonUtils
{
    public static class CollectionExtensionMethods
    {
        public static bool NullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}