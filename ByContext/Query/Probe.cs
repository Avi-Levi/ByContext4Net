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

using System.Collections.Generic;

namespace ByContext.Query
{
    public class Probe : IProbe
    {
        public bool Exclude { get; set; }
        public int ReferencesCount { get; set; }
        public int ExplicitPositiveReferencesCount { get; set; }
        public int ExplicitNoneNegatingNegativeCount { get; set; }

        private readonly List<string> _trace = new List<string>();
        public void Trace(string message, params object[] args)
        {
            this._trace.Add(string.Format(message, args));
        }
    }
}