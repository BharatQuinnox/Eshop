using System.Data;

namespace EShop.Core.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection(string ConnectionString);
    }
}
