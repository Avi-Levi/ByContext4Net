using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class ServicesConfig
    {
        public IEnumerable<Type> ServiceTypesToLoad { get; set; }
    }
}
