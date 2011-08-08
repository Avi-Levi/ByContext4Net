using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NConfig;
using Common;
using Client;
using Server.Services;
using NConfig.Testing;

namespace SampleTests
{
    [TestClass]
    public class LoggingConfigurationTest
    {
        [TestMethod]

        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine2)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginServiceAdapter))]
        public void LoginServiceAdapter_and_machine2()
        {
            var cfg = Configure.With().AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\log.txt", cfg.LogFilePath);
            Assert.AreEqual<LogLevelOption>(LogLevelOption.Error, cfg.LogLevel);
        }

        [TestMethod]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine2)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(ProductsServiceAdapter))]
        public void ProductsServiceAdapter_and_machine2()
        {
            var cfg = Configure.With().AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\log.txt", cfg.LogFilePath);
            Assert.AreEqual<LogLevelOption>(LogLevelOption.Error,cfg.LogLevel);
        }

        private IConfigurationService ConfigurationService()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.ApplicationServer)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ServerMachine)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(ProductsService))]
        public void ProductsService_and_machine2()
        {
            var cfg = Configure.With().AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(@"D:\work\NConfig\NConfig\Server\bin\Debug\logs\all.txt", cfg.LogFilePath);
            Assert.AreEqual<LogLevelOption>(LogLevelOption.Error, cfg.LogLevel);
        }
        [TestMethod]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.ApplicationServer)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ServerMachine)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginService))]
        public void login_folder_path_for_login_service_and_applicationServer()
        {
            var cfg = Configure.With().AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(@"D:\work\NConfig\NConfig\Server\bin\Debug\logs\login.txt", cfg.LogFilePath);
            Assert.AreEqual<LogLevelOption>(LogLevelOption.Error, cfg.LogLevel);
        }

        [TestMethod]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.ApplicationServer)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginService))]
        public void login_folder_path_for_login_service_and_applicationServer_check_with_other_references()
        {
            var cfg = Configure.With().AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(@"D:\work\NConfig\NConfig\Server\bin\Debug\logs\login.txt", cfg.LogFilePath);
            Assert.AreEqual<LogLevelOption>(LogLevelOption.Trace,cfg.LogLevel);
        }

        [TestMethod]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Prod)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        public void some_secure_path_on_production_client_machine()
        {
            var cfg = Configure.With().AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\SomeSecuredPath\log.txt", cfg.LogFilePath);
            Assert.AreEqual<LogLevelOption>(LogLevelOption.Trace, cfg.LogLevel);
        }

        [TestMethod]
        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Prod)]
        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine1)]
        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginServiceAdapter))]
        public void some_secure_path_on_production_client_machine_with_some_log_owner()
        {
            var cfg = Configure.With().AddWindsorTranslatorProvider().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
                .GetSection<LoggingConfiguration>();

            Assert.AreEqual<string>(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\SomeSecuredPath\log.txt", cfg.LogFilePath);
            Assert.AreEqual<LogLevelOption>(LogLevelOption.Trace, cfg.LogLevel);
        }

    }
}
