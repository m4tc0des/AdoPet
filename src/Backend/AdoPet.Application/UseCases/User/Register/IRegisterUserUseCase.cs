using AdoPet.Communication.Requests;

namespace AdoPet.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    void Execute(RequestsRegisterUserJson request);
}
