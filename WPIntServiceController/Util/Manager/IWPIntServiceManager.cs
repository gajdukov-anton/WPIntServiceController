using System;
using System.Collections.Generic;

namespace WPIntServiceController.Util.Manager
{
    public interface IWPIntServiceManager
    {
        Dictionary<string, Uri> GetWPIntServicesFromConfigFile();
        Uri GetFirstService();
        Uri GetService(string serviceName);
        Dictionary<string, Uri> GetServices();
        List<string> GetServicesName();
        string GetServiceName(Uri uri);
    }
}
