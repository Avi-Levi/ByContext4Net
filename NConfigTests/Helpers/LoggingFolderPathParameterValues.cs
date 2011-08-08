using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Configuration;
using System.Reflection;

namespace NConfig.Tests.Helpers
{
    public static class LoggingFolderPathParameterValues
    {
        /// <summary>
        /// inside client installation folder.
        /// at development time, with self hosting.
        /// </summary>
        public static ParameterValue ReletiveOneLevelDownPath = ParameterValue.Create(@".\logs");
        /// <summary>
        /// must be absolute, because installation path is not known.
        /// </summary>
        public static ParameterValue ApplicationServerPath = ParameterValue.Create(@"c:\logs\");

        public static ParameterValue ProdOnlineClient = ParameterValue.Create(@"c:\UserData\ApplicationName\logs\");

        /// <summary>
        /// integration server logs to a different path?
        /// </summary>
        public static ParameterValue IntegrationServerPath = ParameterValue.Create(@"\someNetworDrive\integrationLogs\");

        /// <summary>
        /// specific service logs to a specific path?
        /// </summary>
        public static ParameterValue AuditServicePath = ParameterValue.Create(@"\\someNetworkDrive\enterpriseAuditData\");
    }
}
