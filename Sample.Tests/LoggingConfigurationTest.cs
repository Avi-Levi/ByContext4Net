using ByContext;
using Common;
using Client;
using NUnit.Framework;
using Server.Services;

namespace Sample.Tests
{
    [TestFixture]
    public class LoggingConfigurationTest
    {
        [Test]
        public void LoginServiceAdapter_and_machine2()
        {
            var configSvc =
                Configure.With(cfg =>
                    cfg.AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.OnlineClient);
                        ctx.Add(Subjects.MachineName.Name,
                        Subjects.MachineName.ClientMachine2);
                        ctx.Add(Subjects.LogOwner.Name,
                        typeof (LoginServiceAdapter).FullName);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<LoggingConfiguration>();

            Assert.AreEqual(@".\logs\log.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Error, section.LogLevel);
        }

        [Test]
        public void ProductsServiceAdapter_and_machine2()
        {
            var configSvc =
                Configure.With(cfg =>
                    cfg.AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.OnlineClient);
                        ctx.Add(Subjects.MachineName.Name,
                        Subjects.MachineName.ClientMachine2);
                        ctx.Add(Subjects.LogOwner.Name,
                        typeof (ProductsServiceAdapter).FullName);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<LoggingConfiguration>();

            Assert.AreEqual(@".\logs\log.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Error,section.LogLevel);
        }

        [Test]
        public void ProductsService_and_machine2()
        {
            var configSvc =
                Configure.With(cfg =>
                    cfg.AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.ApplicationServer);
                        ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ServerMachine);
                        ctx.Add(Subjects.LogOwner.Name, typeof(ProductsService).FullName);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<LoggingConfiguration>();

            Assert.AreEqual(@".\logs\all.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Error, section.LogLevel);
        }
        
        [Test]
        public void login_folder_path_for_login_service_and_applicationServer()
        {
            var configSvc =
                Configure.With(cfg =>
                    cfg.AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.ApplicationServer);
                        ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ServerMachine);
                        ctx.Add(Subjects.LogOwner.Name, typeof (LoginService).FullName);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<LoggingConfiguration>();

            Assert.AreEqual(@".\logs\login.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Error, section.LogLevel);
        }

        [Test]
        public void login_folder_path_for_login_service_and_applicationServer_check_with_other_references()
        {
            var configSvc =
                Configure.With(cfg =>
                    cfg.AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.ApplicationServer);
                        ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ClientMachine1);
                        ctx.Add(Subjects.LogOwner.Name, typeof(LoginService).FullName);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<LoggingConfiguration>();

            Assert.AreEqual(@".\logs\login.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Trace,section.LogLevel);
        }

        [Test]
        public void some_secure_path_on_production_client_machine()
        {
            var configSvc =
                Configure.With(cfg =>
                    cfg.AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Prod);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.OnlineClient);
                        ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ClientMachine1);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<LoggingConfiguration>();

            Assert.AreEqual(@".\logs\SomeSecuredPath\log.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Trace, section.LogLevel);
        }

        [Test]
        public void some_secure_path_on_production_client_machine_with_some_log_owner()
        {
            var configSvc =
                Configure.With(cfg =>
                    cfg.AddWindsorTranslatorProvider()
                    .RuntimeContext(ctx =>
                    {
                        ctx.Add(Subjects.Environment.Name, Subjects.Environment.Prod);
                        ctx.Add(Subjects.AppType.Name, Subjects.AppType.OnlineClient);
                        ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ClientMachine1);
                        ctx.Add(Subjects.LogOwner.Name, typeof(LoginServiceAdapter).FullName);
                    })
                    .AddFromXmlFile("Configuration.xml"));

            var section = configSvc.GetSection<LoggingConfiguration>();

            Assert.AreEqual(@".\logs\SomeSecuredPath\log.txt", section.LogFilePath);
            Assert.AreEqual(LogLevelOption.Trace, section.LogLevel);
        }
    }
}
