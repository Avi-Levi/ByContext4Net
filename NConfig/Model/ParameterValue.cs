using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig.Model
{
    public class ParameterValue : IHaveFilterReference
    {
        public override bool Equals(object obj)
        {
            ParameterValue other = obj as ParameterValue;
            if (other == null)
            {
                return false;
            }
            else
            {
                return this.Value.Equals(other.Value);
            }
        }
        public override int GetHashCode()
        {
            if (this.Value == null)
            {
                return 0;
            }
            else
            {
                return this.Value.GetHashCode();
            }
        }
        public override string ToString()
        {
            return this.Value ?? string.Empty;
        }

        private ParameterValue(string value)
        {
            this.Value = value;
            this.References = new List<ContextSubjectReference>();
        }
        public static ParameterValue Create(string value)
        {
            return new ParameterValue(value);
        }
        public string Value { get; private set; }
        public IList<ContextSubjectReference> References { get; private set; }
    }
}
