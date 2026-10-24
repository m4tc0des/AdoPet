using AdoPet.Application.UseCases.User.Register;
using AdoPet.Exception;
using AdoPet.Exception.ExceptionsBase;
using CommonTestUtilities.ClassDataGenerator;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security;
using Shouldly;

namespace UseCases.Tests.User.Register;

public class RegisterUserAccountUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.UserName.ShouldBe(request.UserName);
        result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
        result.Tokens.RefreshToken.ShouldBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(ErrorsGenerator))]
    public async Task Validate_ShouldThrowException_When_UserNameIsEmpty(string userName)
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        request.UserName = userName;

        var useCase = CreateUseCase();

        var exception = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorsMessages().ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(ResourceMessagesException.VALIDATION_USERNAME_REQUIRED);
        });
    }

    [Fact]
    public async Task Validate_ShouldThrowException_When_EmailAlreadyExists()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase(emailThatsAlreadExists: request.Email);

        var exception = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorsMessages().ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(ResourceMessagesException.VALIDATION_EMAIL_ALREADY_EXISTS);
        });
    }

    private RegisterUserAccountUseCase CreateUseCase(string? emailThatsAlreadExists = null)
    {
        var unitOfWork = IUnitOfWorkBuilder.Build();

        var userWriteOnlyRepository = IUserWriteOnlyRepositoryBuilder.Build();

        var userReadOnlyRepository = new IUserReadOnlyRepositoryBuilder();

        var passwordHasher = new IPasswordHasherBuilder().Build();

        var acessTokenGenerateBuilder = IAccessTokenGeneratorBuilder.Build();

        if (!string.IsNullOrEmpty(emailThatsAlreadExists))
        {
            userReadOnlyRepository.ExistActiveUserWithEmail(emailThatsAlreadExists);
        }

        return new RegisterUserAccountUseCase(passwordHasher, userWriteOnlyRepository, unitOfWork, userReadOnlyRepository.Build(), acessTokenGenerateBuilder);
    }
}
