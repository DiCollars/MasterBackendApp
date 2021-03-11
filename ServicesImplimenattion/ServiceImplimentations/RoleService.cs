using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void CreateRole(Role role)
        {
            _roleRepository.CreateRole(role);
        }

        public void DeleteRole(int id)
        {
            _roleRepository.DeleteRole(id);
        }

        public Role GetRole(int id)
        {
            return _roleRepository.GetRole(id);
        }

        public List<Role> GetRoles()
        {
            return _roleRepository.GetRoles();
        }

        public void UpdateRole(Role role)
        {
            _roleRepository.UpdateRole(role);
        }
    }
}
