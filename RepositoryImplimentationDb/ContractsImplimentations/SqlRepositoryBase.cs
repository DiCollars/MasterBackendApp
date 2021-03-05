using Npgsql;
using RepositoryContractsDb.Contracts;
using System.Data;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class SqlRepositoryBase : ISqlRepositoryBase
    {
        private string _connectionString = null;

        public SqlRepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
