using System;
using System.Web.Mvc;
using WPIntServiceController.Util.Manager;

namespace WPIntServiceController.Util
{
    public class BaseController : Controller
    {
        protected ISchedulerManager _schedulerManager;
        protected IWPIntServiceManager _wpIntServiceManager;
        protected const string COOKE_TYPE_NAME = "service";
        protected const string TASK_LIST_CONTROLLER_TITLE = "Список задач";
        protected const string STATISTICS_CONTROLLER_TITLE = "Статистика";

        public BaseController(ISchedulerManager schedulerManager, IWPIntServiceManager wPIntServiceManager)
        {
            _schedulerManager = schedulerManager;
            _wpIntServiceManager = wPIntServiceManager;
        }


        protected Uri GetCurrentService()
        {
            if (HttpContext.Request.Cookies[COOKE_TYPE_NAME] == null)
            {
                Uri service = _wpIntServiceManager.GetFirstService();
                HttpContext.Response.Cookies[COOKE_TYPE_NAME].Value = _wpIntServiceManager.GetServicesName()[0];
                return service;
            }
            else
            {
                var uri = _wpIntServiceManager.GetService(HttpContext.Request.Cookies[COOKE_TYPE_NAME].Value);
                if (uri != null)
                {
                    return uri;
                }
                uri = _wpIntServiceManager.GetFirstService();
                HttpContext.Response.Cookies[COOKE_TYPE_NAME].Value = _wpIntServiceManager.GetServicesName()[0];
                return uri;
            }
        }

        protected ActionResult GetView<T>(string viewName, T infoResponse)
        {
            if (infoResponse == null)
            {
                return View("Error");
            }
            else
            {
                return View(viewName, infoResponse);
            }
        }

        protected ActionResult GetPartialView<T>(string partialViewName,T infoResponse)
        {
            if (infoResponse == null)
            {
                return PartialView("ErrorPartialView");
            }
            else
            {
                return PartialView(partialViewName, infoResponse);
            }
        }
    }
}