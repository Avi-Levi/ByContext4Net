using System.ServiceModel;
using ByContext.ConfigurationDataProviders;

namespace ByContext.WCF
{
    public static class ByContextSettingsWCFExtensions
    {
        public static IByContextSettings AddFromRemoteWCFService(this IByContextSettings source)
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
