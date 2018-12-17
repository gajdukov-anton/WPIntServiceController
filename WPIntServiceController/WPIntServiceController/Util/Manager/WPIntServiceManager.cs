using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Util
{
    public class WPIntServiceManager
    {
        private Dictionary<string, string> services;

        public WPIntServiceManager()
        {
            services = getWPIntServicesFromConfigFile();
        }

        private Dictionary<string, string> getWPIntServicesFromConfigFile()
        {
            Dictionary<string, string> newServices = new Dictionary<string, string>();
            newServices.Add("Service1", "http://localhost:49719/api/test");
            newServices.Add("Service2", "http://localhost:49719/api/test2");

            return newServices;
        }

        public string getFirstService()
        {
            if (services != null)
            {
                return services[services.Keys.ToArray()[0]];
            }
            else
            {
                return null;
            }
        }

        public string getService(string serviceName)
        {
            if (serviceName != null && services.Keys.Contains(serviceName))
            {
                return services[serviceName];
            }
            else
            {
                return null;
            }
        }

        public Dictionary<string, string> getServices()
        {
            return services;
        }
    }
}