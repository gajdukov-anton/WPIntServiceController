using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                    Uri uri = new Uri(_urlWPIntService + WebConfigurationManager.AppSettings["UrlPrefixForTaskList"]);
                    //Uri uri = _urlWPIntService;
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
                //WebRequest request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "api/test/Delete?scheduler=" + taskName + "&task=" + schedulerName);
                WebRequest request = ( HttpWebRequest ) WebRequest.Create( $"{_urlWPIntService}?scheduler={schedulerName}&task={taskName}" );
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
                //WebRequest request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "api/test?scheduler=" + taskName + "&task=" + schedulerName);
                WebRequest request = (HttpWebRequest)WebRequest.Create( $"{_urlWPIntService}?scheduler={schedulerName}&task={taskName}" );
                request.ContentLength = 0;
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
               var request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "api/test/time/"); 
               // var request = (HttpWebRequest)WebRequest.Create($"{_urlWPIntService}time/");
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
                //var request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "api/test/time/");
                var request = (HttpWebRequest)WebRequest.Create($"{_urlWPIntService}time/");
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
            }
        }

        public void ResetStatistics(string taskName)
        {
            if (_urlWPIntService != null)
            {
                var request = (HttpWebRequest)WebRequest.Create(_urlWPIntService + "api/test/time/?task=" + taskName);
                //var request = (HttpWebRequest)WebRequest.Create($"{_urlWPIntService}time/?task={taskName}");
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
            }
        }

        public void SetWPIntService(Uri url)
        {
            _urlWPIntService = url;
        }

        public Uri GetWPIntService()
        {
            return _urlWPIntService;
        }

        /*private Uri createUriForWebRequest(string method, string task, string scheduler)
        {
            string strUri = method;
            if (method.Length != 0 && strUri[strUri.Length] != '/')
                strUri += "/";
            if (scheduler.Equals("") && task.Equals(""))
            {
                return new Uri(strUri);
            } else if
        }*/

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