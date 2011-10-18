using System;
using Common;

namespace NConfig
{
    public static class ConfigurationServiceExtensions
    {
        public static IConfigurationService WithServiceContractRef(this IConfigurationService source, Type contractType)
        {
            return source.WithReference(Subjects.ServiceContract.Name, contractType.FullName);
        }
        public static IConfigurationService WithLogOwnerRef(this IConfigurationService source, Type ownerType)
        {
            return source.WithReference(Subjects.LogOwner.Name, ownerType.FullName);
        }
        public static IConfigurationService WithServiceRef(this IConfigurationService source, Type serviceType)
        {
            return source.WithReference(Subjects.Service.Name, serviceType.FullName);
        }
    }
}
