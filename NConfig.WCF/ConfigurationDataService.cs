using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace NConfig.WCF
{
    public class ConfigurationDataService : IConfigurationDataService 
    {
        public string GetConfigurationData()
        {
            return File.ReadAllText("Configuration.xml");
        }
    }
}
