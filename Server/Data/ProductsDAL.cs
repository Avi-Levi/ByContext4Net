using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Server.Data
{
    public class ProductsDAL
    {
        private readonly List<ProductFullDetails> _products = new List<ProductFullDetails>() 
        {
            new ProductFullDetails{Id=0,Description="some book",Picture = "Picture"},
            new ProductFullDetails{Id=1,Description="some laptop",Picture = "Picture"},
            new ProductFullDetails{Id=2,Description="ipad",Picture = "Picture"},
            new ProductFullDetails{Id=3,Description="iphone",Picture = "Picture"},
            new ProductFullDetails{Id=4,Description="some cell phone",Picture = "Picture"},
            new ProductFullDetails{Id=5,Description="a set of speakers",Picture = "Picture"},
            new ProductFullDetails{Id=6,Description="some pc",Picture = "Picture"},
            new ProductFullDetails{Id=7,Description="some car",Picture = "Picture"},
            new ProductFullDetails{Id=8,Description="some tv",Picture = "Picture"},
        };

        private readonly List<Tuple<long, long>> productsToUserMap = new List<Tuple<long, long>>() 
        {
            new Tuple<long, long>(0,0),
            new Tuple<long, long>(0,1),
            new Tuple<long, long>(0,2),
            new Tuple<long, long>(1,1),
            new Tuple<long, long>(1,8),
            new Tuple<long, long>(1,3),
            new Tuple<long, long>(1,4),
            new Tuple<long, long>(1,5),
            new Tuple<long, long>(1,6),
            new Tuple<long, long>(2,7),
            new Tuple<long, long>(2,8),
            new Tuple<long, long>(2,1),
            new Tuple<long, long>(2,2),
            new Tuple<long, long>(2,5),
            new Tuple<long, long>(2,4),
        };

        public ProductPreviewDetails[] GetProductsForUser(long userId)
        {
            var mapQuery = this.productsToUserMap.Where(x => x.Item1 == userId);
            var productsQuery = this._products.Where(x => mapQuery.Any(m => m.Item2 == x.Id)).Select(p => new
                ProductPreviewDetails { Id = p.Id, Description = p.Description });

            return productsQuery.ToArray();
        }
        public ProductFullDetails GetProduct(long productId)
        {
            if (!this._products.Where(x => x.Id == productId).Any())
            {
                throw new ArgumentOutOfRangeException("invalid product id: " + productId.ToString());
            }

            return this._products.Where(x => x.Id == productId).Single();
        }
    }
}
