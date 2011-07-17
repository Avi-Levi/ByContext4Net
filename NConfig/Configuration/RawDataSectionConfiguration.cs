using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using NConfig.Abstractions;
using NConfig.Model;

namespace NConfig.Configuration
{
    public static class SectionConfigurationExtensions
    {
        public static ParameterConfiguration AddParameter(this SectionConfiguration source)
        {
            return new ParameterConfiguration { ParentSection = source};
        }
    }
    public class RawValueConfiguration : IHaveFilterReference
    {
        public RawValueConfiguration(ParameterConfiguration parent, string rawValue)
        {
            this.Parent = parent;
            this.RawValue = rawValue;
            this.References = new List<ContextSubjectReference>();
        }
        public ParameterConfiguration Parent { get; private set; }
        public string RawValue { get; private set; }

        public IList<ContextSubjectReference> References { get; private set; }
    }
    public static class RawDataSectionConfigurationExtensions
    {
        public static RawValueConfiguration AddRawValue(this ParameterConfiguration source, string value)
        {
            return new RawValueConfiguration (source, value );
        }
    }
}
