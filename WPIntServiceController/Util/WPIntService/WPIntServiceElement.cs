using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Util.WPIntService
{
    public class WPIntServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("url", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Url
        {
            get { return ((string)(base["url"])); }
        }

        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["name"])); }
        }

        [ConfigurationProperty("port", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Port
        {
            get { return ((string)(base["port"])); }
        }

        [ConfigurationProperty("protocol", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Protocol
        {
            get { return ((string)(base["protocol"])); }
        }
    }
}