namespace Identity.Domain.Users.ValueObjects;

public sealed record NormalizedUsername
{
    private NormalizedUsername(string value) => Value = value;

    public string Value { get; init; }

    public static NormalizedUsername Create(string username)
    {
        return new NormalizedUsername(username.ToUpper());
    }
}