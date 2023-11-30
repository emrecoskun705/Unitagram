using System.Text.RegularExpressions;
using FluentValidation;

namespace Unitagram.Application.Users.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();

        RuleFor(c => c.LastName).NotEmpty();
        
        RuleFor(a => a.Email)
            .EmailAddress().WithMessage("EmailInvalidFormat")
            .NotEmpty().WithMessage("EmailCantBlank")
            .NotNull();

        RuleFor(a => a.UserName)
            .NotEmpty().WithMessage("UsernameShouldntEmpty")
            .NotNull()
            .MinimumLength(4).WithMessage("UsernameMinLength")
            .MaximumLength(15).WithMessage("UsernameMaxLength")
            .Must(ValidUsername).WithMessage("InvalidUsername");

        // RuleFor(a => a.PhoneNumber)
        //     .Must(OnlyDigits).WithMessage("PhoneNumberOnlyDigit");

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage("PasswordShouldntEmpty")
            .MinimumLength(8).WithMessage("PasswordMinLength")
            .MaximumLength(50).WithMessage("PasswordMaxLength")
            .Must(RequireUppercase).WithMessage("PasswordContainUppercase")
            .Must(RequireLowercase).WithMessage("PasswordContainLowercase");

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage("ConfirmPasswordCantBlank")
            .Equal(a => a.Password).WithMessage("PasswordNotEquals");
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
    
    private bool OnlyDigits(string arg)
    {
        if (Regex.IsMatch(arg, "^[0-9]*$"))
            return true;

        return false;
    }

    private bool ValidUsername(string arg)
    {
        if (Regex.IsMatch(arg, "^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$"))
            return true;

        return false;
    }
}