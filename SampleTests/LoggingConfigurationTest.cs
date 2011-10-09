using System;
using NConfig;
using Common;
using Client;
using NUnit.Framework;
using Server.Services;
using NConfig.Testing;

namespace SampleTests
{
    [TestFixture]
    public class LoggingConfigurationTest
    {
        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine2)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginServiceAdapter))]
        public void LoginServiceAdapter_and_machine2()
        {
            var section = Configure.With(cfg=>
                cfg.AddWindsorTranslatorProvider()
                .ContextFromCallingMethod()
                .AddFromXmlFile("Configuration.xml"))
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\log.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Error, section.LogLevel);
        }

        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine2)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(ProductsServiceAdapter))]
        public void ProductsServiceAdapter_and_machine2()
        {
            var section = Configure.With(cfg=>cfg.AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"))
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\log.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Error,section.LogLevel);
        }

        private IConfigurationService ConfigurationService()
        {
            throw new NotImplementedException();
        }
        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.ApplicationServer)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ServerMachine)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(ProductsService))]
        public void ProductsService_and_machine2()
        {
            var section = Configure.With(cfg=> cfg.AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"))
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual(@"D:\work\NConfig\NConfig\Server\bin\Debug\logs\all.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Error, section.LogLevel);
        }
        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.ApplicationServer)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ServerMachine)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginService))]
        public void login_folder_path_for_login_service_and_applicationServer()
        {
            var section = Configure.With(cfg=> cfg.AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"))
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual(@"D:\work\NConfig\NConfig\Server\bin\Debug\logs\login.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Error, section.LogLevel);
        }

        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.ApplicationServer)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginService))]
        public void login_folder_path_for_login_service_and_applicationServer_check_with_other_references()
        {
            var section = Configure.With(cfg=> cfg.AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"))
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual(@"D:\work\NConfig\NConfig\Server\bin\Debug\logs\login.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Trace,section.LogLevel);
        }

        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Prod)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        public void some_secure_path_on_production_client_machine()
        {
            var section = Configure.With(cfg=> cfg.AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"))
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\SomeSecuredPath\log.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Trace, section.LogLevel);
        }

        [Test]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Prod)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginServiceAdapter))]
        public void some_secure_path_on_production_client_machine_with_some_log_owner()
        {
            var section = Configure.With(cfg=> cfg.AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml"))
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\SomeSecuredPath\log.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Trace, section.LogLevel);
        }

    }
}
