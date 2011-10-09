using System.Linq;
using NConfig;
using Common;
using System.ServiceModel;
using NUnit.Framework;
using Server.Services;
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
            var configSvc = Configure.With(cfg=>cfg.AddWindsorTranslatorProvider().
                ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<ServiceContractConfig>();
            var binding = configSvc.GetSection<Binding>(section.BindingType);

            Assert.AreEqual(section.Address.AbsoluteUri, "net.tcp://localhost:20/login");
            Assert.AreEqual(typeof(NetTcpBinding), section.BindingType);
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
            var configSvc = Configure.With(cfg=>cfg.AddWindsorTranslatorProvider().
                ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<ServiceContractConfig>();
            var binding = configSvc.GetSection<Binding>(section.BindingType);

            Assert.AreEqual("net.tcp://localhost:21/products", section.Address.AbsoluteUri);
            Assert.AreEqual(typeof(NetTcpBinding), section.BindingType);
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
            var section = Configure.With(cfg=>cfg.AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"))
                .GetSection<ServicesConfig>();

            Assert.IsNotNull(section.ServiceTypesToLoad);
            Assert.AreEqual(section.ServiceTypesToLoad.Count(), 2);
            Assert.IsTrue(section.ServiceTypesToLoad.Contains(typeof(LoginService)));
            Assert.IsTrue(section.ServiceTypesToLoad.Contains(typeof(ProductsService)));
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

            var section = Configure.With(cfg=>cfg.AddWindsorTranslatorProvider(container).ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"))
                .GetSection<SingleServiceConfig>();

            Assert.IsNotNull(section.ServiceBehaviors);
            Assert.AreEqual(1, section.ServiceBehaviors.Count());
            Assert.IsTrue(section.ServiceBehaviors.Any(x=>typeof(DI_InstanceProviderExtension).IsInstanceOfType(x)));
        }
    }
}
