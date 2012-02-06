using System.Collections.Generic;

namespace NConfig.ResultBuilder
{
    /// <summary>
    /// Used to build the final instance of the configuration parameter out of the values relevant for the runtime context.
    /// <remarks>
    /// used mainly when the target parameter is a collection.
    /// </remarks>
    /// </summary>
    public interface IResultBuilder
    {
        /// <summary>
        /// Converts the given collection to the final form of a parameter value.
        /// </summary>
        object Build(IEnumerable<object> input);
    }
}
