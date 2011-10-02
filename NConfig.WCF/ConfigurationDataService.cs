using System.Linq;
using NConfig.Model;
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
