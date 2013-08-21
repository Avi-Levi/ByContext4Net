using System;

namespace ByContext.Logging
{
    public interface ILogger
    {
        void Info(string msg);
        void Info(Func<string> msgFactory);
        void Debug(string msg);
        void Debug(Func<string> msgFactory);
        
    }
}