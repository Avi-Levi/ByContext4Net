using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IProductsService
    {
        [OperationContract]
        ProductPreviewDetails[] GetProductsForUser(long userId);

        [OperationContract]
        ProductFullDetails GetProduct(long productId);
    }
}
