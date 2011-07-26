using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace NConfig.WCF
{
    [ServiceContract]
    public interface IConfigurationDataService
    {
        [OperationContract]
        string GetConfigurationData();
    }
}
