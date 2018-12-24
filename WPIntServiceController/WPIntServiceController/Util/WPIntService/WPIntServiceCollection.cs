using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WPIntServiceController.Util.WPIntService
{
    [ConfigurationCollection(typeof(WPIntServiceElement))]
    public class WPIntServiceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new WPIntServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WPIntServiceElement)(element)).Url;
        }

        public WPIntServiceElement this[int idx]
        {
            get { return (WPIntServiceElement)BaseGet(idx); }
        }
    }
}