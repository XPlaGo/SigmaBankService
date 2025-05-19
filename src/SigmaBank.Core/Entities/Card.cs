namespace SigmaBank.Core.Entities;

public class Card
{
    public long CardId { get; set; }

    public long AccountId { get; set; }

    public string Number { get; set; } = string.Empty;

    public CardType Type { get; set; }
}