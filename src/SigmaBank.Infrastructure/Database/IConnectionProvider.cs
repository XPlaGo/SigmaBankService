using System.Data;

namespace SigmaBank.Infrastructure.Database;

public interface IConnectionProvider
{
    public Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken);
}