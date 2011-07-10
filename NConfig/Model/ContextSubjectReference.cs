using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Model
{
    public class ContextSubjectReference
    {
        public const string ALL = "ALL";
        public ContextSubjectReference(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }

        public override string ToString()
        {
            return this.Name + ":" + this.Value;
        }
    }
}
