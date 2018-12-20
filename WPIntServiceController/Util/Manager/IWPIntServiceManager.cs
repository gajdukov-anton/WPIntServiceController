﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPIntServiceController.Util.Manager
{
    interface IWPIntServiceManager
    {
        Dictionary<string, string> GetWPIntServicesFromConfigFile();
        string GetFirstService();
        string GetService(string serviceName);
        Dictionary<string, string> GetServices();
    }
}
