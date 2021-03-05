using AutoMapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class UserService : IUserService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public UserShort GetUserShort(string login)
        {
            var currentUser = _userRepository.GetUserByLogin(login);
            var usersRole = _roleRepository.GetRole(currentUser.RoleId);

            if (currentUser != null && usersRole != null)
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserShort>()));
                var user = mapper.Map<UserShort>(currentUser);
                user.Role = usersRole.RoleName;

                return user;
            }

            return null;
        }


    }
}
