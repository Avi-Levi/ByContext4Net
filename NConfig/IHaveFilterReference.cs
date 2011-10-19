using System.Collections.Generic;
using NConfig.Model;

namespace NConfig
{
    /// <summary>
    /// Indicates the relevance of the implementing type using a list of references to context subjects.
    /// </summary>
    public interface IHaveFilterReference
    {
        IList<ContextSubjectReference> References { get; }
    }
}
