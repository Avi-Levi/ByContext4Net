using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig
{
    public class SectionConfiguration
    {
        private SectionConfiguration()
        {}

        public ISectionProvider SectionProvider { get; set; }
        public IList<IValueProvider> ValueProviders { get; set; }

        public static SectionConfiguration From()
        {
            return new SectionConfiguration();
        }
    }
}
