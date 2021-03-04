using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IRoleRepository
    {
        void CreateRole(Role role);

        Role GetRole(int id);

        List<Role> GetRoles();

        void UpdateRole(Role role);

        void DeleteRole(int id);
    }
}
