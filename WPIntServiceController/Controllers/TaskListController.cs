using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WPIntServiceController.Models;
using WPIntServiceController.Util.Sort;
using WPIntServiceController.Util.Manager;
using WPIntServiceController.Util;


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
            ViewBag.CurrentService = _wpIntServiceManager.GetServiceName(_schedulerManager.GetWPIntService());
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            if (infoResponse == null)
            {
                return View("Error");
            }
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            return View("Index", infoResponse);
        }

        [HttpPost]
        public ActionResult Resume(SchedulerInfo taskData)
        {
            _schedulerManager.SetWPIntService(getCurrentService());
            _schedulerManager.ResumeTask(taskData.TaskName, taskData.SchedulerName);
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            return PartialView("TableView", infoResponse);

        }

        [HttpPost]
        public ActionResult Pause(SchedulerInfo taskData)
        {
            _schedulerManager.SetWPIntService(getCurrentService());
            _schedulerManager.SuspendTask(taskData.TaskName, taskData.SchedulerName);
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            return PartialView("TableView", infoResponse);
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

       // [Route("/FilterByTaskName")]
        [HttpPost]
        public ActionResult FilterByTaskName(string taskName)
        {
            _schedulerManager.SetWPIntService(getCurrentService());
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            if (taskName != null && taskName != "")
            {
                List<TaskHandlerInfo> taskHandlerInfos = new List<TaskHandlerInfo>();
                foreach (TaskHandlerInfo taskHandlerInfo in infoResponse.TasksInfos)
                {
                    TaskHandlerInfo newTaskHandlerInfo = new TaskHandlerInfo();
                    newTaskHandlerInfo.Name = taskHandlerInfo.Name;
                    newTaskHandlerInfo.NearTaskScheduledTime = taskHandlerInfo.NearTaskScheduledTime;
                    newTaskHandlerInfo.Type = taskHandlerInfo.Type;
                    foreach (TaskInfo taskInfo in taskHandlerInfo.TaskInfos)
                    {
                        if (taskInfo.Name.Contains(taskName))
                        {
                            newTaskHandlerInfo.TaskInfos.Add(taskInfo);
                        }
                    }
                    if (newTaskHandlerInfo.TaskInfos.Count > 0)
                    {
                        taskHandlerInfos.Add(newTaskHandlerInfo);
                    }

                }
                infoResponse.TasksInfos = taskHandlerInfos;
            }
            return PartialView("TableView", infoResponse);
        }

        [HttpPost]
        public ActionResult ChangeWPIntService(string name)
        {
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            HttpContext.Response.Cookies["service"].Value = name;
            _schedulerManager.SetWPIntService(_wpIntServiceManager.GetService(name));
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            ViewBag.CurrentService = name;
            if (infoResponse == null)
            {
                return View("Error");
            }
            infoResponse.TasksInfos = TaskListSort.SortByName(infoResponse.TasksInfos);
            return Redirect("/");

        }
    }
}
