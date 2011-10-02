using System;
using System.Linq.Expressions;
using System.Reflection;
using NConfig.Extensions;
using NConfig.Model;

namespace NConfig.Tests.Helpers
{
    public static class ParameterExtensions
    {
        public static Parameter WithTranslator(this Parameter source, string translatorName)
        {
            source.Translator = translatorName;
            return source;
        }

        public static Parameter FromPropertyInfo(this Parameter source, PropertyInfo pi)
        {
            source.Name = pi.Name;
            source.TypeName = pi.PropertyType.AssemblyQualifiedName;
            
            return source;
        }
        public static Parameter FromExpression<TSection,TProperty>(this Parameter source, Expression<Func<TSection, TProperty>> selector)
            where TSection : class
        {
            source.FromPropertyInfo(selector.ToPropertyInfo<TSection,TProperty>());
            return source;
        }
        
        public static Parameter AddValue(this Parameter source, ParameterValue value)
        {
            source.Values.Add(value);
            return source;
        }
    }
}
