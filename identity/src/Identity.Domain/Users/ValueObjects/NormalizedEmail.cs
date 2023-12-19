namespace Identity.Domain.Users.ValueObjects;

public sealed record NormalizedEmail
{
    private NormalizedEmail(string value) => Value = value;

    public string Value { get; init; }

    public static NormalizedEmail Create(string email)
    {
        return new NormalizedEmail(email.ToUpper());
    }
}