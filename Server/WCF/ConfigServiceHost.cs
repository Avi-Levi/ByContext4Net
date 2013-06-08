using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Common;
using ByContext;

namespace Server.WCF
{
    public class ConfigServiceHost : ServiceHost
    {
        public ConfigServiceHost(Type serviceType, IByContext configService, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this.ConfigService = configService;
        }

        private IByContext ConfigService { get; set; }
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
            var contractConfig = configSvcWithContractRef.GetSection<ServiceContractConfig>();

            var binding = configSvcWithContractRef.GetSection<Binding>(contractConfig.BindingType);
            
            this.AddServiceEndpoint(contractType, binding, contractConfig.Address);
        }
    }
}
