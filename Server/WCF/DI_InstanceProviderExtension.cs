using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using Castle.Windsor;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.ObjectModel;

namespace Server.WCF
{
    public class DI_InstanceProviderExtension : IInstanceProvider, IServiceBehavior
    {
        public DI_InstanceProviderExtension(IWindsorContainer container)
        {
            this.Container = container;
        }

        private IWindsorContainer Container { get; set; }

        #region IInstanceProvider
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.Container.Resolve(instanceContext.Host.Description.ServiceType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return this.Container.Resolve(instanceContext.Host.Description.ServiceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        { }
        #endregion IInstanceProvider

        #region IServiceBehavior
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher cd = cdb as ChannelDispatcher;
                if (cd != null)
                {
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        ed.DispatchRuntime.InstanceProvider = this;
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
        #endregion IServiceBehavior
    }
}
