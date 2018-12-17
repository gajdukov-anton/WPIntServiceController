using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using WPIntServiceController.Models;

namespace WPIntServiceController.Util
{
    public class SchedulerManager
    {
        private string urlWPIntService;

        public SchedulerManager()
        {

        }

        public GetInfoResponse getTaskList()
        {
            if (urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(urlWPIntService + "/TaskList");
                var response = (HttpWebResponse)request.GetResponse();
                var responseStr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<GetInfoResponse>(responseStr);
            }
            else
            {
                return null;
            }
        }

        public bool SuspendTask(string taskName, string schedulerName)
        {
            if (urlWPIntService != null)
            {
                WebRequest request = (HttpWebRequest)WebRequest.Create(urlWPIntService + "/?scheduler=" + taskName + "&task=" + schedulerName);
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
                var responseStr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<bool>(responseStr);
            }
            else
            {
                return false;
            }
        }

        public bool ResumeTask(string taskName, string schedulerName)
        {
            if (urlWPIntService != null)
            {
                WebRequest request = (HttpWebRequest)WebRequest.Create(urlWPIntService + "/?scheduler=" + taskName + "&task=" + schedulerName);
                request.Method = "POST";
                var response = (HttpWebResponse)request.GetResponse();
                var responseStr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<bool>(responseStr);
            }
            else
            {
                return false;
            }
        }

        public Dictionary<string, long> GetStatistics()
        {
            if (urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(urlWPIntService + "/time/");
                var response = (HttpWebResponse)request.GetResponse();
                var responseStr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, long>>(responseStr);
            }
            else
            {
                return null;
            }
        }
        
        public void ResetStatistics()
        {
            if (urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(urlWPIntService + "/time/");
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
            }
        }

        public void ResetStatistics(string taskName)
        {
            if (urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(urlWPIntService + "/time/?task=" + taskName);
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
            }
        }

        public void SetWPIntService(string url)
        {
            urlWPIntService = url;
        }

        public string GetWPIntService()
        {
            return urlWPIntService;
        }
    }
}