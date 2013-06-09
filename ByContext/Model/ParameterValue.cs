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
    [DataContract]
    public class ParameterValue
    {
        public ParameterValue()
        {
            this.FilterConditions = new List<FilterCondition>();
        }
        public override bool Equals(object obj)
        {
            var other = obj as ParameterValue;
            if (other == null)
            {
                return false;
            }
            return this.GetHashCode().Equals(other.GetHashCode());
        }
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
        public override string ToString()
        {
            return this.Value ?? string.Empty;
        }

        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public IList<FilterCondition> FilterConditions { get; private set; }
    }
}
