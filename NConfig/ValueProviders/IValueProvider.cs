namespace NConfig.ValueProviders
{
    /// <summary>
    /// Provides a value of a configuration parameter that is relevant under a given runtime context. 
    /// Implements <see cref="IHaveFilterReference"/> and thus exposing a list of <see cref="NConfig.Model.ContextSubjectReference"/> to context subjects, 
    /// the references list is used to determine the value's relevance for a given runtime context.
    /// a <see cref="IValueProvider"/> is selected at 
    /// <remarks>
    /// Also used as an extension point for providing values in different ways.
    /// </remarks>
    /// </summary>
    public interface IValueProvider : IHaveFilterReference
    {
        /// <summary>
        /// Return a configuration parameter value.
        /// </summary>
        object Get();
    }
}