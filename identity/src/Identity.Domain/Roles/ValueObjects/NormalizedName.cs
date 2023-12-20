namespace Identity.Domain.Roles.ValueObjects;

public sealed record NormalizedName
{
    private NormalizedName(string value) => Value = value;

    public string Value { get; init; }

    public static NormalizedName Create(string name)
    {
        return new NormalizedName(name.ToUpper());
    }
}