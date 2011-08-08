using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Server.Data
{
    public class UsersDal
    {
        private readonly Dictionary<Tuple<string, string>, UserDetails> usersRegistrations =
            new Dictionary<Tuple<string, string>, UserDetails>()
        {
            {new Tuple<string,string>("johan","111111"),new UserDetails { Id = 0, FirstName = "Johan", LastName = "Smith" }},
            {new Tuple<string,string>("greg","123456"),new UserDetails { Id = 1, FirstName = "Greg", LastName = "Yung" }},
            {new Tuple<string,string>("Barak","222222"),new UserDetails { Id = 2, FirstName = "Barak", LastName = "Obama" }},
        };

        public UserDetails GetUser(string userName, string password)
        {
            var key = new Tuple<string, string>(userName, password);
            UserDetails result = null;
            this.usersRegistrations.TryGetValue(key, out result);
            return result;
        }

    }
}
