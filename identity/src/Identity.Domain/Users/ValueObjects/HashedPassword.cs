namespace Identity.Domain.Users.ValueObjects;

public sealed record HashedPassword
{
    private HashedPassword(string value) => Value = value;

    public string Value { get; init; }

    public static HashedPassword Create(string password)
    {
        return new HashedPassword(password);
    }
    
    public static HashedPassword FromValue(string Value) => new HashedPassword(Value);
}