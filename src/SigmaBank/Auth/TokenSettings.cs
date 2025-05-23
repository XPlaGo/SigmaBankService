﻿namespace SigmaBank.Auth;

internal class TokenSettings
{
    public TokenSettings() { }

    public TokenSettings(
        string issuer,
        string audience,
        long lifeTimeInSeconds,
        string key)
    {
        Issuer = issuer;
        Audience = audience;
        LifeTimeInSeconds = lifeTimeInSeconds;
        Key = key;
    }

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public long LifeTimeInSeconds { get; set; }

    public string Key { get; set; } = string.Empty;
}