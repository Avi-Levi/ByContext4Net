using System.Linq;
using Common.Contracts;
using ByContext;
using Common;
using System.ServiceModel;
using NUnit.Framework;
using Server.Services;
using Server.Configuration;
using Server.WCF;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace Sample.Tests
{
    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        public void ServiceContractConfig_for_ILoginService()
        {
            var configSvc = 
                Configure.With(cfg=>cfg
                    .AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx=>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.OnlineClient);
                        ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ClientMachine1);
                        ctx.Add(Subjects.ServiceContract.Name, typeof(ILoginService).FullName);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<ServiceContractConfig>();
            var binding = configSvc.GetSection<Binding>(section.BindingType);

            Assert.AreEqual(section.Address.AbsoluteUri, "net.tcp://localhost:20/login");
            Assert.AreEqual(typeof(NetTcpBinding), section.BindingType);
            Assert.IsTrue(typeof(NetTcpBinding).IsInstanceOfType(binding));

            var tcpBinding = (NetTcpBinding)binding;
            Assert.AreEqual(new NetTcpBinding().MaxReceivedMessageSize, tcpBinding.MaxReceivedMessageSize);
        }

        [Test]
        
        public void ServiceContractConfig_for_IProductsService()
        {
            var configSvc =
                Configure.With(cfg => cfg
                    .AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.OnlineClient);
                        ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ClientMachine1);
                        ctx.Add(Subjects.ServiceContract.Name, typeof(IProductsService).FullName);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<ServiceContractConfig>();
            var binding = configSvc.GetSection<Binding>(section.BindingType);

            Assert.AreEqual("net.tcp://localhost:21/products", section.Address.AbsoluteUri);
            Assert.AreEqual(typeof(NetTcpBinding), section.BindingType);
            Assert.IsTrue(typeof(NetTcpBinding).IsInstanceOfType(binding));

            var tcpBinding = (NetTcpBinding)binding;
            Assert.AreEqual(3145728, tcpBinding.MaxReceivedMessageSize);
        }

        [Test]
        
        public void ServicesConfig()
        {
            var configSvc =
                Configure.With(cfg => cfg
                    .AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                                        {
                                            ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                                            ctx.Add(Subjects.AppType.Name, Subjects.AppType.OnlineClient);
                                            ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ClientMachine1);
                                        })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<ServicesConfig>();

            Assert.IsNotNull(section.ServiceTypesToLoad);
            Assert.AreEqual(section.ServiceTypesToLoad.Count(), 2);
            Assert.IsTrue(section.ServiceTypesToLoad.Contains(typeof(LoginService)));
            Assert.IsTrue(section.ServiceTypesToLoad.Contains(typeof(ProductsService)));
        }

        [Test]
        public void SingleServiceConfig()
        {
            IWindsorContainer container = new WindsorContainer()
            .Register(Component.For<IServiceBehavior>().ImplementedBy<DI_InstanceProviderExtension>().Named("DI"));

            container.Register(Component.For<IWindsorContainer>().Instance(container));

            var configSvc =
                Configure.With(cfg => cfg
                    .AddWindsorTranslatorProvider(container)
                    .RuntimeContext(ctx =>
                                        {
                                            ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                                            ctx.Add(Subjects.AppType.Name, Subjects.AppType.ApplicationServer);
                                            ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ServerMachine);
                                        })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<SingleServiceConfig>();

            Assert.IsNotNull(section.ServiceBehaviors);
            Assert.AreEqual(1, section.ServiceBehaviors.Count());
            Assert.IsTrue(section.ServiceBehaviors.Any(x=>typeof(DI_InstanceProviderExtension).IsInstanceOfType(x)));
        }
    }
}
