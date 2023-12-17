using System.Data;
using Identity.Application.Abstractions.Data;
using Npgsql;

namespace Identity.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var con = new NpgsqlConnection(_connectionString);
        con.Open();

        return con;
    }
}