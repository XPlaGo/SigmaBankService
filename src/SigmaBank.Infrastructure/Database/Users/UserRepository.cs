using Dapper;
using SigmaBank.Core.Entities;
using SigmaBank.Core.Repositories.Users;
using System.Data;

namespace SigmaBank.Infrastructure.Database.Users;

public class UserRepository(IConnectionProvider connectionProvider) : IUsersRepository
{
    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await connectionProvider.CreateConnectionAsync(cancellationToken);

        const string query = """
                    insert into users (
                                       phone_number,
                                       first_name,
                                       last_name,
                                       age)
                    values (
                            @PhoneNumber,
                            @FirstName,
                            @LastName,
                            @Age)
                    returning
                        id as Id,
                        phone_number as PhoneNumber,
                        first_name as FirstName,
                        last_name as LastName,
                        age as Age
                    """;

        var command = new CommandDefinition(
            query,
            parameters: new
            {
                user.PhoneNumber,
                user.FirstName,
                user.LastName,
                user.Age,
            },
            cancellationToken: cancellationToken);

        User result = await connection.QueryFirstAsync<User>(command);

        return result;
    }

    public async Task<bool> UserExistsByIdAsync(long userId, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await connectionProvider.CreateConnectionAsync(cancellationToken);

        const string query = """
                select 1
                from users
                where id = @Id;
                """;

        var command = new CommandDefinition(
            query,
            parameters: new
            {
                Id = userId,
            },
            cancellationToken: cancellationToken);

        int? result = await connection.QueryFirstOrDefaultAsync<int?>(command);

        return result.HasValue;
    }

    public async Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await connectionProvider.CreateConnectionAsync(cancellationToken);

        const string query = """
                select 1
                from users
                where phone_number = @PhoneNumber;
                """;

        var command = new CommandDefinition(
            query,
            parameters: new
            {
                PhoneNumber = phoneNumber,
            },
            cancellationToken: cancellationToken);

        int? result = await connection.QueryFirstOrDefaultAsync<int?>(command);

        return result.HasValue;
    }

    public async Task<User> GetUserByIdAsync(long userId, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await connectionProvider.CreateConnectionAsync(cancellationToken);

        const string query = """
                    select
                        id as Id,
                        phone_number as PhoneNumber,
                        first_name as FirstName,
                        last_name as LastName,
                        age as Age
                    from users
                    where id = @Id;
                    """;

        var command = new CommandDefinition(
            query,
            parameters: new
            {
                Id = userId,
            },
            cancellationToken: cancellationToken);

        User result = await connection.QueryFirstAsync<User>(command);

        return result;
    }

    public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await connectionProvider.CreateConnectionAsync(cancellationToken);

        const string query = """
                    select
                        id as Id,
                        phone_number as PhoneNumber,
                        first_name as FirstName,
                        last_name as LastName,
                        age as Age
                    from users
                    where phone_number = @PhoneNumber;
                    """;

        var command = new CommandDefinition(
            query,
            parameters: new
            {
                PhoneNumber = phoneNumber,
            },
            cancellationToken: cancellationToken);

        User result = await connection.QueryFirstAsync<User>(command);

        return result;
    }
}