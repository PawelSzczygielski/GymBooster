using System;

namespace GymBooster.Common.Utils
{
    public static class ObjectExtensionMethods
    {
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }

}
