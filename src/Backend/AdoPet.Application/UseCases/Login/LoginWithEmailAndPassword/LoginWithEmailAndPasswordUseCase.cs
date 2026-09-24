using AdoPet.Communication.Requests;
using AdoPet.Communication.Responses;
using AdoPet.Domain.Repositories.User;
using AdoPet.Domain.Security.PasswordHashing;
using AdoPet.Domain.Security.Tokens;
using AdoPet.Exception.ExceptionsBase;

namespace AdoPet.Application.UseCases.Login.LoginWithEmailAndPassword;

public class LoginWithEmailAndPasswordUseCase : ILoginWithEmailAndPasswordUseCase
{
    private readonly IUserReadOnlyRepository _userReadRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public LoginWithEmailAndPasswordUseCase(IUserReadOnlyRepository userReadRepository, IPasswordHasher passwordHasher, IAccessTokenGenerator accessTokenGenerator)
    {
        _userReadRepository = userReadRepository;
        _passwordHasher = passwordHasher;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
    {
        var user = await _userReadRepository.GetByEmail(request.Email);

        if (user is null) throw new InvalidLoginException();

        var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.Password);

        if (isPasswordValid == false) throw new InvalidLoginException();

        return new ResponseRegisterUserJson
        {
            UserName = user.UserName,
            Tokens = new ResponseTokensJson()
            {
                AccessToken = _accessTokenGenerator.Generate(user)
            }
        };
    }
}
