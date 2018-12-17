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


namespace WPIntServiceController.Controllers
{
    public class TaskListController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Schedulers = ManagerCollector.WPIntServiceManager.getServices().Keys.ToList();
            if (ManagerCollector.SchedulerManager.GetWPIntService() == null)
                ManagerCollector.SchedulerManager.SetWPIntService(ManagerCollector.WPIntServiceManager.getFirstService());
            GetInfoResponse infoResponse = ManagerCollector.SchedulerManager.getTaskList();
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            return View("Index", infoResponse);
        }

        [HttpPost]
        public string Resume(string taskName)
        {
            return taskName + "resume";
        }

        [HttpPost]
        public string Pause(string taskName)
        {
            return taskName + "pause";
        }

        [HttpPost]
        public ActionResult SortTaskList(string typeSort)
        {
            GetInfoResponse infoResponse = ManagerCollector.SchedulerManager.getTaskList();
            switch(typeSort)
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
        public ActionResult FilterByTaskName(string schedulerName, string taskName)
        {
            GetInfoResponse infoResponse = ManagerCollector.SchedulerManager.getTaskList();
            List<TaskHandlerInfo> taskHandlerInfos = new List<TaskHandlerInfo>();
            foreach(TaskHandlerInfo taskHandlerInfo in infoResponse.TasksInfos)
            {
                if (taskHandlerInfo.Name.Equals(schedulerName))
                {
                    List<TaskInfo> taskInfos = new List<TaskInfo>();
                    foreach(TaskInfo taskInfo in taskHandlerInfo.TaskInfos)
                    {
                        if (taskInfo.Name.Equals(taskName))
                        {
                            taskInfos.Add(taskInfo);
                        }
                    }
                    taskHandlerInfo.TaskInfos = taskInfos;
                    taskHandlerInfos.Add(taskHandlerInfo);
                }
            }
            infoResponse.TasksInfos = taskHandlerInfos;
            return PartialView("TableView", infoResponse);
        }

        [HttpPost]
        public ActionResult ChangeWPIntService(string name)
        {
            ManagerCollector.SchedulerManager.SetWPIntService(ManagerCollector.WPIntServiceManager.getService(name));
            GetInfoResponse infoResponse = ManagerCollector.SchedulerManager.getTaskList();
            return Index();
            /*if (infoResponse != null)
            {
                ViewBag.Schedulers = ManagerCollector.WPIntServiceManager.getServices().Keys.ToList();
                return View("Index", infoResponse);
            }
            else
            {
                return HttpNotFound();
            }*/
        }
    }
}
