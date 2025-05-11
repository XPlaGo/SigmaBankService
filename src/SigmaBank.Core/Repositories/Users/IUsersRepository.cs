using SigmaBank.Core.Entities;

namespace SigmaBank.Core.Repositories.Users;

public interface IUsersRepository
{
    public Task<User> AddUserAsync(User user, CancellationToken cancellationToken);

    public Task<bool> UserExistsByIdAsync(long userId, CancellationToken cancellationToken);

    public Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);

    public Task<User> GetUserByIdAsync(long userId, CancellationToken cancellationToken);

    public Task<User> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
}