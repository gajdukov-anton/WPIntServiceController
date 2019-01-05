using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPIntServiceController.Util.Manager
{
    public interface IWPIntServiceManager
    {
        Dictionary<string, Uri> GetWPIntServicesFromConfigFile();
        Uri GetFirstService();
        Uri GetService(string serviceName);
        Dictionary<string, Uri> GetServices();
        List<string> GetServicesName();
        bool IsExist(Uri serviceUri);
    }
}
