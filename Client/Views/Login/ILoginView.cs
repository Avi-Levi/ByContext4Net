using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Client.Views.Login
{
    public interface ILoginView
    {
        LoginRequest GetLoginInfo();

        void Clear();

        event EventHandler Login;
    }
}
