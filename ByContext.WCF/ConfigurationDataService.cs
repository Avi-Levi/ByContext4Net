using System.Linq;
using ByContext.Model;
using ByContext.XML;

namespace ByContext.WCF
{
    public class ConfigurationDataService : IConfigurationDataService 
    {
        public Section[] GetConfigurationData()
        {
            return new XmlLoader().LoadFile("Configuration.xml").ToArray();
        }
    }
}
