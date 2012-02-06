using System;

namespace NConfig
{
    /// <summary>
    /// The configuration service's abstraction.
    /// Used to get configuration sections with values filtered according to the configured runtime context
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Returns a configuration section of type <typeparamref name="TSection"/> initialized with values filtered according to the configured runtime context.
        /// </summary>
        /// <typeparam name="TSection">Type of the requested section.</typeparam>
        /// <returns>A configuration section of type <typeparamref name="TSection"/>.</returns>
        /// IConfigurationService configService = Configure.With(cfg=>...);
        /// ExampleSection section = configService.GetSection<ExampleSection>();
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="TSection">The type of requested section.</typeparam>
        /// <returns>Section, populated with values configured according to the application's runtime context.</returns>
        TSection GetSection<TSection>() where TSection : class;
        
        /// <summary>
        /// Returns a configuration section of type <paramref name="sectionType"/> initialized with values filtered according to the configured runtime context.
        /// </summary>
        /// <param name="sectionType">Type of the requested section.</param>
        /// <returns>A configuration section of type <paramref name="sectionType"/>.</returns>
        /// IConfigurationService configService = Configure.With(cfg=>...);
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
        /// Returns a new instance of <see cref="IConfigurationService"/> with the given context subject reference.
        /// <remarks>
        /// Intended to be used when a subject's value is not known at <see cref="IConfigurationService"/>'s creation time.
        /// </remarks>
        /// </summary>
        /// <param name="subjectName">The name of the referenced context subject.</param>
        /// <param name="subjectValue">The reference's value.</param>
        /// <returns>New instance of <see cref="IConfigurationService"/> with the given context subject reference.</returns>
        IConfigurationService WithReference(string subjectName, string subjectValue);
        void AddReference(string subjectName, string subjectValue);
    }
}
