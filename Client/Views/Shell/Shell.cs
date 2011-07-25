using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Client.Views.Shell;
using Client.Views;
using Common;

namespace Client
{
    public partial class Shell : Form,IShellView
    {
        public Shell()
        {
            InitializeComponent();
        }

        private ShellController _controller = null;

        public ShellController Controller
        {
            get { return _controller; }
            set 
            { 
                _controller = value;
                _controller.View = this;
            }
        }

        public void ShowUserDetails(Common.UserDetails user)
        {
            this.label1.Text = "Wellcom back " + user.FirstName + " " + user.LastName;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Controller.OnViewLoad();
        }
        public void ShowProducts(ProductPreviewDetails[] products)
        {
            foreach (var product in products)
            {
                this.lb_products.Items.Add(product);
            }
        }

        public void ShowInMainWorkspace(object view)
        {
            Control ctrl = (Control)view;
            this.pnl_mainWorkspace.Controls.Clear();
            this.pnl_mainWorkspace.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill; 
        }

        private void lb_products_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox senderLB = (ListBox)sender;
            var selectedProduct = (ProductPreviewDetails)senderLB.SelectedItem;
            this.Controller.OnProductSelected(selectedProduct.Id);
        }
    }
}
