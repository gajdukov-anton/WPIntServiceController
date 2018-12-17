using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WPIntServiceController.Models;

namespace WPIntServiceController.Util.Sort
{
    public class TaskListSort
    {
        public static List<TaskHandlerInfo> SortByName(List<TaskHandlerInfo> taskHandlerInfos)
        {
            foreach (TaskHandlerInfo taskHandlerInfo in taskHandlerInfos)
            {
                var sortedTaskInfos = from taskInfo in taskHandlerInfo.TaskInfos
                                      orderby taskInfo.Name
                                      select taskInfo;
                taskHandlerInfo.TaskInfos = sortedTaskInfos.ToList();
            }
            var sortedTaskHandlerInfos = from taskHandlerInfo in taskHandlerInfos
                                         orderby taskHandlerInfo.Name
                                         select taskHandlerInfo;
            return sortedTaskHandlerInfos.ToList();
        }

        public static List<TaskHandlerInfo> SortByTime(List<TaskHandlerInfo> taskHandlerInfos)
        {
            foreach (TaskHandlerInfo taskHandlerInfo in taskHandlerInfos)
            {
                var sortedTaskInfos = from taskInfo in taskHandlerInfo.TaskInfos
                                      orderby taskInfo.ScheduledTimeFormatted
                                      select taskInfo;
                taskHandlerInfo.TaskInfos = sortedTaskInfos.ToList();
            }
            var sortedTaskHandlerInfos = from taskHandlerInfo in taskHandlerInfos
                                         orderby taskHandlerInfo.Name
                                         select taskHandlerInfo;
            return sortedTaskHandlerInfos.ToList();
        }
    }
}