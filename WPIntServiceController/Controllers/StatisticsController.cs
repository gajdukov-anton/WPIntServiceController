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
    public class StatisticsController : Controller
    {
        ISchedulerManager _schedulerManager;
        IWPIntServiceManager _wpIntServiceManager;

        public StatisticsController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<ISchedulerManager>().To<SchedulerManager>();
            ninjectKernel.Bind<IWPIntServiceManager>().To<WPIntServiceManager>();
            _schedulerManager = ninjectKernel.Get<ISchedulerManager>();
            _wpIntServiceManager = ninjectKernel.Get<IWPIntServiceManager>();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            // SchedulerManager schedulerManager = new SchedulerManager(getServiceName());
            _schedulerManager.SetWPIntService(getCurrentService());
            Dictionary<string, long> statistics = _schedulerManager.GetStatistics();
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            ViewBag.CurrentService = HttpContext.Request.Cookies["service"].Value;
            return View(StatisticSort.SortName(statistics));
        }

        [HttpPost]
        public ActionResult ResetStatisticsTime(string taskName)
        {
            //SchedulerManager schedulerManager = new SchedulerManager(getServiceName());
            _schedulerManager.SetWPIntService(getCurrentService());
            Dictionary<string, long> statistics = _schedulerManager.GetStatistics();
            statistics[taskName] = 0;
            return PartialView("TaskTimeView", (long) 0);
        }
       
        [HttpPost]
        public ActionResult SortStatistics(string typeSort)
        {
            // SchedulerManager schedulerManager = new SchedulerManager(getServiceName());
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

        private string getCurrentService()
        {
            if (HttpContext.Request.Cookies["service"] == null)
            {
                string service = _wpIntServiceManager.GetFirstService();
                HttpContext.Response.Cookies["service"].Value = _wpIntServiceManager.GetServicesName()[0];
                return service;
            }
            else
            {
                return _wpIntServiceManager.GetService(HttpContext.Request.Cookies["service"].Value);
            }
        }
    }
}
