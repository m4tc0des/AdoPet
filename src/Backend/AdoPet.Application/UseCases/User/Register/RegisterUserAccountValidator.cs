using AdoPet.Communication.Requests;
using AdoPet.Exception;
using FluentValidation;

namespace AdoPet.Application.UseCases.User.Register;

public class RegisterUserAccountValidator : AbstractValidator<RequestsRegisterUserJson>
{
    public RegisterUserAccountValidator()
    {
        RuleFor(user => user.UserName).NotEmpty().WithMessage(ResourceMessagesException.VALIDATION_USERNAME_REQUIRED);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.VALIDATION_EMAIL_REQUIRED);
        RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceMessagesException.VALIDATION_PASSWORD_REQUIRED);
        When(user => string.IsNullOrEmpty(user.Email) == false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.VALIDATION_EMAIL_INVALID);
        });
    }
}
