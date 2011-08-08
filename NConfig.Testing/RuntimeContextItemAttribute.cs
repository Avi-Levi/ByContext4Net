using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Testing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RuntimeContextItemAttribute : Attribute
    {
        public RuntimeContextItemAttribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        public RuntimeContextItemAttribute(string name, Type ownerType)
        {
            this.Name = name;
            this.Value = ownerType.FullName;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
