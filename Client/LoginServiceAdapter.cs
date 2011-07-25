using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.ServiceModel;
using NConfig;
using System.ServiceModel.Channels;

namespace Client
{
    public class LoginServiceAdapter
    {
        public LoginServiceAdapter(ProxyFactory proxyFactory, LoggerFactory loggerFactory)
        {
            this.Factory = proxyFactory;
            this.Logger = loggerFactory.Create(this.GetType());
        }
        private ILogger Logger { get; set; }
        private ProxyFactory Factory { get; set; }

        public void LoginUser(LoginRequest loginRequest, Action<LoginResponse> callback)
        {
            ILoginService proxy = this.Factory.Get<ILoginService>();
            AsyncHelper.InvokeOnBackground(()=>
                {
                    try
                    {
                        this.Logger.Write("enter 'LoginServiceAdapter.LoginUser'", LogLevelOption.Trace);

                        LoginResponse response = proxy.Login(loginRequest);

                        callback(response);
                    }
                    finally
                    {
                        this.Logger.Write("exit 'LoginServiceAdapter.LoginUser'", LogLevelOption.Trace);
                    }
                });
        }
    }
}
