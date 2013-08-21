using System;
using ByContext.Logging;
using log4net;

namespace ByContext.Log4Net
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        public ILogger GetLogger(Type source)
        {
            return new Log4NetLogger(LogManager.GetLogger(source));
        }

        public ILogger GetLogger(string source, Type t)
        {
            return new Log4NetLogger(LogManager.GetLogger(source + "." + t.Name));
        }
    }
}