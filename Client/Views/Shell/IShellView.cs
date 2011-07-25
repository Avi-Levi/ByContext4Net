using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Views.Shell
{
    public interface IShellView
    { 
        void ShowUserDetails(Common.UserDetails user);
        void ShowProducts(Common.ProductPreviewDetails[] products);

        void ShowInMainWorkspace(object view);
    }
}
