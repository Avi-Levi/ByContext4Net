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

namespace ByContext.Exceptions
{
    /// <summary>
    /// Thrown when an error occured when parsing a value of a configuration property.
    /// </summary>
    public class ConfigurationPropertyException : ByContextException
    {
        public ConfigurationPropertyException(string propertyName, string propertyValue, Type propertyType, Exception inner)
            :base(string.Format(
            "unknown value for property {0}, property name is {1}. the value must be a name of a registered value " +
            "of a valid type name that implements {0}.", propertyName, propertyValue), inner)
        {}
    }
}
