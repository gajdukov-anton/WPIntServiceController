using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Util.Manager
{
    public class ManagerCollector
    {
        public static WPIntServiceManager WPIntServiceManager { private set; get; } = new WPIntServiceManager();

        public static SchedulerManager SchedulerManager { private set; get; } = new SchedulerManager();

    }
}