using System.ServiceModel;

namespace Common.Contracts
{
    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        LoginResponse Login(LoginRequest loginRequest);
    }
}
