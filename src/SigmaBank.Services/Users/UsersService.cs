using SigmaBank.Core.Entities;
using SigmaBank.Core.Repositories.Users;
using SigmaBank.Core.Services.Users;
using SigmaBank.Core.Services.Users.Models;

namespace SigmaBank.Services.Users;

public class UsersService(IUsersRepository usersRepository) : IUsersService
{
    public async Task<User> CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = 0,
            PhoneNumber = model.PhoneNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Age = model.Age,
        };

        return await usersRepository.AddUserAsync(user, cancellationToken);
    }

    public async Task<bool> UserExistsByIdAsync(long userId, CancellationToken cancellationToken)
    {
        return await usersRepository.UserExistsByIdAsync(userId, cancellationToken);
    }

    public async Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        return await usersRepository.UserExistsByPhoneNumberAsync(phoneNumber, cancellationToken);
    }

    public async Task<User> GetUserByIdAsync(long userId, CancellationToken cancellationToken)
    {
        return await usersRepository.GetUserByIdAsync(userId, cancellationToken);
    }

    public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        return await usersRepository.GetUserByPhoneNumberAsync(phoneNumber, cancellationToken);
    }
}