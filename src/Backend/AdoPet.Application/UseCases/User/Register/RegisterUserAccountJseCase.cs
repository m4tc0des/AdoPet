using AdoPet.Communication.Requests;
using AdoPet.Domain.Security.PasswordHashing;
using AdoPet.Exception.ExceptionsBase;
using Mapster;

namespace AdoPet.Application.UseCases.User.Register;

public class RegisterUserAccountJseCase : IRegisterUserUseCase
{
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserAccountJseCase(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public void Execute(RequestsRegisterUserJson request)
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
