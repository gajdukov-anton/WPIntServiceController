﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WPIntServiceController.Models;

namespace WPIntServiceController.Controllers
{
    public class Test2Controller : ApiController
    {
        public IHttpActionResult Get()
        {
            return Json(CreateGetInfoResponse());
        }

        public bool Delete(string taskName, string schedulerName)
        {
            GetInfoResponse getInfoResponse = CreateGetInfoResponse();
            foreach (TaskHandlerInfo taskHandlerInfo in getInfoResponse.TasksInfos)
            {
                if (taskHandlerInfo.Name.Equals(schedulerName))
                {
                    foreach (TaskInfo taskInfo in taskHandlerInfo.TaskInfos)
                    {
                        if (taskInfo.Name.Equals(taskName) && !taskInfo.IsPaused)
                        {
                            taskInfo.IsPaused = true;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool Post(string taskName, string schedulerName)
        {
            GetInfoResponse getInfoResponse = CreateGetInfoResponse();
            foreach (TaskHandlerInfo taskHandlerInfo in getInfoResponse.TasksInfos)
            {
                if (taskHandlerInfo.Name.Equals(schedulerName))
                {
                    foreach (TaskInfo taskInfo in taskHandlerInfo.TaskInfos)
                    {
                        if (taskInfo.Name.Equals(taskName) && taskInfo.IsPaused)
                        {
                            taskInfo.IsPaused = false;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        [HttpGet]
        public IHttpActionResult Time()
        {
            Dictionary<string, long> dictionary = CreateStatistics();
            return Json(dictionary);
        }

        [HttpDelete]
        public void ResetAllTime()
        {

        }

        [HttpDelete]
        public void Time(string taskName)
        {

        }

        [HttpGet]
        public IHttpActionResult TaskList()
        {
            GetInfoResponse getInfoResponse = new GetInfoResponse();
            getInfoResponse.TasksInfos = new List<TaskHandlerInfo>();
            getInfoResponse.Usage = "Usage2";
            getInfoResponse.TasksInfos.Add(CreateTestItem("task4", true, new DateTime(2015, 7, 20), "NameQWwd"));
            getInfoResponse.TasksInfos.Add(CreateTestItem("task5", true, new DateTime(2015, 7, 20), "Adqf"));
            getInfoResponse.TasksInfos.Add(CreateTestItem("task22", false, new DateTime(2016, 7, 20), "Name"));
            return Json(getInfoResponse);
        }

        [HttpGet]
        public IHttpActionResult Statistics()
        {
            Dictionary<string, long> dictionary = CreateStatistics();
            return Json(dictionary);
        }

        private GetInfoResponse CreateGetInfoResponse()
        {
            GetInfoResponse getInfoResponse = new GetInfoResponse();
            getInfoResponse.TasksInfos = new List<TaskHandlerInfo>();
            getInfoResponse.Usage = "Usage1";
            getInfoResponse.TasksInfos.Add(CreateTestItem("task4", true, new DateTime(2015, 7, 20), "NameQWwd"));
            getInfoResponse.TasksInfos.Add(CreateTestItem("task5", true, new DateTime(2015, 7, 20), "Adqf"));
            getInfoResponse.TasksInfos.Add(CreateTestItem("task22", false, new DateTime(2016, 7, 20), "Name"));
            return getInfoResponse;
        }

        private TaskHandlerInfo CreateTestItem(string value, bool isPaused, DateTime dateTime, string name)
        {
            TaskHandlerInfo taskHandlerInfo = new TaskHandlerInfo();
            taskHandlerInfo.Name = value;
            taskHandlerInfo.Count = 1;
            taskHandlerInfo.NearTaskScheduledTime = dateTime;
            taskHandlerInfo.Type = TaskHandlerType.Queue;
            taskHandlerInfo.TaskInfos = new List<TaskInfo>();
            taskHandlerInfo.TaskInfos.Add(CreateTaskInfo("10/31/2018 7:18:46 AM", "task12", true));
            taskHandlerInfo.TaskInfos.Add(CreateTaskInfo("10/30/2018 7:56:59 PM", "task22", false));
            taskHandlerInfo.TaskInfos.Add(CreateTaskInfo("10/31/2018 7:15:18 AM", "task12", true));
            return taskHandlerInfo;
        }

        private TaskInfo CreateTaskInfo(string time, string name, bool isPaused)
        {
            TaskInfo taskInfo = new TaskInfo();
            taskInfo.Name = name;
            taskInfo.ScheduledTimeFormatted = time;
            taskInfo.IsPaused = isPaused;
            return taskInfo;
        }

        private Dictionary<string, long> CreateStatistics()
        {
            Dictionary<string, long> dictionary = new Dictionary<string, long>();
            dictionary.Add("svskf", 1231414);
            dictionary.Add("tassgsgsgkASD", 124141);
            dictionary.Add("tsgsgaskAsd", 3256145);
            dictionary.Add("tsgsgsaskZXC", 24154141);
            dictionary.Add("tsgsgsgaskHJK", 18741461);
            return dictionary;
        }
    }
}