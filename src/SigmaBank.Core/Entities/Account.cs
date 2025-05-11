namespace SigmaBank.Core.Entities;

public class Account
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public decimal Balance { get; set; }
}