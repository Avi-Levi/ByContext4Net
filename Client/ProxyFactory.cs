using Common;
using ByContext;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Client
{
    public class ProxyFactory
    {
        public ProxyFactory(IByContext configSvc)
        {
            this.ConfigService = configSvc;
        }

        private IByContext ConfigService { get; set; }

        public TContract Get<TContract>() where TContract : class
        {
            var configSvcWithContractRef = this.ConfigService.WithServiceContractRef(typeof(TContract));

            ServiceContractConfig contractConfig = configSvcWithContractRef.GetSection<ServiceContractConfig>();

            Binding binding = configSvcWithContractRef.GetSection<Binding>(contractConfig.BindingType);

            TContract proxy =
                ChannelFactory<TContract>.CreateChannel(binding, new EndpointAddress(contractConfig.Address));

            return proxy;
        }
    }
}
