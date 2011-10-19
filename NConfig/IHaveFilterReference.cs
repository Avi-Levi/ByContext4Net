using System.Collections.Generic;
using NConfig.Model;

namespace NConfig
{
    /// <summary>
    /// Represents an object that has references to certain context subjects.
    /// <remarks>
    /// this interface is used 
    /// </remarks>
    /// </summary>
    public interface IHaveFilterReference
    {
        IList<ContextSubjectReference> References { get; }
    }
}
