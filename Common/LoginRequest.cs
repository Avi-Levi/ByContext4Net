using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
