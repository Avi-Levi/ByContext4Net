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

namespace ByContext.ValueProviders
{
    /// <summary>
    /// Represents an object that provides a parameter's value.
    /// <remarks>
    /// For now, implemented by <see cref="TranslateFromStringValueProvider"/> and provides an extension point to enable providing values in different ways.
    /// Also used as an extension point for providing values in different ways.
    /// </remarks>
    /// </summary>
    public interface IValueProvider
    {
        /// <summary>
        /// Returns a configuration value.
        /// </summary>
        /// <returns>object</returns>
        object Get();
    }
}