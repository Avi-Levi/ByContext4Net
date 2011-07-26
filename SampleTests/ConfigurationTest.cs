using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NConfig;
using Common;
using System.ServiceModel;
using Server.Services;
using Client;
using Server;
using NConfig.Filter;
using NConfig.Filter.Rules;

namespace SampleTests
{
    [TestClass]
    public class ConfigurationTest
    {
        private static IConfigurationService ConfigurationService()
        {
            IConfigurationService configSvc = Configure.With()
                .RuntimeContext(ctx =>
                {
                    ctx.Add(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev);
                    ctx.Add(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient);
                })
                .AddFromXmlFile("Configuration.xml")
                .Build();

            return configSvc;
        }

        [TestMethod]
        public void LoginServiceAdapter_for_ILoginService()
        {
            var cfgLogin = ConfigurationService()
                .WithReference(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.SomeMachine1)
                .WithServiceContractRef(typeof(ILoginService).FullName)
                .GetSection<ServiceContractConfig>();

            Assert.AreEqual<string>(cfgLogin.Address.AbsoluteUri, "net.tcp://localhost:20/login");
            Assert.AreEqual<Type>(cfgLogin.BindingType, typeof(NetTcpBinding));
        }

        [TestMethod]
        public void LoginServiceAdapter_for_IProductsService()
        {
            var cfg = ConfigurationService()
                .WithReference(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.SomeMachine1)
                .WithServiceContractRef(typeof(IProductsService).FullName)
                .GetSection<ServiceContractConfig>();

            Assert.AreEqual<string>(cfg.Address.AbsoluteUri, "net.tcp://localhost:21/products");
            Assert.AreEqual<Type>(cfg.BindingType, typeof(NetTcpBinding));
        }

        [TestMethod]
        public void LoggingConfiguration_for_LoginServiceAdapter_and_machine2()
        {
            var cfg = ConfigurationService()
                .WithReference(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.SomeMachine2)
                .WithLogOwnerRef(typeof(LoginServiceAdapter).FullName)
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(cfg.LogFilePath, @"D:\work\NConfig\NConfig\bin\logs\all.txt");
            Assert.AreEqual<LogLevelOption>(cfg.LogLevel, LogLevelOption.Error);
        }

        [TestMethod]
        public void LoggingConfiguration_for_ProductsServiceAdapter_and_machine2()
        {
            var cfg = ConfigurationService()
                .WithReference(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.SomeMachine2)
                .WithLogOwnerRef(typeof(ProductsServiceAdapter).FullName)
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(cfg.LogFilePath, @"D:\work\NConfig\NConfig\bin\logs\all.txt");
            Assert.AreEqual<LogLevelOption>(cfg.LogLevel, LogLevelOption.Error);
        }
        [TestMethod]
        public void LoggingConfiguration_for_ProductsService_and_machine2()
        {
            var cfg = ConfigurationService()
                .WithReference(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.SomeMachine2)
                .WithLogOwnerRef(typeof(ProductsService).FullName)
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(cfg.LogFilePath, @"D:\work\NConfig\NConfig\bin\logs\all.txt");
            Assert.AreEqual<LogLevelOption>(cfg.LogLevel, LogLevelOption.Error);
        }
        [TestMethod]
        public void LoggingConfiguration_for_LoginService_and_machine2()
        {
            var cfg = ConfigurationService()
                .WithReference(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.SomeMachine2)
                .WithLogOwnerRef(typeof(LoginService).FullName)
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(cfg.LogFilePath, @"D:\work\NConfig\NConfig\bin\logs\login.txt");
            Assert.AreEqual<LogLevelOption>(cfg.LogLevel, LogLevelOption.Error);
        }
        [TestMethod]
        public void LoggingConfiguration_for_LoginService_and_machine1()
        {
            var cfg = ConfigurationService()
                .WithReference(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.SomeMachine1)
                .WithLogOwnerRef(typeof(LoginService).FullName)
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(cfg.LogFilePath, @"D:\work\NConfig\NConfig\bin\logs\login.txt");
            Assert.AreEqual<LogLevelOption>(cfg.LogLevel, LogLevelOption.Trace);
        }

        [TestMethod]
        public void ServicesConfig()
        {
            var cfg = ConfigurationService()
                .WithReference(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.SomeMachine1)
                .GetSection<ServicesConfig>();

            Assert.IsNotNull(cfg.ServiceTypesToLoad);
            Assert.AreEqual<int>(cfg.ServiceTypesToLoad.Count(), 2);
            Assert.IsTrue(cfg.ServiceTypesToLoad.Contains(typeof(LoginService)));
            Assert.IsTrue(cfg.ServiceTypesToLoad.Contains(typeof(ProductsService)));
        }
    }
}
