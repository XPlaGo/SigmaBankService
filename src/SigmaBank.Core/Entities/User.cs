﻿namespace SigmaBank.Core.Entities;

public record User
{
    public long UserId { get; init; }

    public string PhoneNumber { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public int Age { get; init; }
}