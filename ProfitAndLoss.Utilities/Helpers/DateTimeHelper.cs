using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProfitAndLoss.Utilities.Helpers
{
    public static class DateTimeHelper
    {

        /// <summary>
        /// Format date time to string
        /// </summary>
        /// <param name="time"></param>
        /// <returns>Return N/A when DateTime Null, else return dd-MM-yyyy</returns>
        public static string ToFormal(this DateTime time)
        {
            if (time != null)
            {
                return time.ToString("dd/MM/yyyy");
            }
            return "N/A";
        }

        /// <summary>
        /// Format date time to string for Viet Nam Region
        /// </summary>
        /// <param name="time"></param>
        /// <returns>Return N/A when DateTime Null, else return Ngày dd Tháng MM Năm yyyy</returns>
        public static string ToFormalVN(this DateTime time)
        {
            if (time != null)
            {
                return string.Format("Ngày " + time.Day + " Tháng " + time.Month + " Năm " + time.Year);
            }
            return "N/A";
        }

        public static string GetMonthYear(this DateTime time)
        {
            if (time != null)
            {
                return $"{time.Month}-{time.Year}";
            }
            return "N/A";
        }

    }
}
