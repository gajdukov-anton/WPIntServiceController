using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Models
{
    public class TaskHandlerInfo
    {
        public string Name;
        public int? Count;
        public DateTime? NearTaskScheduledTime;
        public TaskHandlerType Type;
        public List<TaskInfo> TaskInfos = new List<TaskInfo>();

        public string GetAllTaskInfo()
        {
            string result = "";
            foreach (var item in TaskInfos)
            {
                result = result + item.ToString() + "\r\n";
            }
            return result;
        }
    }
}