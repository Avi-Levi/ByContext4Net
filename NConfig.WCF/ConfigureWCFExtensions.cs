using System.ServiceModel;
using NConfig.ConfigurationDataProviders;

namespace NConfig.WCF
{
    public static class ConfigureWCFExtensions
    {
        public static INConfigSettings AddFromRemoteWCFService(this INConfigSettings source)
        {
            source.AddConfigurationDataProvider(new ConvertFromSectionDataProvider(() =>
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
