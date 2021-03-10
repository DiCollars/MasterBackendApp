using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IUserService
    {
        UserShort GetUserShort(string login);

        UserFull GetUserFull(string login);

        UserShort GetUserShortStrict(string login);

        List<UserShort> GetAllUsersShort();

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int id);
    }
}
