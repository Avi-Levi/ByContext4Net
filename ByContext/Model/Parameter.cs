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
using System.Runtime.Serialization;

namespace ByContext.Model
{
    /// <summary>
    /// Represents data item, which we want to have a different value
    /// according to the context provided at runtime by the caller.
    /// </summary>

    [DataContract]
    public class Parameter
    {
        public Parameter()
        {
            this.Values = new List<ParameterValue>();
        }

        [DataMember]
        public string Translator { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public string PolicyName { get; set; }
        [DataMember]
        public string Required { get; set; }

        [DataMember]
        public IList<ParameterValue> Values { get; set; }
    }
}
