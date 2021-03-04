using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using Dapper;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Npgsql;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class RoleRepository : IRoleRepository
    {
        string _connectionString = null;

        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateRole(Role role)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO \"Roles\" (RoleName, AccessRight) VALUES(@RoleName, @AccessRight)";
                db.Execute(sqlQuery, role);
            }
        }

        public void DeleteRole(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM \"Roles\" WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Role GetRole(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return db.Query<Role>("SELECT * FROM \"Roles\" WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<Role> GetRoles()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return db.Query<Role>("SELECT * FROM \"Roles\"").ToList();
            }
        }

        public void UpdateRole(Role role)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE \"Roles\" SET RoleName = @RoleName, AccessRight = @AccessRight WHERE Id = @Id";
                db.Execute(sqlQuery, role);
            }
        }
    }
}
