using SigmaBank.Core.Entities;

namespace SigmaBank.Core.Repositories.Accounts;

public interface IAccountsRepository
{
    public Task<IReadOnlyCollection<Account>> GetAccountsByUserIdAsync(long userId, CancellationToken cancellationToken);
}