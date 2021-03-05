using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IUserRepository
    {
        void CreateUser(User user);

        User GetUser(int id);

        List<User> GetUsers();

        User GetUserByLogin(string login);

        void UpdateUser(User user);

        void DeleteUser(int id);
    }
}
