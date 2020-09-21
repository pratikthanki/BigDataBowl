using System;

namespace NFL.BigDataBowl.Utilities
{
    public static class StringParser
    {
        public static float ToFloat(string number) =>
            (float) (number == "" || number == "0" ? 0.0 : float.Parse(number));

        public static int ToInt(string number) => number == "" ? 0 : int.Parse(number);

        public static long ToLong(string number) => long.Parse(number);

        public static DateTime ToDateTime(string dateTime, string format) =>
            DateTime.ParseExact(dateTime, format, null);
    }
}