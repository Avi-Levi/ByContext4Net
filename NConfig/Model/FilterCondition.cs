using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NConfig.Model
{
    [DataContract]
    public class FilterCondition
    {
        private FilterCondition(string conditionName, Dictionary<string,string> properties)
        {
            this.ConditionName = conditionName;
            this.Properties = properties;
        }

        public static FilterCondition Create(string conditionName, Dictionary<string, string> properties)
        {
            return new FilterCondition(conditionName,properties);
        }

        [DataMember]
        public string ConditionName { get; private set; }
        [DataMember]
        public Dictionary<string, string> Properties { get; private set; }
    }
}
