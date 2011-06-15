using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NConfig
{
    public interface ILoggerFacade
    {
        void Log(string message);

        void LogFormat(string message, params string[] args);
    }
}
