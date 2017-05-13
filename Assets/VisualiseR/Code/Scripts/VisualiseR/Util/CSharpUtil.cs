using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualiseR.Util
{
    public static class CSharpUtil
    {
        public static T ToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        public static List<string> EnumToList<T>()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }
    }
}