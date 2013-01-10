using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NConfig.Model
{
    /// <summary>
    /// Represents data item, which we want to have a different value
    /// according to the context provided at runtime by the caller.
    /// </summary>

    [DataContract]
    public class Parameter
    {
        public Parameter()
        {
            this.Values = new List<ParameterValue>();
        }

        [DataMember]
        public string Translator { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public string PolicyName { get; set; }
        [DataMember]
        public string Required { get; set; }

        [DataMember]
        public IList<ParameterValue> Values { get; set; }
    }
}
