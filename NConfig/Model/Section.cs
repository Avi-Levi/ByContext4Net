using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NConfig.Model
{
    [DataContract]
    public class Section
    {
        private Section()
        {
            this.Parameters = new Dictionary<string, Parameter>();
        }

        public static Section Create()
        {
            return new Section();
        }

        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public string ModelBinder { get; set; }
        [DataMember]
        public IDictionary<string, Parameter> Parameters { get; set; }
    }
}
