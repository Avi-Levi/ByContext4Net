using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig.Configuration
{
    public class ParameterConfiguration
    {
        private ParameterConfiguration()
        {}

        public IValueProvider ValueProvider { get; set; }

        public static ParameterConfiguration From()
        {
            return new ParameterConfiguration();
        }
    }
}
