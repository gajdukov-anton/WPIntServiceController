using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WPIntServiceController.Models;

namespace WPIntServiceController.Util.Sort
{
    public class StatisticSort
    {
        public static Dictionary<string, long> SortByTime(Dictionary<string, long> statistics)
        {
            Dictionary<string, long> sortedStatistics = statistics;
            sortedStatistics = sortedStatistics.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            return sortedStatistics;
        }

        public static Dictionary<string, long> SortName(Dictionary<string, long> statistics)
        {
            SortedDictionary<string, long> sortedStatistics = new SortedDictionary<string, long>(statistics);
            Dictionary<string, long> result = sortedStatistics.ToDictionary(x => x.Key, x => x.Value);
            return result;
        }
    }
}