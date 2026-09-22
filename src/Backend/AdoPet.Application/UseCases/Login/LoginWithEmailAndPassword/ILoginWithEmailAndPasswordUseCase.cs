using AdoPet.Communication.Requests;
using AdoPet.Communication.Responses;

namespace AdoPet.Application.UseCases.Login.LoginWithEmailAndPassword;

public interface ILoginWithEmailAndPasswordUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
}
