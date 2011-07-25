using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ServiceContractConfig
    {
        public Uri Address { get; set; }
        public Type BindingType { get; set; }
    }
}
