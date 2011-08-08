using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class ConfigConstants
    {
        public static class Subjects
        {
            public static class Environment
            {
                public const string Name = "Environment";

                public const string Dev = "dev";
                public const string Prod = "prod";
            }
            public static class AppType
            {
                public const string Name = "AppType";

                public const string OnlineClient = "onlineClient";
                public const string ApplicationServer = "applicationServer";
                public const string IntegrationServer = "integrationServer";
            }
            public static class Service
            {
                public const string Name = "service";

                public const string Login = "Login";
            }
            public static class ServiceContract
            {
                public const string Name = "ServiceContract";

                public const string Login = "Common.ILoginService";
            }
            public static class LogOwner
            {
                public const string Name = "LogOwner";
            }
            public static class MachineName
            {
                public const string Name = "MachineName";

                public const string ClientMachine1 = "ClientMachine1";
                public const string ClientMachine2 = "ClientMachine2";

                public const string ServerMachine = "ServerMachine";
                
            }
        }
    }
}
