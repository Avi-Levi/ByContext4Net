using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using NConfig;
using Common;
using Castle.MicroKernel.Registration;
using Client.Views.Login;
using Client.Views.Product;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Client.Views.Shell
{
    public class ShellController
    {
        #region properties
        public IShellView View { get; set; }
        public ILoginView LoginView { get; set; }
        public ProductsServiceAdapter ProductsAdapter { get; set; }
        public LoginServiceAdapter LoginAdapter { get; set; }
        public IProductView ProductView { get; set; }
        #endregion properties

        #region private methods
        private void loginView_Login(object sender, EventArgs e)
        {
            LoginRequest loginRequest = this.LoginView.GetLoginInfo();
            this.LoginAdapter.LoginUser(loginRequest, this.HandleLoginResponse);
        }

        private void HandleLoginResponse(LoginResponse response)
        {
            if (response.IsSuccess)
            {
                this.LoginView.Login -= this.loginView_Login;
                AsyncHelper.InvokeOnUI(()=>this.View.ShowUserDetails(response.User));
                this.ProductsAdapter.GetProductsPreviewForUser(response.User.Id, this.HandleGetProductsResponse);
            }
            else
            {
                if (MessageBox.Show("Login failed, try again?", "login failed",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    AsyncHelper.InvokeOnUI(() => this.LoginView.Clear());
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void HandleGetProductsResponse(ProductPreviewDetails[] products)
        {
            AsyncHelper.InvokeOnUI(()=>this.View.ShowProducts(products));
        }

        private void HandleGetProductResponse(ProductFullDetails product)
        {
            AsyncHelper.InvokeOnUI(() =>
                {
                    this.ProductView.ShowProduct(product);
                    this.View.ShowInMainWorkspace(this.ProductView);
                });
        }

        #endregion private methods

        #region public methods
        public void OnProductSelected(long selectedProductId)
        {
            this.ProductsAdapter.GetProduct(selectedProductId, this.HandleGetProductResponse);
        }

        public void OnViewLoad()
        {
            this.LoginView.Login += this.loginView_Login;
            this.View.ShowInMainWorkspace(this.LoginView);
        }
        #endregion public methods

        private byte[] ImageToByteArray(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Bmp);

            return ms.ToArray();
        }
        private Image ByteArrayToImage(byte[] array)
        {
            MemoryStream ms = new MemoryStream(array);
            return Image.FromStream(ms);
        }
    }
}
