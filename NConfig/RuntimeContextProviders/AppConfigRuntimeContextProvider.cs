using System;
using System.Collections.Generic;

namespace ByContext.RuntimeContextProviders
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
