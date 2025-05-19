namespace SigmaBank.Core.Entities;

public class CardPrivateData
{
    public long Id { get; set; }

    public long CardId { get; set; }

    public DateTimeOffset ExpirationDate { get; set; }

    public string Code { get; set; } = string.Empty;
}