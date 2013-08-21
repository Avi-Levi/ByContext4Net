using System;

namespace ByContext.Logging
{
    public class NullLoggerProvider : ILoggerProvider
    {
        public ILogger GetLogger(Type source)
        {
            return new NullLogger();
        }

        public ILogger GetLogger(string source, Type t)
        {
            return new NullLogger();
        }
    }
}