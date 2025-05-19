using Dapper;
using SigmaBank.Core.Entities;
using SigmaBank.Core.Repositories.CardsPrivateData;
using System.Data;

namespace SigmaBank.Infrastructure.Database.CardsPrivateData;

public class CardsPrivateDataRepository(IConnectionProvider connectionProvider) : ICardsPrivateDataRepository
{
    public async Task<CardPrivateData> GetCardPrivateDataByCardIdAsync(long cardId, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await connectionProvider.CreateConnectionAsync(cancellationToken);

        const string query = """
                             select
                                 cpd.card_private_data_id as CardPrivateDataId,
                                 cpd.card_id as CardId,
                                 cpd.expiration_date as ExpirationDate,
                                 cpd.code as Code
                             from cards_private_data cpd
                             where cpd.card_id = @CardId
                             """;

        var command = new CommandDefinition(
            query,
            parameters: new
            {
                CardId = cardId,
            },
            cancellationToken: cancellationToken);

        CardPrivateData result = await connection.QueryFirstAsync<CardPrivateData>(command);

        return result;
    }
}