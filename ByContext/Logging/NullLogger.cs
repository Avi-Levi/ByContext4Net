using System;

namespace ByContext.Logging
{
    public class NullLogger : ILogger
    {
        public void Info(string msg)
        {}

        public void Info(Func<string> msgFactory)
        {}

        public void Debug(string msg)
        {}

        public void Debug(Func<string> msgFactory)
        { }

        public void Debug(Action msgFactory)
        {}
    }
}