using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig.Configuration
{
    public class SectionConfiguration : ISectionProvider
    {
        private SectionConfiguration()
        {}

        public string Name { get; private set; }
        public string TypeName { get; private set; }

        public static SectionConfiguration FromType<TSection>() where TSection : class
        {
            return FromType(typeof(TSection));
        }

        public static SectionConfiguration FromType(Type type)
        {
            return new SectionConfiguration{Name = type.FullName, TypeName= type.AssemblyQualifiedName};
        }
        public abstract object Get(IDictionary<string, string> runtimeContext);
    }
}
