using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Client.Views.Product
{
    public interface IProductView
    {
        void ShowProduct(ProductFullDetails product);
    }
}
