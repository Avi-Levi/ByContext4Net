using System;
using NConfig.Model;

namespace NConfig.Tests.Helpers
{
    public static class SectionExtensions
    {
        public static Section FromType<TSection>(this Section source) where TSection : class
        {
            return FromType(source, typeof(TSection));
        }

        public static Section FromType(this Section source, Type type)
        {
            source.TypeName = type.AssemblyQualifiedName;
            return source;
        }

        public static Section AddParameter(this Section source, Parameter parameter)
        {
            source.Parameters.Add(parameter.Name,parameter);
            return source;
        }
    }
}
