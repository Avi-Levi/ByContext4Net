using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Model
{
    public class ContextSubjectReference
    {
        public const string ALL = "ALL";
        private ContextSubjectReference(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public static ContextSubjectReference Create(string name, string value)
        {
            return new ContextSubjectReference(name, value);
        }

        public string Name { get; private set; }
        public string Value { get; private set; }

        public override string ToString()
        {
            return this.Name + ":" + this.Value;
        }
    }
}
