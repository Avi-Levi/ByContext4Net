using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NConfig.WCF;
using NConfig.Impl;

namespace NConfig
{
    public static class ConfigureWCFExtensions
    {
        public static Configure AddFromRemoteWCFService(this Configure source)
        {
            source.AddConfigurationDataProvider(new ConvertFromSectionProvider(() =>
                {
                    IConfigurationDataService channel = 
                        new ChannelFactory<IConfigurationDataService>("ConfigurationData").CreateChannel();
                    return channel.GetConfigurationData();
                },source)
                );

            return source;
        }
    }
}
