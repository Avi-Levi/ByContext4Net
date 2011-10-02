using System;
using System.Collections.Generic;

namespace Server.Configuration
{
    public class ServicesConfig
    {
        public IEnumerable<Type> ServiceTypesToLoad { get; set; }
    }
}
