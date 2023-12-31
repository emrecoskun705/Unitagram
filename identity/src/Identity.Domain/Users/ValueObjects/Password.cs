﻿namespace Identity.Domain.Users.ValueObjects;

public sealed record Password
{
    private Password(string value) => Value = value;

    public string Value { get; init; }
    
    public static Password FromValue(string Value) => new Password(Value);
}