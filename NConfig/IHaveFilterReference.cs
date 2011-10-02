using System.Collections.Generic;
using NConfig.Model;

namespace NConfig
{
    public interface IHaveFilterReference
    {
        IList<ContextSubjectReference> References { get; }
    }
}
