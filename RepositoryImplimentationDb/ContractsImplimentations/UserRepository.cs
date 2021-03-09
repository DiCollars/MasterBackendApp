using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public UserRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateUser(User user)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"Users\" (\"RoleId\", \"Login\", \"Password\", \"LastName\", \"FirstName\", \"MiddleName\") VALUES(@RoleId, @Login, @Password, @LastName, @FirstName, @MiddleName)";
            db.Execute(sqlQuery, user);
        }

        public void DeleteUser(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"Users\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public User GetUserByLogin(string login)
        {
            login = login.ToLower();

            using var db = _sqlRepositoryBase.Connection();
            return db.Query<User>("SELECT \"Id\", \"RoleId\", \"Login\", \"LastName\", \"FirstName\", \"MiddleName\", \"Password\" FROM \"Users\" WHERE LOWER(\"Login\") = @login", new { login }).FirstOrDefault();
        }

        public User GetUserByLoginStrict(string login)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<User>("SELECT \"Id\", \"RoleId\", \"Login\", \"LastName\", \"FirstName\", \"MiddleName\", \"Password\" FROM \"Users\" WHERE \"Login\" = @login", new { login }).FirstOrDefault();
        }

        public User GetUser(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<User>("SELECT \"Id\", \"RoleId\", \"Login\", \"LastName\", \"FirstName\", \"MiddleName\", \"Password\" FROM \"Users\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<User>("SELECT \"Id\", \"RoleId\", \"Login\", \"LastName\", \"FirstName\", \"MiddleName\", \"Password\" FROM \"Users\"").ToList();
        }

        public void UpdateUser(User user)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"Users\" SET \"RoleId\" = @RoleId, \"Login\" = @Login, \"Password\" = @Password, \"LastName\" = @LastName, \"FirstName\" = @FirstName, \"MiddleName\" = @MiddleName WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, user);
        }
    }
}
