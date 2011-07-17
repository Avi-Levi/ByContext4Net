using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;
using System.Reflection;
using System.Linq.Expressions;

namespace NConfig.Configuration
{
    public class ParameterConfiguration
    {
        public SectionConfiguration ParentSection { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }

        public ParameterConfiguration FromPropertyInfo(PropertyInfo pi)
        {
            this.Name = pi.Name;
            this.TypeName = pi.PropertyType.AssemblyQualifiedName;

            return this;
        }
        public ParameterConfiguration FromExpression<TSection, TProperty>(Expression<Func<TSection, TProperty>> selector)
            where TSection : class
        {
            PropertyInfo pi = ((PropertyInfo)((MemberExpression)selector.Body).Member);
            this.FromPropertyInfo(pi);
            return this;
        }
    }
}
