using AdoPet.Communication.Requests;
using AdoPet.Exception.ExceptionsBase;
using Mapster;

namespace AdoPet.Application.UseCases.User.Register;

public class RegisterUserAccountJseCase
{
    public void Excute(RequestsRegisterUserJson request)
    {
        ValidateAndThrowOnValidation(request);

        var user = request.Adapt<Domain.Entities.User>();
    }

    public void ValidateAndThrowOnValidation(RequestsRegisterUserJson request)
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
