using System;
using System.Globalization;

namespace BadBroker.Application.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ToDate(string strDate) {
            if (DateTime.TryParseExact(strDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return result;

            return DateTime.MinValue;
        }

        public static bool IsValidDate(string strDate)
        {
            if (DateTime.TryParseExact(strDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return true;
            else
                return false;
        }
        public static string ToStringWithFormat(string format, DateTime date) {
            return date.ToString(format);
        }

        public static bool FirstIsSmallerThanSecond(DateTime firstDate, DateTime secondDate) 
        {
            return DateTime.Compare(firstDate, secondDate) <= 0;
        }

        public static bool FirstIsSmallerThanSecond(string firstDate, string secondDate)
        {
            var first = ToDate(firstDate);
            var second = ToDate(secondDate);

            return FirstIsSmallerThanSecond(first, second);
        }
    }
}