using System;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for time.
    /// </summary>
    public static class TimeUtil
    {
        private static readonly string TIME_FORMAT = "{0:0}:{1:00}";

        /// <summary>
        /// Formats time given in minutes and seconds to time with schema <see cref="TIME_FORMAT"/>
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string FormatTime(float minutes, float seconds)
        {
            return string.Format(TIME_FORMAT, minutes, seconds);
        }

        /// <summary>
        /// Formats time given in seconds to time with schema <see cref="TIME_FORMAT"/>
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string FormatTime(float seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return string.Format(TIME_FORMAT, t.Minutes, t.Seconds);
        }
    }
}