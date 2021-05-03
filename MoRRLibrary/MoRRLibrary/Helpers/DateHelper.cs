using MD.PersianDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Helpers
{
    public class DateHelper
    {
        public static string ChangeDateType(DateTime? date)
        {
            if (date != null)
            {
                var pDate = new PersianDateTime(date);
                return pDate.ToShortDateString();
            }
            return date.ToString();
        }
        public static string ToEnDate(string FaDate)
        {
            if (!string.IsNullOrEmpty(FaDate))
            {
                int year = Convert.ToInt32(FaDate.Substring(0, 4));
                int month = Convert.ToInt32(FaDate.Substring(5, 2));
                int day = Convert.ToInt32(FaDate.Substring(8, 2));
                DateTime EnDate = new DateTime(year, month, day, new System.Globalization.PersianCalendar());
                return EnDate.ToString("yyyy/MM/dd");
            }
            return string.Empty;
        }
        public static string PersionDateNow()
        {
            var dateNow = PersianDateTime.Today.ToString("yyyy/MM/dd");
            
            return dateNow;
        }
    }
}