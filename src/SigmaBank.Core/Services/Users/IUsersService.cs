using SigmaBank.Core.Entities;
using SigmaBank.Core.Services.Users.Models;

namespace SigmaBank.Core.Services.Users;

public interface IUsersService
{
    public Task<User> CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken);

    public Task<bool> UserExistsByIdAsync(long userId, CancellationToken cancellationToken);

    public Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);

    public Task<User> GetUserByIdAsync(long userId, CancellationToken cancellationToken);

    public Task<User> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
}