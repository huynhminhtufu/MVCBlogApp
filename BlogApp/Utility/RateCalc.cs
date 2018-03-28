using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Utility
{
    public class RateCalc
    {
        public static string RatingCalc(int RatingPoint, int NumOfRating)
        {
            if (NumOfRating == 0)
            {
                return "0";
            }
            double rate = RatingPoint * 1.0 / NumOfRating;
            return rate.ToString("F1");
        }
    }
}