using System;

namespace NFL.BigDataBowl.Utilities
{
    public static class InputHandler
    {
        public static float ParseFloat(string number) =>
            (float) (number == "" || number == "0" ? 0.0 : float.Parse(number));

        public static int ParseInt(string number) => number == "" ? 0 : int.Parse(number);

        public static long ParseLong(string number) => long.Parse(number);

        public static DateTime ParseDateTime(string number, string format) =>
            DateTime.ParseExact(number, format, null);
    }
}