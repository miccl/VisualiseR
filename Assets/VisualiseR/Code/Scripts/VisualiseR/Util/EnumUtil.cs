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
        /// <summary>
        /// Converts a string value to a enum value.
        /// </summary>
        /// <param name="value">string value</param>
        /// <typeparam name="T">type of the enum</typeparam>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Converts an enum to a list that contains all value of given enum.
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <returns></returns>
        public static List<string> EnumToList<T>()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }

        /// <summary>
        /// Returns the count of all values of an enum.
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <returns></returns>
        public static int Length<T>()
        {
            return Enum.GetNames(typeof(T)).Length;
        }

        /// <summary>
        /// Converts an enum to an array that contains all value of given enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Array GetValues<T>()
        {
            return Enum.GetValues(typeof(T));
        }
    }
}