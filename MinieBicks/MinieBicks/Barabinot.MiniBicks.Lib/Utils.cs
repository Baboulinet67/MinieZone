using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Barabinot.MiniBicks.Lib
{
    public class Utils
    {
        //Renvoie le nombre de jours ouvrés entre deux dates
        public static int GetNumberOfWorkingDays(DateTime start, DateTime stop)
        {
            int days = 0;
            while (start <= stop)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++days;
                }
                start = start.AddDays(1);
            }
            return days;
        }


        private static readonly Regex _regex = new Regex("[^0-9,-]+"); //regex that matches disallowed text
        public static string IsTextAllowed(string text)
        {
            return _regex.Replace(text, "");
        }

    }
}
