using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for enums.
    /// </summary>
    public static class EnumUtil
    {
        public static T ToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        public static List<string> EnumToList<T>()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }

        public static int Length<T>()
        {
            return Enum.GetNames(typeof(T)).Length;
        }

        public static Array GetValues<T>()
        {
            return Enum.GetValues(typeof(T));
        }
    }
}