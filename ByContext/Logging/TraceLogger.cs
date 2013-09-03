// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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