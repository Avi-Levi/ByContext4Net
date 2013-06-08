using System;
using System.IO;
using ByContext;

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

            this._absolutePath = Path.GetFullPath(config.LogFilePath);
            var directoryPath = Path.GetDirectoryName(this._absolutePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        private string _absolutePath;
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
        public LoggerFactory(IByContext byContext)
        {
            this.ByContext = byContext;
        }

        private IByContext ByContext { get; set; }

        public ILogger Create(Type owner)
        {
            var config = this.ByContext.
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
