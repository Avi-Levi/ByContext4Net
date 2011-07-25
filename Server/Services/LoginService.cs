using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Server.Data;

namespace Server.Services
{
    public class LoginService : ILoginService
    {
        public LoginService(LoggerFactory loggerFactory, UsersDal dal)
        {
            this.Logger = loggerFactory.Create(this.GetType());
            this.DAL = dal;
        }

        private UsersDal DAL { get; set; }
        private ILogger Logger { get; set; }

        public LoginResponse Login(LoginRequest loginRequest)
        {
            UserDetails user = this.DAL.GetUser(loginRequest.UserName,loginRequest.Password);
            var response = new LoginResponse { IsSuccess = user != null, User = user };
            
            this.Logger.Write(string.Format("login for user: {0}, has {1}",
                loginRequest.UserName, response.IsSuccess ? "succeed" : "failed"),LogLevelOption.Trace);

            return response;
        }
    }
}
