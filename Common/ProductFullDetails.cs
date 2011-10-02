using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class ProductFullDetails : ProductPreviewDetails
    {
        public string Picture { get; set; }
    }
}
