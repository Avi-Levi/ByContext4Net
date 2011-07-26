using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace NConfig
{
    public static class ConfigurationServiceExtensions
    {
        public static IConfigurationService WithServiceContractRef(this IConfigurationService source, string value)
        {
            return source.WithReference(ConfigConstants.Subjects.ServiceContract.Name, value);
        }
        public static IConfigurationService WithLogOwnerRef(this IConfigurationService source, string value)
        {
            return source.WithReference(ConfigConstants.Subjects.LogOwner.Name, value);
        }
    }
}
