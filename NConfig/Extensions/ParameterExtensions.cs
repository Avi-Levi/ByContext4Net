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
        public static Parameter FromExpression<TSection>(this Parameter source, Expression<Func<TSection, object>> selector)
            where TSection : class
        {
            MemberExpression s = ((MemberExpression)((UnaryExpression)selector.Body).Operand);
            PropertyInfo pi = ((PropertyInfo)s.Member);
            source.FromPropertyInfo(pi);
            return source;
        }
        
        public static Parameter AddValue(this Parameter source, ParameterValue value)
        {
            source.Values.Add(value);
            return source;
        }
    }
}
