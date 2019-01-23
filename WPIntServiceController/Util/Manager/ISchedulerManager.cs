using System;
using System.Collections.Generic;
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
        bool ResetStatistics(string taskName);
        void SetWPIntService(Uri url);
        Uri GetWPIntService();
    }
}
