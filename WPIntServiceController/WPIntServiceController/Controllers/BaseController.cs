using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WPIntServiceController.Util.Manager;

namespace WPIntServiceController.Util
{
    public class BaseController : Controller
    {
        protected ISchedulerManager _schedulerManager;
        protected IWPIntServiceManager _wpIntServiceManager;

        public BaseController(ISchedulerManager schedulerManager, IWPIntServiceManager wPIntServiceManager)
        {
            _schedulerManager = schedulerManager;
            _wpIntServiceManager = wPIntServiceManager;
        }


        protected string getCurrentService()
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