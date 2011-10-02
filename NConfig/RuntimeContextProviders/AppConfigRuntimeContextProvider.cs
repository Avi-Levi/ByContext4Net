using System;
using System.Collections.Generic;

namespace NConfig.RuntimeContextProviders
{
    public class AppConfigRuntimeContextProvider : IRuntimeContextProvider
    {
        public IDictionary<string, string> Get()
        {
            //TODO: get context from app.config
            throw new NotImplementedException();
        }
    }
}
