using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public bool IsSuccess { get; set; }
        [DataMember]
        public UserDetails User { get; set; }
    }
}
