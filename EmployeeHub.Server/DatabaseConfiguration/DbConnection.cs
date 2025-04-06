using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeHub.Server.DatabaseConfiguration;

public class SqlServerConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    
    public SqlServerConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken ct)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(ct);
        return connection;
    }
}

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken ct);
}
