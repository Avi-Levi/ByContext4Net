using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using System.Reflection;
using System.Linq.Expressions;

namespace NConfig
{
    public static class ParameterExtensions
    {
        public static Parameter FromPropertyInfo(this Parameter source, PropertyInfo pi)
        {
            source.Name = pi.Name;
            source.Parse = ConfigurationServiceBuilder.Instance.TypeBinders[pi.PropertyType];

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
        public static Parameter WithPolicy<TPolicy>(this Parameter source) where TPolicy : IFilterPolicy
        {
            source.Policy = Activator.CreateInstance<TPolicy>();
            return source;
        }
    }
}
