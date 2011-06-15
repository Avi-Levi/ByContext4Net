using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NConfig.Impl
{
    public class DebugLogger : ILoggerFacade
    {
        public void Log(string message)
        {
            Debug.WriteLine(message, "Filter");
        }

        public void LogFormat(string message, params string[] args)
        {
            Debug.WriteLine(string.Format(message, args));
        }
    }
}
