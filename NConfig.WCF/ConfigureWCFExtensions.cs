using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NConfig.WCF;

namespace NConfig
{
    public static class ConfigureWCFExtensions
    {
        public static Configure AddFromRemoteWCFService(this Configure source)
        {
            IConfigurationDataService channel = new ChannelFactory<IConfigurationDataService>("ConfigurationData").CreateChannel();

            string data = channel.GetConfigurationData();

            source.AddFromRawXml(data);

            return source;
        }
    }
}
