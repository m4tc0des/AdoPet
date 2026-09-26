using AdoPet.Application.UseCases.User.Register;
using AdoPet.Exception;
using CommonTestUtilities.ClassDataGenerator;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.User.Register;

public class RegisterUserAccountValidatorTests
{
    [Fact]
    public void Success()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [ClassData(typeof(ErrorsGenerator))]
    public void Validate_ShouldHaveError_When_UserNameIsEmpty(string userName)
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        request.UserName = userName;

        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();

        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_USERNAME_REQUIRED));
        });
    }

    [Theory]
    [ClassData(typeof(ErrorsGenerator))]
    public void Validate_ShouldHaveError_When_EmailIsEmpty(string email)
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        request.Email = email;

        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();

        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_EMAIL_REQUIRED));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_When_EmailIsInvalid()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        request.Email = "invalidemail.com";

        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();

        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_EMAIL_INVALID));
        });
    }

    [Theory]
    [ClassData(typeof(ErrorsGenerator))]
    public void Validate_ShouldHaveError_When_PasswordIsEmpty(string password)
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        request.Password = password;

        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();

        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_PASSWORD_REQUIRED));
        });
    }
}
