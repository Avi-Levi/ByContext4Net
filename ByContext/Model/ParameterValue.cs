using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ByContext.Model
{
    [DataContract]
    public class ParameterValue
    {
        public ParameterValue()
        {
            this.FilterConditions = new List<FilterCondition>();
        }
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

        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public IList<FilterCondition> FilterConditions { get; private set; }
    }
}
