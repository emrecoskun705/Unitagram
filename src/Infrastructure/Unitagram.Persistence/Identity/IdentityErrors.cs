using Unitagram.Domain.Shared;

namespace Unitagram.Persistence.Identity;

public static class IdentityErrors
{
    public static readonly Error DefaultError = new(
        "Identity.DefaultError",
        "DefaultError");

    public static readonly Error ConcurrencyFailure = new(
        "Identity.ConcurrencyFailure",
        "ConcurrencyFailure");

    public static readonly Error PasswordMismatch = new(
        "Identity.PasswordMismatch",
        "PasswordMismatch");

    public static readonly Error InvalidToken = new(
        "Identity.InvalidToken",
        "InvalidToken");

    public static readonly Error RecoveryCodeRedemptionFailed = new(
        "Identity.RecoveryCodeRedemptionFailed",
        "RecoveryCodeRedemptionFailed");

    public static readonly Error LoginAlreadyAssociated = new(
        "Identity.LoginAlreadyAssociated",
        "LoginAlreadyAssociated");

    public static readonly Error InvalidUserName = new(
        "Identity.InvalidUserName",
        "InvalidUserName");

    public static readonly Error InvalidEmail = new(
        "Identity.InvalidEmail",
        "InvalidEmail");

    public static readonly Error DuplicateUserName = new(
        "Identity.DuplicateUserName",
        "DuplicateUserName");

    public static readonly Error DuplicateEmail = new(
        "Identity.DuplicateEmail",
        "DuplicateEmail");

    public static readonly Error InvalidRoleName = new(
        "Identity.InvalidRoleName",
        "InvalidRoleName");

    public static readonly Error DuplicateRoleName = new(
        "Identity.DuplicateRoleName",
        "DuplicateRoleName");

    public static readonly Error UserAlreadyHasPassword = new(
        "Identity.UserAlreadyHasPassword",
        "UserAlreadyHasPassword");

    public static readonly Error UserLockoutNotEnabled = new(
        "Identity.UserLockoutNotEnabled",
        "UserLockoutNotEnabled");

    public static readonly Error UserAlreadyInRole = new(
        "Identity.UserAlreadyInRole",
        "UserAlreadyInRole");

    public static readonly Error UserNotInRole = new(
        "Identity.UserNotInRole",
        "UserNotInRole");

    public static readonly Error PasswordTooShort = new(
        "Identity.PasswordTooShort",
        "PasswordTooShort");

    public static readonly Error PasswordRequiresUniqueChars = new(
        "Identity.PasswordRequiresUniqueChars",
        "PasswordRequiresUniqueChars");

    public static readonly Error PasswordRequiresNonAlphanumeric = new(
        "Identity.PasswordRequiresNonAlphanumeric",
        "PasswordRequiresNonAlphanumeric");

    public static readonly Error PasswordRequiresDigit = new(
        "Identity.PasswordRequiresDigit",
        "PasswordRequiresDigit");

    public static readonly Error PasswordRequiresLower = new(
        "Identity.PasswordRequiresLower",
        "PasswordRequiresLower");

    public static readonly Error PasswordRequiresUpper = new(
        "Identity.PasswordRequiresUpper",
        "PasswordRequiresUpper");
}