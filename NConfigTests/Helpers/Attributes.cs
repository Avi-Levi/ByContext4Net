using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Tests.Helpers
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RuntimeContextItemAttribute : Attribute
    {
        public RuntimeContextItemAttribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RuntimeContextItemToFilterByAttribute : Attribute
    {
        public RuntimeContextItemToFilterByAttribute(string name, string value)
        {
            this.Item = new KeyValuePair<string, string>(name, value);
        }

        public KeyValuePair<string,string> Item { get; set; }
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class FilterPolicyAttribute : Attribute
    {
        public FilterPolicyAttribute(params Type[] rules)
        {
            this.Rules = rules;
        }

        public Type[] Rules { get; set; }
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ParameterValuesTypeAttribute : Attribute
    {
        public ParameterValuesTypeAttribute(Type containerClassType)
        {
            this.ContainerClassType = containerClassType;
        }

        public Type ContainerClassType { get; set; }
    }


}
