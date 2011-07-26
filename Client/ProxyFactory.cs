using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig;
using Common;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Client
{
    public class ProxyFactory
    {
        public ProxyFactory(IConfigurationService configSvc)
        {
            this.ConfigService = configSvc;
        }

        private IConfigurationService ConfigService { get; set; }

        public TContract Get<TContract>() where TContract : class
        {
            ServiceContractConfig contractConfig =
                this.ConfigService.WithServiceContractRef(typeof(TContract).FullName).GetSection<ServiceContractConfig>();

            Binding binding = (Binding)Activator.CreateInstance(contractConfig.BindingType);

            TContract proxy =
                ChannelFactory<TContract>.CreateChannel(binding, new EndpointAddress(contractConfig.Address));

            return proxy;
        }
    }
}
