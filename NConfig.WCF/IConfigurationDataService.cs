using System.ServiceModel;
using NConfig.Model;

namespace NConfig.WCF
{
    [ServiceContract]
    public interface IConfigurationDataService
    {
        [OperationContract]
        Section[] GetConfigurationData();
    }
}
