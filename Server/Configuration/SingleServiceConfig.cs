using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace Server.Configuration
{
    public class SingleServiceConfig
    {
        public IEnumerable<IServiceBehavior> ServiceBehaviors { get; set; }
    }
}
