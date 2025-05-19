using SigmaBank.Core.Entities;

namespace SigmaBank.Core.Services.Accounts;

public interface IAccountsService
{
    public Task<IReadOnlyCollection<Account>> GetAccountsByUserIdAsync(long userId, CancellationToken cancellationToken);
}