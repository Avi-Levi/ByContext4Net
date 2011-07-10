using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using System.Linq.Expressions;
using NConfig.Abstractions;
using NConfig.Impl;

namespace NConfig
{
    public static class SectionExtensions
    {
        public static Section FromType<TSection>(this Section source) where TSection : class
        {
            return FromType(source, typeof(TSection));
        }

        public static Section FromType(this Section source, Type type)
        {
            source.Name = type.FullName;
            source.TypeName = type.AssemblyQualifiedName;
            return source;
        }

        public static Section AddParameter(this Section source, Parameter parameter)
        {
            source.Parameters.Add(parameter.Name,parameter);
            return source;
        }

        public static ISectionProvider ToSectionProvider(this Section source, Configure config)
        {
            Type sectionType = Type.GetType(source.TypeName,true);

            SectionProvider provider =
                new SectionProvider{SectionType = sectionType, ModelBinder = config.ModelBinder };


            source.Parameters.Values.ForEach
                (p => provider.ParameterValuesProviders.Add(p.Name, p.ToValueProvider(config)));

            return provider;
        }
    }
}
