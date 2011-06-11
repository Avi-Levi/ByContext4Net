using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig;
using NConfig.Model;

namespace TestProject1
{

    static class Items
    {
        /// <summary>
        /// inside client installation folder.
        /// at development time, with self hosting.
        /// </summary>
        public static ParameterValue ReletiveOneLevelDownPath = new ParameterValue(@".\logs" );
        /// <summary>
        /// must be absolute, because installation path is not known.
        /// </summary>
        public static ParameterValue applicationServerPath = new ParameterValue (@"c:\logs\");

        public static ParameterValue prodOnlineClient = new ParameterValue (@"c:\UserData\ApplicationName\logs\" );
        

        /// <summary>
        /// integration server logs to a different path?
        /// </summary>
        public static ParameterValue IntegrationServerPath = new ParameterValue (@"\someNetworDrive\integrationLogs\");

        /// <summary>
        /// specific service logs to a specific path?
        /// </summary>
        public static ParameterValue AuditServicePath = new ParameterValue(@"\\someNetworkDrive\enterpriseAuditData\");

        public static ParameterValue[] All = new ParameterValue[] 
        { ReletiveOneLevelDownPath, applicationServerPath, prodOnlineClient, IntegrationServerPath, AuditServicePath };
    }
    class env
    {
        public const string Name = "env";

        public const string dev = "dev";
        public const string prod = "prod";
    }
    class appType
    {
        public const string Name = "appType";

        public const string onlineClient = "onlineClient";
        public const string applicationServer = "applicationServer";
        public const string integrationServer = "integrationServer";
    }

    class services
    {
        public const string name = "services";

        public const string AuditService = "AuditService";
        public const string SomeService = "SomeService";
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RuntimeContextitemAttribute : Attribute
    {
        public RuntimeContextitemAttribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class FilterPolicyAttribute : Attribute
    {
        public FilterPolicyAttribute(Type policyType)
        {
            this.PolicyType = policyType;
        }

        public Type PolicyType { get; set; }
    }


}
