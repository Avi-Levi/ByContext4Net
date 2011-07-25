using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class ProductPreviewDetails
    {
        public override string ToString()
        {
            return this.Description;
        }
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
