using AdoPet.Communication.Requests;

namespace AdoPet.Application.UseCases.User.Register;

public class RegisterUserAccountJseCase
{
    public void Excute(RequestsRegisterUserJson request)
    {
        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);
    }
}
