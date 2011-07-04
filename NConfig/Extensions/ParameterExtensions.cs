using System;
using NConfig.Model;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using NConfig.Abstractions;
using NConfig.Impl;

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

        public static IValueProvider ToValueProvider(this Parameter source)
        {
            Type parameterType = Type.GetType(source.TypeName,true);
            var parseMethod = Configure.Instance.GetValueParser(parameterType);
            IFilterPolicy policy = GetPolicyForParameter(source, parameterType);
            IValueProvider provider = new ValueProvider(source, parseMethod, policy);

            return provider;
        }

        private static IFilterPolicy GetPolicyForParameter(Parameter parameter,Type parameterType)
        {
            if (!string.IsNullOrEmpty(parameter.PolicyName))
            {
                return Configure.Instance.Policies[parameter.PolicyName];
            }
            else if (parameterType.IsAssignableFrom(typeof(IEnumerable)))
            {
                return Configure.Instance.Policies[Configure.DefaultCollectionFilterPolicyName];
            }
            else
            {
                return Configure.Instance.Policies[Configure.DefaultSingleValueFilterPolicyName];
            }
        }
    }
}
