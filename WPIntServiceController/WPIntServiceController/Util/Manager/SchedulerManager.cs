using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using WPIntServiceController.Models;
using WPIntServiceController.Util.Manager;

namespace WPIntServiceController.Util
{
    public class SchedulerManager : ISchedulerManager
    {
        private string _urlWPIntService;

        public SchedulerManager(string urlWPIntService)
        {
            _urlWPIntService = urlWPIntService;

        }

        public SchedulerManager()
        {
        }

        public GetInfoResponse GetTaskList()
        {
            if (_urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "/TaskList");
                var response = (HttpWebResponse)request.GetResponse();
                return JsonConvert.DeserializeObject<GetInfoResponse>(getStrFromResponse(response));
            }
            else
            {
                return null;
            }
        }

        public bool SuspendTask(string taskName, string schedulerName)
        {
            if (_urlWPIntService != null)
            {
                WebRequest request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "/?scheduler=" + taskName + "&task=" + schedulerName);
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
                return JsonConvert.DeserializeObject<bool>(getStrFromResponse(response));
            }
            else
            {
                return false;
            }
        }

        public bool ResumeTask(string taskName, string schedulerName)
        {
            if (_urlWPIntService != null)
            {
                WebRequest request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "/?scheduler=" + taskName + "&task=" + schedulerName);
                request.Method = "POST";
                var response = (HttpWebResponse)request.GetResponse();
                return JsonConvert.DeserializeObject<bool>(getStrFromResponse(response));
            }
            else
            {
                return false;
            }
        }

        public Dictionary<string, long> GetStatistics()
        {
            if (_urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "/time/");
                var response = (HttpWebResponse)request.GetResponse();
                return JsonConvert.DeserializeObject<Dictionary<string, long>>(getStrFromResponse(response));
            }
            else
            {
                return null;
            }
        }

        public void ResetStatistics()
        {
            if (_urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "/time/");
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
            }
        }

        public void ResetStatistics(string taskName)
        {
            if (_urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "/time/?task=" + taskName);
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
            }
        }

        public void SetWPIntService(string url)
        {
            _urlWPIntService = url;
        }

        public string GetWPIntService()
        {
            return _urlWPIntService;
        }

        private string getStrFromResponse(WebResponse response)
        {
            string responseStr;
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseStr = streamReader.ReadToEnd();
                streamReader.Close();
            }
            return responseStr;
        }

    }
}