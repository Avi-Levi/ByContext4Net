using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Impl
{
    public class ConfigurationDataRepository : IConfigurationDataRepository
    {
        public ConfigurationDataRepository()
        {
            this.Sections = new List<Section>();
        }
        public IList<Section> Sections{get;private set;}
    }
}
