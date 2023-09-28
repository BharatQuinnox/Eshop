using EShop.Core.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace EShop.Infrastructure.ConnectionFactory
{
    public class SqlDBConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection(string ConnectionString)
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
