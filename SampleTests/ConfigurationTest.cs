using System.Linq;
using Common.Configuration;
using NConfig;
using Common;
using System.ServiceModel;
using NUnit.Framework;
using Server.Services;
using Server;
using NConfig.Testing;
using Server.Configuration;
using Server.WCF;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace SampleTests
{
    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        [RuntimeContextItem(ConfigConstants.Subjects.ServiceContract.Name, "Common.ILoginService")]

        public void ServiceContractConfig_for_ILoginService()
        {
            var configSvc = Configure.With().AddWindsorTranslatorProvider().
                ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build();
            var cfg = configSvc.GetSection<ServiceContractConfig>();
            var binding = configSvc.GetSection<Binding>(cfg.BindingType);

            Assert.AreEqual(cfg.Address.AbsoluteUri, "net.tcp://localhost:20/login");
            Assert.AreEqual(typeof(NetTcpBinding), cfg.BindingType);
            Assert.IsTrue(typeof(NetTcpBinding).IsInstanceOfType(binding));

            NetTcpBinding tcpBinding = (NetTcpBinding)binding;
            Assert.AreEqual(new NetTcpBinding().MaxReceivedMessageSize, tcpBinding.MaxReceivedMessageSize);
        }

        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        [RuntimeContextItem(ConfigConstants.Subjects.ServiceContract.Name, "Common.IProductsService")]
        public void ServiceContractConfig_for_IProductsService()
        {
            var configSvc = Configure.With().AddWindsorTranslatorProvider().
                ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build();

            var cfg = configSvc.GetSection<ServiceContractConfig>();
            var binding = configSvc.GetSection<Binding>(cfg.BindingType);

            Assert.AreEqual("net.tcp://localhost:21/products", cfg.Address.AbsoluteUri);
            Assert.AreEqual(typeof(NetTcpBinding), cfg.BindingType);
            Assert.IsTrue(typeof(NetTcpBinding).IsInstanceOfType(binding));

            NetTcpBinding tcpBinding = (NetTcpBinding)binding;
            Assert.AreEqual(3145728, tcpBinding.MaxReceivedMessageSize);
        }

        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        public void ServicesConfig()
        {
            var cfg = Configure.With().AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<ServicesConfig>();

            Assert.IsNotNull(cfg.ServiceTypesToLoad);
            Assert.AreEqual(cfg.ServiceTypesToLoad.Count(), 2);
            Assert.IsTrue(cfg.ServiceTypesToLoad.Contains(typeof(LoginService)));
            Assert.IsTrue(cfg.ServiceTypesToLoad.Contains(typeof(ProductsService)));
        }

        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.ApplicationServer)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ServerMachine)]
        public void SingleServiceConfig()
        {
            IWindsorContainer container = new WindsorContainer()
            .Register(Component.For<IServiceBehavior>().ImplementedBy<DI_InstanceProviderExtension>().Named("DI"));

            container.Register(Component.For<IWindsorContainer>().Instance(container));

            var cfg = Configure.With().AddWindsorTranslatorProvider(container).ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<SingleServiceConfig>();

            Assert.IsNotNull(cfg.ServiceBehaviors);
            Assert.AreEqual(1, cfg.ServiceBehaviors.Count());
            Assert.IsTrue(cfg.ServiceBehaviors.Any(x=>typeof(DI_InstanceProviderExtension).IsInstanceOfType(x)));
        }
    }
}
