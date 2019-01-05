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
using WPIntServiceController.Util;
using Ninject;

namespace WPIntServiceController.Controllers
{
    public class StatisticsController : BaseController
    {
        public StatisticsController(ISchedulerManager schedulerManager, IWPIntServiceManager wPIntServiceManager)
            :base(schedulerManager, wPIntServiceManager)
        {
           
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            _schedulerManager.SetWPIntService(getCurrentService());
            Dictionary<string, long> statistics = _schedulerManager.GetStatistics();
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            ViewBag.CurrentService = HttpContext.Request.Cookies["service"].Value;
            return View(StatisticSort.SortName(statistics));
        }

        [HttpPost]
        public ActionResult ResetStatisticsTime(string taskName)
        {
            _schedulerManager.SetWPIntService(getCurrentService());
            _schedulerManager.ResetStatistics(taskName);
            return PartialView("TaskTimeView", "0");
        }
       
        [HttpPost]
        public ActionResult SortStatistics(string typeSort)
        {
            _schedulerManager.SetWPIntService(getCurrentService());
            Dictionary<string, long> statistics = _schedulerManager.GetStatistics();
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
