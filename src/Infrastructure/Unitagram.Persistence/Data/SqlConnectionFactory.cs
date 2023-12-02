using System.Data;
using Npgsql;

namespace Unitagram.Persistence.Data;

internal sealed class SqlConnectionFactory
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