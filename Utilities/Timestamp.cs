using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Timestamp
    {
        public static double Now()
        {
            //DateTime date = DateTime.Now;
            //DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0);
            //double result = Math.Round(date.Subtract(startTime).TotalSeconds, 0);
            //return result;
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
        public static double Date(DateTime date)
        {
            return new DateTimeOffset(date).ToUnixTimeSeconds();
        }
        public static DateTime ToDateTime(double timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)timestamp).DateTime;
        }
        //chuyển timestamp về datetime địa phương
        public static DateTime ToDateTimeLocal(double timestamp)
        {
            return DateTime.Now.AddSeconds(timestamp - Timestamp.Now());
        }
        public static double ConvertByTimeZone(double time, double timezone)
        {
            return time + (timezone * 3600000);
        }
        public static double ConvertToUTC(double time, double timezone)
        {
            return time - (timezone * 3600000);
        }
        /// <summary>
        /// Lấy thời gian kết thúc
        /// </summary>
        /// <param name="monthNumber"></param>
        /// <returns></returns>
        public static double GetUtcNowEnd(double start, int monthNumber)
        {
            DateTime end = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(start / 1000).AddMonths(monthNumber);
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0);
            var result = Math.Round(end.Subtract(startTime).TotalSeconds, 0);
            return result;
        }
        /// <summary>
        /// Lấy thời gian bởi ngày truyền vào
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="dayNumber"></param>
        /// <returns></returns>
        public static double GetUtcNowByDays(double datetime, int dayNumber)
        {
            DateTime end = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(datetime / 1000).AddMonths(dayNumber);
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0);
            var result = Math.Round(end.Subtract(startTime).TotalSeconds, 0);
            return result;
        }
        public class DateTimes
        {
            public double Start { get; set; }
            public double End { get; set; }
        }
    }
}
