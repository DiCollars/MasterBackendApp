using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IRoleService
    {
        Role GetRole(int id);

        List<Role> GetRoles();

        void UpdateRole(Role role);

        void DeleteRole(int id);

        void CreateRole(Role role);
    }
}
