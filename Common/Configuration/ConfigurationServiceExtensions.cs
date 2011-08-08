using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace NConfig
{
    public static class ConfigurationServiceExtensions
    {
        public static IConfigurationService WithServiceContractRef(this IConfigurationService source, Type contractType)
        {
            return source.WithReference(ConfigConstants.Subjects.ServiceContract.Name, contractType.FullName);
        }
        public static IConfigurationService WithLogOwnerRef(this IConfigurationService source, Type ownerType)
        {
            return source.WithReference(ConfigConstants.Subjects.LogOwner.Name, ownerType.FullName);
        }
        public static IConfigurationService WithServiceRef(this IConfigurationService source, Type serviceType)
        {
            return source.WithReference(ConfigConstants.Subjects.Service.Name, serviceType.FullName);
        }
    }
}
