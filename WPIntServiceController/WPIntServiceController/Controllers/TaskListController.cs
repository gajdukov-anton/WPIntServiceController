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
using Ninject.Web.Common;
using Ninject;
using System.Web.Routing;
using System.Configuration;
using WPIntServiceController.Util.testConfig;
using WPIntServiceController.Util.WPIntService;
using System.Web.Configuration;

namespace WPIntServiceController.Controllers
{
    public class TaskListController :  BaseController
    {
        public TaskListController(ISchedulerManager schedulerManager, IWPIntServiceManager wPIntServiceManager)
            :base(schedulerManager, wPIntServiceManager)
        {
        }

        public ActionResult Index()
        {
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            _schedulerManager.SetWPIntService(getCurrentService());
            ViewBag.CurrentService = HttpContext.Request.Cookies["service"].Value;
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            return View("Index", infoResponse);
        }

        [HttpPost]
        public string Resume(string taskName)
        {
            WPIntServiceConfigSection section = WebConfigurationManager.GetSection("WPIntServicesSection") as WPIntServiceConfigSection;
         //   StartupFoldersConfigSection section = WebConfigurationManager.GetSection("StartupFolders") as StartupFoldersConfigSection;
            string id = section.Services[0].Url + section.Services[0].Name + section.Services[0].Port;
           // string id = section.FolderItems[0].FolderType;
            return taskName + "resume " + id;
        }

        [HttpPost]
        public string Pause(string taskName)
        {
            return taskName + "pause";
        }

        [HttpPost]
        public ActionResult SortTaskList(string typeSort)
        {

            _schedulerManager.SetWPIntService(getCurrentService());
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
            _schedulerManager.SetWPIntService(getCurrentService());
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
            HttpContext.Response.Cookies["service"].Value = name;
            _schedulerManager.SetWPIntService(_wpIntServiceManager.GetService(name));
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            ViewBag.CurrentService = name;
            return View("Index", infoResponse);
        }
    }
}
