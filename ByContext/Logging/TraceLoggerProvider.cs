using System;

namespace ByContext.Logging
{
    public class TraceLoggerProvider : ILoggerProvider
    {
        private readonly LogLevel _level;

        public TraceLoggerProvider(LogLevel level)
        {
            _level = level;
        }

        public ILogger GetLogger(string source, Type t)
        {
            return new TraceLogger(source + "." + t.Name, _level);
        }
    }
}