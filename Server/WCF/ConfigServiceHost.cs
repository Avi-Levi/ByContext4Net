using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Common;
using NConfig;

namespace Server
{
    public class ConfigServiceHost : ServiceHost
    {
        public ConfigServiceHost(Type serviceType, IConfigurationService configService, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this.ConfigService = configService;
        }

        private IConfigurationService ConfigService { get; set; }
        protected override void OnOpening()
        {
            foreach(var implementedContract in this.ImplementedContracts)
            {
                this.AddEndpointForContract(implementedContract.Value.ContractType);
            }

            base.OnOpening();
        }

        private void AddEndpointForContract(Type contractType)
        {
            var configSvcWithContractRef = this.ConfigService.WithServiceContractRef(contractType);
            ServiceContractConfig contractConfig = configSvcWithContractRef.GetSection<ServiceContractConfig>();

            Binding binding = configSvcWithContractRef.GetSection<Binding>(contractConfig.BindingType);
            
            this.AddServiceEndpoint(contractType, binding, contractConfig.Address);
        }
    }
}
