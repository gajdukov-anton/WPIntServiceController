using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WPIntServiceController.Models;
using WPIntServiceController.Util.Sort;
using WPIntServiceController.Util.Manager;
using System.Net.Http.Headers;
using WPIntServiceController.Util;
using Ninject;
using System.Web.Routing;

namespace WPIntServiceController.Controllers
{
    public class TaskListController : Controller
    {
        ISchedulerManager _schedulerManager;
        IWPIntServiceManager _wpIntServiceManager;

        public TaskListController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<ISchedulerManager>().To<SchedulerManager>();
            ninjectKernel.Bind<IWPIntServiceManager>().To<WPIntServiceManager>();
            _schedulerManager = ninjectKernel.Get<ISchedulerManager>();
            _wpIntServiceManager = ninjectKernel.Get<IWPIntServiceManager>();
        }

        public ActionResult Index()
        {
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            _schedulerManager.SetWPIntService(getServiceName());
            ViewBag.CurrentService = HttpContext.Request.Cookies["serviceName"].Value;
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            return View("Index", infoResponse);
        }

        [HttpPost]
        public string Resume(string taskName)
        {
            string id = HttpContext.Request.Cookies["serviceName"].Value;
            return taskName + "resume" + id;
        }

        [HttpPost]
        public string Pause(string taskName)
        {
            return taskName + "pause";
        }

        [HttpPost]
        public ActionResult SortTaskList(string typeSort)
        {
            _schedulerManager.SetWPIntService(getServiceName());
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            switch (typeSort)
            {
                case "byName":
                    infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
                    return PartialView("TableView", infoResponse);
                case "byTime":
                    infoResponse.TasksInfos = TaskListSort.SortByTime(infoResponse.TasksInfos);
                    return PartialView("TableView", infoResponse);
                default:
                    return PartialView("TableView", infoResponse);
            }
        }

        [HttpPost]
        public ActionResult FilterByTaskName(string schedulerName)
        {
            _schedulerManager.SetWPIntService(getServiceName());
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            List<TaskHandlerInfo> taskHandlerInfos = new List<TaskHandlerInfo>();
            foreach (TaskHandlerInfo taskHandlerInfo in infoResponse.TasksInfos)
            {
                if (taskHandlerInfo.Name.Equals(schedulerName))
                {
                    taskHandlerInfos.Add(taskHandlerInfo);
                }
            }
            infoResponse.TasksInfos = taskHandlerInfos;
            return PartialView("TableView", infoResponse);
        }

        [HttpPost]
        public ActionResult ChangeWPIntService(string name)
        {
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            HttpContext.Response.Cookies["serviceName"].Value = name;
            _schedulerManager.SetWPIntService(_wpIntServiceManager.GetService(name));
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            ViewBag.CurrentService = name;
            return View("Index", infoResponse);
        }



        private string getServiceName()
        {
            if (HttpContext.Request.Cookies["serviceName"] == null)
            {
                string service = _wpIntServiceManager.GetFirstService();
                HttpContext.Response.Cookies["serviceName"].Value = service;
                return service;
            }
            else
            {
                return _wpIntServiceManager.GetService(HttpContext.Request.Cookies["serviceName"].Value);
            }
        }
    }
}
