using System.ServiceModel;

namespace Common.Contracts
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
