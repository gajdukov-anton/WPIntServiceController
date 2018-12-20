using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Models
{
    public enum TaskHandlerType : byte
    {
        Unknown = 0,
        Queue = 1,
        Scheduler = 2
    }
}