using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WPIntServiceController.Util.Sort;
using WPIntServiceController.Util.Manager;
namespace WPIntServiceController.Controllers
{
    public class StatisticsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Dictionary<string, long> statistics = ManagerCollector.SchedulerManager.GetStatistics();
            ViewBag.Schedulers = ManagerCollector.WPIntServiceManager.getServices().Keys.ToList();

            return View(StatisticSort.SortName(statistics));
        }

        [HttpPost]
        public ActionResult ResetStatisticsTime(string taskName)
        {
            Dictionary<string, long> statistics = ManagerCollector.SchedulerManager.GetStatistics();
            statistics[taskName] = 0;
            return PartialView("TaskTimeView", (long) 0);
        }
       
        [HttpPost]
        public ActionResult SortStatistics(string typeSort)
        {
            Dictionary<string, long> statistics = ManagerCollector.SchedulerManager.GetStatistics();
            switch (typeSort)
            {
                case "byName":
                    return PartialView("TableView", StatisticSort.SortName(statistics));
                case "byTime":
                    return PartialView("TableView", StatisticSort.SortByTime(statistics));
                default:
                    return PartialView("TableView", statistics);
            }
        }
    }
}
