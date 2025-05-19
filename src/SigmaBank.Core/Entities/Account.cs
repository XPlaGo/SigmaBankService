namespace SigmaBank.Core.Entities;

public class Account
{
    public long AccountId { get; set; }

    public long UserId { get; set; }

    public decimal Amount { get; set; }

    public IReadOnlyCollection<Card> Cards { get; set; } = [];
}