using System;

namespace VisualiseR.Util
{
    public static class TimeUtil
    {
        private static readonly string TIME_FORMAT = "{0:0}:{1:00}";

        public static string FormatTime(float minutes, float seconds)
        {
            return string.Format(TIME_FORMAT, minutes, seconds);
        }

        public static string FormatTime(float seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return string.Format(TIME_FORMAT, t.Minutes, t.Seconds);
        }
    }
}