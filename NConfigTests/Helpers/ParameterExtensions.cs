using System;
using NConfig.Configuration;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using NConfig.Abstractions;
using NConfig.Impl;
using NConfig.Filter;
using NConfig.Filter.Rules;
using NConfig.Exceptions;
using NConfig;

namespace NConfig.Tests
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
