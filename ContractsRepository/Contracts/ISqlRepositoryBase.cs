using System.Data;

namespace RepositoryContractsDb.Contracts
{
    public interface ISqlRepositoryBase
    {
        IDbConnection Connection();
    }
}
