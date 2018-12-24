using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Util.WPIntService
{
    public class WPIntServiceConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Service")]
        public WPIntServiceCollection Services
        {
            get { return ((WPIntServiceCollection)(base["Service"])); }
        }
    }
}