using AdoPet.Communication.Requests;
using AdoPet.Communication.Responses;

namespace AdoPet.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestsRegisterUserJson request);
}
