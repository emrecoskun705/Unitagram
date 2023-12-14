using System.Text.RegularExpressions;
using FluentValidation;
using Unitagram.Application.Contracts.Common;

namespace Unitagram.Application.Users.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(a => a.Email)
            .EmailAddress().WithMessage(localizationService["EmailInvalidFormat"])
            .NotEmpty().WithMessage(localizationService["EmailCantBlank"])
            .NotNull();

        RuleFor(a => a.UserName)
            .NotEmpty().WithMessage(localizationService["UsernameShouldntEmpty"])
            .NotNull()
            .MinimumLength(4).WithMessage(localizationService["UsernameMinLength"])
            .MaximumLength(15).WithMessage(localizationService["UsernameMaxLength"])
            .Must(ValidUsername).WithMessage(localizationService["InvalidUsername"]);
        

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage(localizationService["PasswordShouldntEmpty"])
            .MinimumLength(8).WithMessage(localizationService["PasswordMinLength"])
            .MaximumLength(50).WithMessage(localizationService["PasswordMaxLength"])
            .Must(RequireUppercase).WithMessage(localizationService["PasswordContainUppercase"])
            .Must(RequireLowercase).WithMessage(localizationService["PasswordContainLowercase"]);

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage(localizationService["ConfirmPasswordCantBlank"])
            .Equal(a => a.Password).WithMessage(localizationService["PasswordNotEquals"]);
    }
    
    private bool RequireUppercase(string args)
    {
        foreach (char c in args)
        {
            if (char.IsUpper(c))
                return true;
        }

        return false;
    }
    
    private bool RequireLowercase(string args)
    {
        foreach (char c in args)
        {
            if (char.IsLower(c))
                return true;
        }

        return false;
    }

    private bool ValidUsername(string arg)
    {
        if (Regex.IsMatch(arg, "^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$"))
            return true;

        return false;
    }
}