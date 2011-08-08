using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NConfig.Configuration;

namespace NConfig.WCF
{
    [ServiceContract]
    public interface IConfigurationDataService
    {
        [OperationContract]
        Section[] GetConfigurationData();
    }
}
