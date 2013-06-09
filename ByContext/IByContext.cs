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
using System.Collections.Generic;

namespace ByContext
{
    /// <summary>
    /// The configuration service's abstraction.
    /// Used to get configuration sections with values filtered according to the configured runtime context
    /// </summary>
    public interface IByContext
    {
        /// <summary>
        /// Returns a configuration section of type <typeparamref name="TSection"/> initialized with values filtered according to the configured runtime context.
        /// </summary>
        /// <typeparam name="TSection">Type of the requested section.</typeparam>
        /// <returns>A configuration section of type <typeparamref name="TSection"/>.</returns>
        /// IByContext configService = Configure.With(cfg=>...);
        /// ExampleSection section = configService.GetSection<ExampleSection>();
        /// <typeparam name="TSection">The type of requested section.</typeparam>
        /// <returns>Section, populated with values configured according to the application's runtime context.</returns>
        TSection GetSection<TSection>() where TSection : class;
        
        /// <summary>
        /// Returns a configuration section of type <paramref name="sectionType"/> initialized with values filtered according to the configured runtime context.
        /// </summary>
        /// <param name="sectionType">Type of the requested section.</param>
        /// <returns>A configuration section of type <paramref name="sectionType"/>.</returns>
        /// IByContext configService = Configure.With(cfg=>...);
        /// ExampleSection section = configService.GetSection(typeof(ExampleSection));
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="sectionType">The type of requested section.</param>
        /// <returns>Section, populated with values configured according to the application's runtime context.</returns>
        object GetSection(Type sectionType);

        /// <summary>
        /// Returns a configuration section of type <paramref name="sectionType"/> casted to type <typeparamref name="TSection"/>.
        /// Initialized with values filtered according to the configured runtime context.
        /// <remarks>
        /// this overload is a helper method on top of <code>object GetSection(Type sectionType)</code> in that it casts the requested section to <typeparamref name="TSection"/>.
        /// </remarks>
        /// </summary>
        /// <typeparam name="TSection">The type to cast the section to.</typeparam>
        /// <param name="sectionType">Type of the requested section.</param>
        /// <returns>A configuration section of type <paramref name="sectionType"/>.</returns>
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="sectionType">The type of requested section.</param>
        /// <returns>Section, populated with values configured according to the application's runtime context.</returns>
        TSection GetSection<TSection>(Type sectionType) where TSection : class;

        /// <summary>
        /// Returns a new instance of <see cref="IByContext"/> with the given context subject reference, added to the existing context, overriding existing keys.
        /// <remarks>
        /// Intended to be used when a subject's value is not known at <see cref="IByContext"/>'s creation time.
        /// </remarks>
        /// </summary>
        /// <param name="subjectName">The name of the referenced context subject.</param>
        /// <param name="subjectValue">The reference's value.</param>
        /// <returns>New instance of <see cref="IByContext"/> with the given context subject reference.</returns>
        IByContext WithReference(string subjectName, string subjectValue);

        /// <summary>
        /// Returns a new instance of <see cref="IByContext"/> with the given context references, added to the existing context, overriding existing keys.
        /// <remarks>
        /// Intended to be used when a subject's value is not known at <see cref="IByContext"/>'s creation time.
        /// </remarks>
        /// </summary>
        /// <param name="references">a collection of context references</param>
        /// <returns>New instance of <see cref="IByContext"/> with the given context subject reference.</returns>
        IByContext WithReferences(IDictionary<string, string> references);
        
        void AddReference(string subjectName, string subjectValue);
    }
}