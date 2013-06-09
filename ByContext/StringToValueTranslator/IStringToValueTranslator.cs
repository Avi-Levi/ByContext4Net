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

using ByContext.StringToValueTranslator.SerializeStringToValueTranslator;

namespace ByContext.StringToValueTranslator
{
    /// <summary>
    /// Provides an abstraction for resolving configuration values from given strings.
    /// Provides an extension point for resolving string configuration values in different ways.
    /// <remarks>
    /// Implementation that is provided with the library is <see cref="SerializeStringToValueTranslatorProvider"/>
    /// which deserializes the given string.
    /// </remarks>
    /// </summary>
    public interface IStringToValueTranslator
    {
        /// <summary>
        /// Deserializes the given string.
        /// </summary>
        /// <param name="value">Input string to be translated.</param>
        /// <returns>A configuration value.</returns>
        object Translate(string value);
    }
}
