using System;
using ByContext.Logging;
using log4net;

namespace ByContext.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(ILog log)
        {
            _log = log;
        }

        public void Info(string msg)
        {
            this._log.Info(msg);
        }

        public void Info(Func<string> msgFactory)
        {
            if (this._log.IsInfoEnabled)
            {
                this._log.Info(msgFactory());
            }
        }

        public void Debug(string msg)
        {
            this._log.Debug(msg);
        }

        public void Debug(Func<string> msgFactory)
        {
            if (this._log.IsDebugEnabled)
            {
                this._log.Debug(msgFactory());
            }
        }
    }
}