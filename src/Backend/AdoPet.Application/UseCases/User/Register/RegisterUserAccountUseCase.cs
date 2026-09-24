using AdoPet.Communication.Requests;
using AdoPet.Communication.Responses;
using AdoPet.Domain.Repositories;
using AdoPet.Domain.Repositories.User;
using AdoPet.Domain.Security.PasswordHashing;
using AdoPet.Domain.Security.Tokens;
using AdoPet.Exception;
using AdoPet.Exception.ExceptionsBase;
using Mapster;

namespace AdoPet.Application.UseCases.User.Register;

public class RegisterUserAccountUseCase : IRegisterUserUseCase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public RegisterUserAccountUseCase(IPasswordHasher passwordHasher,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IUserReadOnlyRepository userReadOnlyRepository,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _passwordHasher = passwordHasher;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _userReadOnlyRepository = userReadOnlyRepository;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestsRegisterUserJson request)
    {
        await ValidateAndThrowOnValidation(request);

        var user = request.Adapt<Domain.Entities.User>();

        user.Password = _passwordHasher.HashPassword(request.Password);

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisterUserJson
        {
            UserName = user.UserName,
            Tokens = new ResponseTokensJson()
            {
                AccessToken = _accessTokenGenerator.Generate(user)
            }
        };
    }

    private async Task ValidateAndThrowOnValidation(RequestsRegisterUserJson request)
    {
        var validator = new RegisterUserAccountValidator();

        var result = await validator.ValidateAsync(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

        var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            throw new ErrorOnValidationException(new List<string> { ResourceMessagesException.VALIDATION_EMAIL_ALREADY_EXISTS });
        }
    }
}
