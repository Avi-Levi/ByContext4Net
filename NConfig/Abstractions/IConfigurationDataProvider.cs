using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Configuration;

namespace NConfig.Abstractions
{
    public interface IConfigurationDataProvider
    {
        IDictionary<string, ISectionProvider> Get();
    }
}
