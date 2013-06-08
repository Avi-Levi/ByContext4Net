using System;
using Common;

namespace ByContext
{
    public static class ByContextExtensions
    {
        public static IByContext WithServiceContractRef(this IByContext source, Type contractType)
        {
            return source.WithReference(Subjects.ServiceContract.Name, contractType.FullName);
        }
        public static IByContext WithLogOwnerRef(this IByContext source, Type ownerType)
        {
            return source.WithReference(Subjects.LogOwner.Name, ownerType.FullName);
        }
        public static IByContext WithServiceRef(this IByContext source, Type serviceType)
        {
            return source.WithReference(Subjects.Service.Name, serviceType.FullName);
        }
    }
}
