using SigmaBank.Core.Entities;
using SigmaBank.Core.Repositories.Accounts;
using SigmaBank.Core.Services.Accounts;
using SigmaBank.Core.Services.Users;

namespace SigmaBank.Services.Acounts;

public class AccountsService(
    IUsersService usersService,
    IAccountsRepository accountsRepository) : IAccountsService
{
    public async Task<IReadOnlyCollection<Account>> GetAccountsByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        if (!await usersService.UserExistsByIdAsync(userId, cancellationToken))
        {
            throw new InvalidOperationException("User does not exists");
        }

        return await accountsRepository.GetAccountsByUserIdAsync(userId, cancellationToken);
    }
}