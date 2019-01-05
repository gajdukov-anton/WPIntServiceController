using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WPIntServiceController.Util.Manager;
using WPIntServiceController.Util.WPIntService;

namespace WPIntServiceController.Util
{
    public class WPIntServiceManager : IWPIntServiceManager
    {
        private Dictionary<string, Uri> _services;

        public WPIntServiceManager()
        {
            _services = GetWPIntServicesFromConfigFile();
        }

        public Dictionary<string, Uri> GetWPIntServicesFromConfigFile()
        {
            Dictionary<string, Uri> newServices = new Dictionary<string, Uri>();
            WPIntServiceConfigSection section = WebConfigurationManager.GetSection("WPIntServicesSection") as WPIntServiceConfigSection;
            foreach (WPIntServiceElement item in section.Services )
            {
                UriBuilder serviceUri = new UriBuilder(item.Protocol, item.Url, Convert.ToInt32(item.Port));
                newServices.Add(item.Name, serviceUri.Uri);
            }

            return newServices;
        }

        public Uri GetFirstService()
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

        public Uri GetService(string serviceName)
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

        public Dictionary<string, Uri> GetServices()
        {
            return _services;
        }

        public List<string> GetServicesName()
        {
            return _services.Keys.ToList();
        }

        bool IsExist(Uri serviceUri)
        {
            return _services.
        }

    }
}