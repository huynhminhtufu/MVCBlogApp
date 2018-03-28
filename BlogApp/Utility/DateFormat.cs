using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Utility
{
    public class DateFormat
    {
        public static string FormatDateTime(string sample)
        {
            string[] date = sample.Split('/', ' ');
            int month = int.Parse(date[0]);
            int day = int.Parse(date[1]);
            int year = int.Parse(date[2]) + 2000;
            DateTime newDate = new DateTime(year, month, day);
            return newDate.ToString("dd/MM/yyyy");
        }
    }
}