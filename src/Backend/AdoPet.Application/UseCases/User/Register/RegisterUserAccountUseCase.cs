using AdoPet.Communication.Requests;
using AdoPet.Domain.Repositories.User;
using AdoPet.Domain.Security.PasswordHashing;
using AdoPet.Exception.ExceptionsBase;
using Mapster;

namespace AdoPet.Application.UseCases.User.Register;

public class RegisterUserAccountUseCase : IRegisterUserUseCase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

    public RegisterUserAccountUseCase(IPasswordHasher passwordHasher, IUserWriteOnlyRepository userWriteOnlyRepository)
    {
        _passwordHasher = passwordHasher;
        _userWriteOnlyRepository = userWriteOnlyRepository;
    }

    public async Task Execute(RequestsRegisterUserJson request)
    {
        ValidateAndThrowOnValidation(request);

        var user = request.Adapt<Domain.Entities.User>();

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        await _userWriteOnlyRepository.Add(user);
    }

    private void ValidateAndThrowOnValidation(RequestsRegisterUserJson request)
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
