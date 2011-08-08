using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using NConfig.Configuration;
using NConfig.XML;

namespace NConfig.WCF
{
    public class ConfigurationDataService : IConfigurationDataService 
    {
        public Section[] GetConfigurationData()
        {
            return new XmlLoader().LoadFile("Configuration.xml").ToArray();
        }
    }
}
