using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Tests.Helpers
{
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
}
