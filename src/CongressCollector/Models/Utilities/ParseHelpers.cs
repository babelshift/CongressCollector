using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector.Models.Utilities
{
    public static class ParseHelpers
    {
        public static string GetFirstStringOrEmpty(IList<string> items)
        {
            return items != null && items.Count > 0 ? items[0] : String.Empty;
        }

        /// <summary>
        /// Attempts to combine a date and time to form a DateTime. Ignores the time component if it's empty or white space only.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime? ParseNullableDateTime(string date, string time)
        {
            if (String.IsNullOrWhiteSpace(date)) { return null; }

            if (time == null) { time = String.Empty; }

            string rawDateTime = String.Format("{0} {1}", date.Trim(), time.Trim());

            DateTime dateTime;
            if (DateTime.TryParse(rawDateTime, out dateTime))
            {
                return dateTime;
            }

            return null;
        }

        /// <summary>
        /// Attempts to parse a date to a DateTime and returns null if parsing fails.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime? ParseNullableDateTime(string dateTime)
        {
            if (String.IsNullOrWhiteSpace(dateTime))
            {
                return null;
            }

            DateTime updateDate;
            if (DateTime.TryParse(dateTime, out updateDate))
            {
                return (DateTime?)updateDate;
            }

            return null;
        }
    }
}