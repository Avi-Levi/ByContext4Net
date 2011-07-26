using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NConfig;
using Common;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Castle.Windsor;
using System.Collections.ObjectModel;

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
            ServiceContractConfig contractConfig =
                this.ConfigService.WithServiceContractRef(contractType.FullName)
                .GetSection<ServiceContractConfig>();
            
            Binding binding = (Binding)Activator.CreateInstance(contractConfig.BindingType);

            this.AddServiceEndpoint(contractType, binding, contractConfig.Address);
        }
    }

    public class DI_InstanceProvider : IInstanceProvider
    {
        public DI_InstanceProvider(IWindsorContainer container)
        {
            this.Container = container;
        }

        private IWindsorContainer Container { get; set; }
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.Container.Resolve(instanceContext.Host.Description.ServiceType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return this.Container.Resolve(instanceContext.Host.Description.ServiceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {}
    }
    internal class InstanceProviderServiceBehavior : IServiceBehavior
    {
        public InstanceProviderServiceBehavior(IInstanceProvider provider)
        {
            this._provider = provider;
        }

        private IInstanceProvider _provider;

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher cd = cdb as ChannelDispatcher;
                if (cd != null)
                {
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        ed.DispatchRuntime.InstanceProvider = this._provider;
                    }
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            // Not in use.
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            // Not in use.
        }
    }
}
