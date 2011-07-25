using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Server.Data;

namespace Server.Services
{
    public class ProductsService : IProductsService
    {
        public ProductsService(LoggerFactory loggerFactory, ProductsDAL dal)
        {
            this.Logger = loggerFactory.Create(this.GetType());
            this.DAL = dal;
        }
        private ProductsDAL DAL { get; set; }
        private ILogger Logger { get; set; }

        public ProductPreviewDetails[] GetProductsForUser(long userId)
        {
            var result = this.DAL.GetProductsForUser(userId);

            this.Logger.Write(result.Count() + " products returned for user id: " + userId.ToString(), LogLevelOption.Trace);
            
            return result;
        }

        public ProductFullDetails GetProduct(long productId)
        {
            var result = this.DAL.GetProduct(productId);

            return result;
        }
    }
}
