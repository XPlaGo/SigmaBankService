using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Transactions;

namespace SigmaBank.Infrastructure.Database;

public class ConnectionProvider(IConfiguration configuration) : IConnectionProvider
{
    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
    {
        if (Transaction.Current is not null &&
            Transaction.Current.TransactionInformation.Status is TransactionStatus.Aborted)
        {
            throw new TransactionAbortedException("Transaction was aborted (probably by user cancellation request)");
        }

        var connection = new NpgsqlConnection(GetConfigurationString());
        await connection.OpenAsync(cancellationToken);

        await connection.ReloadTypesAsync(cancellationToken);

        return connection;
    }

    private string GetConfigurationString()
    {
        string? connectionString = configuration.GetConnectionString("PostgresConnection");
        return connectionString ?? throw new InvalidOperationException("Connection string not found");
    }
}