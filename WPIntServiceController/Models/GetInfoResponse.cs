using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Models
{
    public class GetInfoResponse
    {
        public string Usage;
        public List<TaskHandlerInfo> TasksInfos = new List<TaskHandlerInfo>();
    }
}