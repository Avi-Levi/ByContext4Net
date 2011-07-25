using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class ProductFullDetails : ProductPreviewDetails
    {
        public string Picture { get; set; }
    }
}
