using AdoPet.Communication.Requests;
using AdoPet.Exception.ExceptionsBase;

namespace AdoPet.Application.UseCases.User.Register;

public class RegisterUserAccountJseCase
{
    public void Excute(RequestsRegisterUserJson request)
    {
        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
