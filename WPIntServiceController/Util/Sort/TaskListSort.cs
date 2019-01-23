using System.Collections.Generic;
using System.Linq;
using WPIntServiceController.Models;

namespace WPIntServiceController.Util.Sort
{
    public class TaskListSort
    {
        public static GetInfoResponse SortByName(GetInfoResponse getInfoResponse)
        {
            if (getInfoResponse != null)
            {
                foreach (TaskHandlerInfo taskHandlerInfo in getInfoResponse.TasksInfos)
                {
                    var sortedTaskInfos = from taskInfo in taskHandlerInfo.TaskInfos
                                          orderby taskInfo.Name
                                          select taskInfo;
                    taskHandlerInfo.TaskInfos = sortedTaskInfos.ToList();
                }
                return getInfoResponse;
            }
            else
            {
                return null;
            }
        }

        public static GetInfoResponse SortByTime(GetInfoResponse getInfoResponse)
        {
            if (getInfoResponse != null)
            {
                foreach (TaskHandlerInfo taskHandlerInfo in getInfoResponse.TasksInfos)
                {
                    var sortedTaskInfos = from taskInfo in taskHandlerInfo.TaskInfos
                                          orderby taskInfo.ScheduledTimeFormatted
                                          select taskInfo;
                    taskHandlerInfo.TaskInfos = sortedTaskInfos.ToList();
                }
                return getInfoResponse;
            }
            else
            {
                return null;
            }
        }
    }
}