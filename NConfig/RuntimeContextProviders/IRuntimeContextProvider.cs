using System.Collections.Generic;

namespace NConfig.RuntimeContextProviders
{
    public interface IRuntimeContextProvider
    {
        IDictionary<string, string> Get();
    }
}
