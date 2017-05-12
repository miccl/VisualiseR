using System;

namespace VisualiseR.Util
{
    public static class CSharpUtil
    {
        public static T ToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }
    }
}