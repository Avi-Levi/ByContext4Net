using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using System.Linq.Expressions;

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
            return source;
        }
        public static Section AddParameter(this Section source, Parameter parameter)
        {
            source.Parameters.Add(parameter);
            return source;
        }
    }
}
