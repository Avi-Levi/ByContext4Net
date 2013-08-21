using System;
using System.Diagnostics;

namespace ByContext.Logging
{
    public class TraceLogger : ILogger
    {
        private readonly string _name;
        private readonly int _level;

        public TraceLogger(string name, LogLevel level)
        {
            _name = name;
            _level = (int)level;
        }

        public void Info(string msg)
        {
            if(this._level >= (int)LogLevel.Info) DoTrace(msg);
        }

        public void Info(Func<string> msgFactory)
        {
            if (this._level >= (int)LogLevel.Info) DoTrace(msgFactory());
        }

        public void Debug(string msg)
        {
            if (this._level >= (int)LogLevel.Debug) DoTrace(msg);
        }

        public void Debug(Func<string> msgFactory)
        {
            if (this._level >= (int)LogLevel.Debug) DoTrace(msgFactory());
        }

        private void DoTrace(string msg)
        {
            Trace.WriteLine(_name + ": " + msg);
        }
    }
}