using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPIntServiceController.Models;

namespace WPIntServiceController.Util.Manager
{
    public interface ISchedulerManager
    {
        GetInfoResponse GetTaskList();
        bool SuspendTask(string taskName, string schedulerName);
        bool ResumeTask(string taskName, string schedulerName);
        Dictionary<string, long> GetStatistics();
        void ResetStatistics();
        void ResetStatistics(string taskName);
        void SetWPIntService(Uri url);
    }
}
