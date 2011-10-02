using System;
using Common;
using Common.Contracts;

namespace Client
{
    public class ProductsServiceAdapter
    {
        public ProductsServiceAdapter(ProxyFactory proxyFactory, LoggerFactory loggerFactory)
        {
            this.Factory = proxyFactory;
            this.Logger = loggerFactory.Create(this.GetType());
        }

        private ILogger Logger { get; set; }
        private ProxyFactory Factory { get; set; }

        public void GetProductsPreviewForUser(long userId, Action<ProductPreviewDetails[]> callback)
        {
            AsyncHelper.InvokeOnBackground(() =>
                {
                    try
                    {
                        this.Logger.Write("enter 'LoginServiceAdapter.LoginUser'", LogLevelOption.Trace);

                        ProductPreviewDetails[] products = this.Factory.Get<IProductsService>().GetProductsForUser(userId);

                        callback(products);
                    }
                    finally
                    {
                        this.Logger.Write("exit 'LoginServiceAdapter.LoginUser'", LogLevelOption.Trace);
                    }
                });
        }

        public void GetProduct(long productId, Action<ProductFullDetails> callback)
        {
            AsyncHelper.InvokeOnBackground(() =>
                {
                    try
                    {
                        this.Logger.Write("enter 'LoginServiceAdapter.LoginUser'", LogLevelOption.Trace);

                        ProductFullDetails product = this.Factory.Get<IProductsService>().GetProduct(productId);

                        callback(product);
                    }
                    finally
                    {
                        this.Logger.Write("exit 'LoginServiceAdapter.LoginUser'", LogLevelOption.Trace);
                    }
                });
        }
    }
}
