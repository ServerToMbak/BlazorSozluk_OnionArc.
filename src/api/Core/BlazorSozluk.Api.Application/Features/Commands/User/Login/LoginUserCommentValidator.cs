using BlazorSozluk.Common.Models.RequestModels;
using FluentValidation;
using FluentValidation.Validators;

namespace BlazorSozluk.Api.Application.Features.Commands.User;

public class LoginUserCommentValidator :AbstractValidator<LoginUserCommand>
{
    public LoginUserCommentValidator()
    {
        RuleFor(i => i.EmailAddress)
            .NotNull()
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{PropertyName} not a valid email address ");

        RuleFor(i => i.Password)
            .NotNull()
            .MinimumLength(6).WithMessage("{PropertyName} should at least be {MinLength} characters");
    }
}
