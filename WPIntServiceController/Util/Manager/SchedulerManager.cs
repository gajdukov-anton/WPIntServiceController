using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using WPIntServiceController.Models;
using WPIntServiceController.Util.Manager;
using System.Web.Configuration;

namespace WPIntServiceController.Util
{
    public class SchedulerManager : ISchedulerManager
    {
        private Uri _urlWPIntService;

        public SchedulerManager(Uri urlWPIntService)
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
                try
                {
                    Uri uri = createUriForWebRequest("", "", "");
                    var request = (HttpWebRequest)WebRequest.Create(uri);
                    var response = (HttpWebResponse)request.GetResponse();
                    return JsonConvert.DeserializeObject<GetInfoResponse>(getStrFromResponse(response));
                }
                catch
                {
                    return null;
                }
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
                try
                {
                    WebRequest request = (HttpWebRequest)WebRequest.Create(createUriForWebRequest("", schedulerName, taskName));
                    request.Method = "DELETE";
                    var response = (HttpWebResponse)request.GetResponse();
                    return JsonConvert.DeserializeObject<bool>(getStrFromResponse(response));
                }
                catch
                {
                    return false;
                }
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
                try
                {
                    WebRequest request = (HttpWebRequest)WebRequest.Create(createUriForWebRequest("", schedulerName, taskName));
                    request.ContentLength = 0;
                    request.Method = "POST";
                    var response = (HttpWebResponse)request.GetResponse();
                    return JsonConvert.DeserializeObject<bool>(getStrFromResponse(response));
                }
                catch
                {
                    return false;
                }
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
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(createUriForWebRequest(WebConfigurationManager.AppSettings["StatisticsUrlPrefix"], "", ""));
                    var response = (HttpWebResponse)request.GetResponse();
                    return JsonConvert.DeserializeObject<Dictionary<string, long>>(getStrFromResponse(response));
                }
                catch
                {
                    return null;
                }
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
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(createUriForWebRequest(WebConfigurationManager.AppSettings["StatisticsUrlPrefix"], "", ""));
                    request.Method = "DELETE";
                    var response = (HttpWebResponse)request.GetResponse();
                }
                catch
                { }
            }
        }

        public bool ResetStatistics(string taskName)
        {
            if (_urlWPIntService != null)
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(createUriForWebRequest(WebConfigurationManager.AppSettings["StatisticsUrlPrefix"], "", taskName));
                    request.Method = "DELETE";
                    var response = (HttpWebResponse)request.GetResponse();
                    return true;
                }
                catch
                { }
            }
            return false;
        }

        public void SetWPIntService(Uri url)
        {
            _urlWPIntService = url;
        }

        public Uri GetWPIntService()
        {
            return _urlWPIntService;
        }

        private Uri createUriForWebRequest(string prefix, string schedulerName, string taskName)
        {
            if (!schedulerName.Equals("") && !taskName.Equals(""))
            {
                Uri uri = new Uri($"{_urlWPIntService}{prefix}?scheduler={schedulerName}&task={taskName}");
                return uri;
            }
            if (!taskName.Equals(""))
            {
                Uri uri = new Uri($"{_urlWPIntService}?task={taskName}");
                return uri;
            }
            if (schedulerName.Equals("") && taskName.Equals(""))
            {
                Uri uri = new Uri($"{_urlWPIntService}{prefix}");
                return uri;
            }
            return null;
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