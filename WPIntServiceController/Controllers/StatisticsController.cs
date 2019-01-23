using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WPIntServiceController.Util.Sort;
using WPIntServiceController.Util.Manager;
using WPIntServiceController.Util;


namespace WPIntServiceController.Controllers
{
    public class StatisticsController : BaseController
    {
        public StatisticsController(ISchedulerManager schedulerManager, IWPIntServiceManager wPIntServiceManager)
            : base(schedulerManager, wPIntServiceManager)
        {

        }

        public ActionResult Index()
        {
            ViewBag.Title = STATISTICS_CONTROLLER_TITLE;
            _schedulerManager.SetWPIntService(GetCurrentService());
            Dictionary<string, long> statistics = _schedulerManager.GetStatistics();
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            ViewBag.CurrentService = HttpContext.Request.Cookies[COOKE_TYPE_NAME].Value;
            return GetView("Index", StatisticSort.SortName(statistics));
        }

        [HttpPost]
        public ActionResult ResetStatisticsTime(string taskName)
        {
            _schedulerManager.SetWPIntService(GetCurrentService());
            if (_schedulerManager.ResetStatistics(taskName))
                return PartialView("TaskTimeView", "0");
            else
                return PartialView("ErrorPartialView");

        }

        [HttpPost]
        public ActionResult SortStatistics(string typeSort)
        {
            _schedulerManager.SetWPIntService(GetCurrentService());
            Dictionary<string, long> statistics = _schedulerManager.GetStatistics();
            switch (typeSort)
            {
                case "byName":
                    return GetPartialView("TableView", StatisticSort.SortName(statistics));
                case "byTime":
                    return GetPartialView("TableView", StatisticSort.SortByTime(statistics));
                default:
                    return GetPartialView("TableView", statistics);
            }
        }
    }
}
