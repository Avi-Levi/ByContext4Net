using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;
using System.Runtime.Serialization;

namespace NConfig.Configuration
{
    [DataContract]
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
                return this.GetHashCode().Equals(other.GetHashCode());
            }
        }
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
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
        [DataMember]
        public string Value { get; private set; }
        [DataMember]
        public IList<ContextSubjectReference> References { get; private set; }
    }
}
