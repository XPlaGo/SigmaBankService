namespace SigmaBank.Core.Entities;

public class Card
{
    public long Id { get; set; }

    public long AccountId { get; set; }

    public CardType CardType { get; set; }

    public DateTimeOffset ExpiryDate { get; set; }

    public string Code { get; set; } = string.Empty;
}