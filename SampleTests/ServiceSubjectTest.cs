//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NConfig.Testing;
//using Common;
//using NConfig;
//using Server.Services;

//namespace SampleTests
//{
//    [TestClass]
//    public class ServiceSubjectTest
//    {
//        [TestMethod]

//        [RuntimeContextItem(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev)]
//        [RuntimeContextItem(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.OnlineClient)]
//        [RuntimeContextItem(ConfigConstants.Subjects.MachineName.Name, ConfigConstants.Subjects.MachineName.ClientMachine2)]
//        [RuntimeContextItem(ConfigConstants.Subjects.LogOwner.Name, typeof(LoginService))]
//        public void LoginServiceAdapter_and_machine2()
//        {
//            var cfg = Configure.With().ContextFromCallingMethod().AddFromXmlFile("Configuration.xml").Build()
//                .GetSection<LoggingConfiguration>();

//            Assert.AreEqual(@"D:\work\NConfig\NConfig\Client\bin\Debug\logs\log.txt", cfg.LogFilePath);
//            Assert.AreEqual<LogLevelOption>(LogLevelOption.Error, cfg.LogLevel);
//        }
//    }
//}
