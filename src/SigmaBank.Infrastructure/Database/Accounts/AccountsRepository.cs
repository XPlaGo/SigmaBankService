using Dapper;
using SigmaBank.Core.Entities;
using SigmaBank.Core.Repositories.Accounts;
using System.Data;

namespace SigmaBank.Infrastructure.Database.Accounts;

public class AccountsRepository(IConnectionProvider connectionProvider) : IAccountsRepository
{
    public async Task<IReadOnlyCollection<Account>> GetAccountsByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await connectionProvider.CreateConnectionAsync(cancellationToken);

        const string query = """
                             select
                                 a.account_id as AccountId,
                                 a.user_id as UserId,
                                 a.amount as Amount,
                                 c.card_id as CardId,
                                 c.account_id as AccountId,
                                 c.number as Number,
                                 c.type as Type
                             from accounts a
                             inner join cards c on a.account_id = c.account_id
                             where a.user_id = @UserId;
                             """;

        var command = new CommandDefinition(
            query,
            parameters: new
            {
                UserId = userId,
            },
            cancellationToken: cancellationToken);

        var accountsMap = new Dictionary<long, Account>();

        _ = await connection.QueryAsync<Account, Card, Account>(
            command,
            (account, card) =>
            {
                if (!accountsMap.TryGetValue(account.AccountId, out Account? currentAccount))
                {
                    currentAccount = account;
                    currentAccount.Cards = [];
                    accountsMap.Add(account.AccountId, currentAccount);
                }

                currentAccount.Cards = [.. currentAccount.Cards, card];

                return currentAccount;
            },
            splitOn: "CardId");

        return [.. accountsMap.Values];
    }
}