using SigmaBank.Core.Entities;

namespace SigmaBank.Core.Repositories.CardsPrivateData;

public interface ICardsPrivateDataRepository
{
    public Task<CardPrivateData> GetCardPrivateDataByCardIdAsync(long cardId, CancellationToken cancellationToken);
}