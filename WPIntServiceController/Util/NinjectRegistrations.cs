using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Ninject.Web.Common;
using WPIntServiceController.Util.Manager;

namespace WPIntServiceController.Util
{
    public class NinjectRegistrations : NinjectModule
    {

        public override void Load()
        {
            Bind<ISchedulerManager>().To<SchedulerManager>().InRequestScope();
            Bind<IWPIntServiceManager>().To<WPIntServiceManager>().InRequestScope();
        }
    }
}