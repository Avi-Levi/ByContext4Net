using System;

namespace NConfig
{
    /// <summary>
    /// Defines the behavior of the configuration service.
    /// Provides configuration data according to the application's runtime context.
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Returns a section, populated with values configured according to the application's runtime context.
        /// <example>
        /// Example usage:
        /// <code>
        /// IConfigurationService configService = Configure.With(cfg=>...);
        /// ExampleSection section = configService.GetSection<ExampleSection>();
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="TSection">The type of requested section.</typeparam>
        /// <returns>Section, populated with values configured according to the application's runtime context.</returns>
        TSection GetSection<TSection>() where TSection : class;

        /// <summary>
        /// Returns a section, populated with values configured according to the application's runtime context.
        /// <example>
        /// Example usage:
        /// <code>
        /// IConfigurationService configService = Configure.With(cfg=>...);
        /// ExampleSection section = configService.GetSection(typeof(ExampleSection));
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="sectionType">The type of requested section.</param>
        /// <returns>Section, populated with values configured according to the application's runtime context.</returns>
        object GetSection(Type sectionType);

        /// <summary>
        /// Returns a section, populated with values configured according to the application's runtime context.
        /// <remarks>
        /// This is a helper method in the way that it casts the result section to <typeparamref name="TSection"/>.
        /// </remarks>
        /// <example>
        /// Example usage:
        /// <code>
        /// IConfigurationService configService = Configure.With(cfg=>...);
        /// ParentSectionType section = configService.GetSection<ParentSectionType>(typeof(DerivedSectionType));
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
    }
}
