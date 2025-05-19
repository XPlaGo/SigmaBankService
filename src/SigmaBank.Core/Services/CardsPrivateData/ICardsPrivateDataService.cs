using SigmaBank.Core.Entities;

namespace SigmaBank.Core.Services.CardsPrivateData;

public interface ICardsPrivateDataService
{
    public Task<CardPrivateData> GetCardPrivateDataByCardIdAsync(long cardId, CancellationToken cancellationToken);
}