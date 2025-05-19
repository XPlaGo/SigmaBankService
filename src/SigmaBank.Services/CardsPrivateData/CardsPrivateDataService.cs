using SigmaBank.Core.Entities;
using SigmaBank.Core.Repositories.CardsPrivateData;
using SigmaBank.Core.Services.CardsPrivateData;

namespace SigmaBank.Services.CardsPrivateData;

public class CardsPrivateDataService(ICardsPrivateDataRepository cardsPrivateDataRepository) : ICardsPrivateDataService
{
    public async Task<CardPrivateData> GetCardPrivateDataByCardIdAsync(long cardId, CancellationToken cancellationToken)
    {
        return await cardsPrivateDataRepository.GetCardPrivateDataByCardIdAsync(cardId, cancellationToken);
    }
}