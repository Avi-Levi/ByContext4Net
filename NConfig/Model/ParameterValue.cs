using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NConfig.Model
{
    [DataContract]
    public class ParameterValue
    {
        public override bool Equals(object obj)
        {
            var other = obj as ParameterValue;
            if (other == null)
            {
                return false;
            }
            return this.GetHashCode().Equals(other.GetHashCode());
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
