using System.ServiceModel;
using ByContext.Model;

namespace ByContext.WCF
{
    [ServiceContract]
    public interface IConfigurationDataService
    {
        [OperationContract]
        Section[] GetConfigurationData();
    }
}
