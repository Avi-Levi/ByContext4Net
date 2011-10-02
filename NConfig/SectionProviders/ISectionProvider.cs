using System.Collections.Generic;

namespace NConfig.SectionProviders
{
    public interface ISectionProvider
    {
        object Get(IDictionary<string, string> runtimeContext);
    }
}
