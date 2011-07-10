using System;
using NConfig.Model;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using NConfig.Abstractions;
using NConfig.Impl;
using NConfig.Filter;
using NConfig.Filter.Rules;

namespace NConfig
{
    public static class ParameterExtensions
    {
        public static Parameter FromPropertyInfo(this Parameter source, PropertyInfo pi)
        {
            source.Name = pi.Name;
            source.TypeName = pi.PropertyType.AssemblyQualifiedName;
            
            return source;
        }
        public static Parameter FromExpression<TSection,TProperty>(this Parameter source, Expression<Func<TSection, TProperty>> selector)
            where TSection : class
        {
            PropertyInfo pi = ((PropertyInfo)((MemberExpression)selector.Body).Member);
            source.FromPropertyInfo(pi);
            return source;
        }
        
        public static Parameter AddValue(this Parameter source, ParameterValue value)
        {
            source.Values.Add(value);
            return source;
        }

        public static IValueProvider ToValueProvider(this Parameter source, Configure config)
        {
            Type parameterType = Type.GetType(source.TypeName,true);
            var parseMethod = config.GetValueParser(parameterType);
            IFilterPolicy policy = GetFilterPolicyForParameter(source, parameterType, config);
            IValueProvider provider = new ValueProvider(source, parseMethod, policy);

            return provider;
        }

        private static IFilterPolicy GetFilterPolicyForParameter(Parameter parameter, Type parameterType, Configure config)
        {
            if (!string.IsNullOrEmpty(parameter.PolicyName))
            {
                return config.FilterPolicies[parameter.PolicyName];
            }
            else if (parameterType.IsAssignableFrom(typeof(IEnumerable)))
            {
                return config.FilterPolicies[Configure.DefaultCollectionFilterPolicyName];
            }
            else
            {
                return config.FilterPolicies[Configure.DefaultSingleValueFilterPolicyName];
            }
        }
    }
}
