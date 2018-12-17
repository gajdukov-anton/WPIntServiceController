using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Models
{
    public class TaskInfo
    {
        public string Name;
        public string ScheduledTimeFormatted;
        public bool IsPaused;

        public override string ToString()
        {
            return Name + " " + ScheduledTimeFormatted + " " + IsPaused.ToString(); 
        }
    }
}