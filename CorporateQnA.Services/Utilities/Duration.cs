using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Utilities
{
    static class Duration
    {
        public static string getValue(this DateTime dateTime)
        {
            string duration ="";
            TimeSpan ts = DateTime.Now.Subtract(dateTime);
            if (ts.TotalDays >= 1)
            {
                duration = $"{Math.Floor(ts.TotalDays)} days ago";
            }
            else if (ts.TotalHours >= 2)
            {
                duration = $"{Math.Floor(ts.TotalHours)} hrs ago";
            }
            else if (ts.TotalHours >= 1)
            {
                duration = "1 hr ago";
            }
            else if (ts.TotalMinutes >= 2)
            {
                duration = $"{Math.Floor(ts.TotalMinutes)} min ago";
            }
            else
                duration = "Few seconds ago";
            return duration;
        }
    }
}
