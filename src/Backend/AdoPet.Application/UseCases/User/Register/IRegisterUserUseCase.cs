using AdoPet.Communication.Requests;

namespace AdoPet.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task Execute(RequestsRegisterUserJson request);
}
