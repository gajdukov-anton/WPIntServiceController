using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WPIntServiceController.Util.Manager;

namespace WPIntServiceController.Util
{
    public class WPIntServiceManager : IWPIntServiceManager
    {
        private Dictionary<string, string> _services;

        public WPIntServiceManager()
        {
            _services = GetWPIntServicesFromConfigFile();
        }

        public Dictionary<string, string> GetWPIntServicesFromConfigFile()
        {
            Dictionary<string, string> newServices = new Dictionary<string, string>();
            newServices.Add("Service1", "http://localhost:49719/api/test");
            newServices.Add("Service2", "http://localhost:49719/api/test2");

            return newServices;
        }

        public string GetFirstService()
        {
            if (_services != null)
            {
                return _services[_services.Keys.ToArray()[0]];
            }
            else
            {
                return null;
            }
        }

        public string GetService(string serviceName)
        {
            if (serviceName != null && _services.Keys.Contains(serviceName))
            {
                return _services[serviceName];
            }
            else
            {
                return null;
            }
        }

        public Dictionary<string, string> GetServices()
        {
            return _services;
        }

        public List<string> GetServicesName()
        {
            return _services.Keys.ToList();
        }
    }
}