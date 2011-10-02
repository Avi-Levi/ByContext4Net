using System.Windows.Forms;

namespace Client.Views.Product
{
    public partial class ProductView : UserControl, IProductView
    {
        public ProductView()
        {
            InitializeComponent();
        }

        public void ShowProduct(Common.ProductFullDetails product)
        {
            this.lbl_description.Text = product.Description;
            this.label1.Text = product.Picture;
        }
    }
}
