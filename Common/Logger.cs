using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NConfig;

namespace Common
{
    public interface ILogger
    {
        void Write(string message, LogLevelOption level);
    }
    internal class FlatFileLogger : ILogger
    {
        public FlatFileLogger(LoggingConfiguration config)
        {
            this._config = config;

            string dirName = Path.GetDirectoryName(config.LogFilePath);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
        }

        private LoggingConfiguration _config;

        public void Write(string message, LogLevelOption level)
        {
            if ((int)level >= (int)this._config.LogLevel)
            {
                File.AppendAllText(this._config.LogFilePath, DateTime.Now.ToString() + " - " + message + Environment.NewLine);
            }
        }
    }

    public class LoggerFactory
    {
        public LoggerFactory(IConfigurationService configurationService)
        {
            this.ConfigurationService = configurationService;
        }

        private IConfigurationService ConfigurationService { get; set; }

        public ILogger Create(Type owner)
        {
            var config = this.ConfigurationService.
                WithLogOwnerRef(owner).GetSection<LoggingConfiguration>();

            return new FlatFileLogger(config);
        }
    }
    public enum LogLevelOption
    {
        Error=0,
        Trace = 1,
    }
    public class LoggingConfiguration
    {
        public LogLevelOption LogLevel { get; set; }
        public string LogFilePath { get; set; }
    }
}
