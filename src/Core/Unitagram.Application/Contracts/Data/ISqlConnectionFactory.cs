using System.Data;

namespace Unitagram.Application.Contracts.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}