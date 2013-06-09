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

namespace ByContext.ParameterValueProviders
{
    /// <summary>
    /// Provides a configuration parameter value that is relevant to a certain runtime context.
    /// </summary>
    public interface IParameterValueProvider
    {
        /// <summary>
        /// Provide a configuration parameter value that is relevant to the given <paramref name="runtimeContext"/>.
        /// </summary>
        object Get(IDictionary<string, string> runtimeContext);
    }
}
