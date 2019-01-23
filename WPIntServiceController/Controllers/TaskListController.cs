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
            ViewBag.Title = TASK_LIST_CONTROLLER_TITLE;
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            _schedulerManager.SetWPIntService(GetCurrentService());
            ViewBag.CurrentService = _wpIntServiceManager.GetServiceName(_schedulerManager.GetWPIntService());
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse = TaskListSort.SortByName(infoResponse);
            return GetView("Index", infoResponse);
        }

        [HttpPost]
        public ActionResult Resume(SchedulerInfo taskData)
        {
            _schedulerManager.SetWPIntService(GetCurrentService());
            _schedulerManager.ResumeTask(taskData.TaskName, taskData.SchedulerName);
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse = TaskListSort.SortByName(infoResponse);
            return GetPartialView("TableView", infoResponse);
        }

        [HttpPost]
        public ActionResult Pause(SchedulerInfo taskData)
        {
            _schedulerManager.SetWPIntService(GetCurrentService());
            _schedulerManager.SuspendTask(taskData.TaskName, taskData.SchedulerName);
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse = TaskListSort.SortByName(infoResponse);
            return GetPartialView("TableView", infoResponse);
        }

        [HttpPost]
        public ActionResult SortTaskList(string typeSort)
        {
            _schedulerManager.SetWPIntService(GetCurrentService());
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            switch (typeSort)
            {
                case "byName":
                    infoResponse = TaskListSort.SortByName(infoResponse);
                    return GetPartialView("TableView", infoResponse);
                case "byTime":
                    infoResponse = TaskListSort.SortByTime(infoResponse);
                    return GetPartialView("TableView", infoResponse);
                default:
                    return GetPartialView("TableView", infoResponse);
            }
        }

        [HttpPost]
        public ActionResult FilterByTaskName(string taskName)
        {
            _schedulerManager.SetWPIntService(GetCurrentService());
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            infoResponse = TaskListSort.SortByName(infoResponse);
            infoResponse.TasksInfos = FilterTasksByName(taskName, infoResponse);
            /*if (taskName != null && taskName != "")
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
            }*/
            return GetPartialView("TableView", infoResponse);
        }

        private List<TaskHandlerInfo> FilterTasksByName(string taskName, GetInfoResponse infoResponse)
        {
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
            return infoResponse.TasksInfos;
        }

        [HttpPost]
        public ActionResult ChangeWPIntService(string name)
        {
            ViewBag.Services = _wpIntServiceManager.GetServices().Keys.ToList();
            HttpContext.Response.Cookies[COOKE_TYPE_NAME].Value = name;
            _schedulerManager.SetWPIntService(_wpIntServiceManager.GetService(name));
            GetInfoResponse infoResponse = _schedulerManager.GetTaskList();
            ViewBag.CurrentService = name;
            if (infoResponse == null)
            {
                return View("Error");
            }
            infoResponse = TaskListSort.SortByName(infoResponse);
            return Redirect("~/");
        }
    }
}
