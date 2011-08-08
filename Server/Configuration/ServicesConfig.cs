using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace Server
{
    public class ServicesConfig
    {
        public IEnumerable<Type> ServiceTypesToLoad { get; set; }
    }
}
