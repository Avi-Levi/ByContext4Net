using System;

namespace ByContext.Logging
{
    public interface ILoggerProvider
    {
        ILogger GetLogger(string source, Type t);
    }
}