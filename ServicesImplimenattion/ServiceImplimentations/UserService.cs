using AutoMapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Role usersRole = null;

            if (currentUser != null)
            {
                usersRole = _roleRepository.GetRole(currentUser.RoleId);
            }

            if (currentUser != null && usersRole != null)
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserShort>()));
                var user = mapper.Map<UserShort>(currentUser);
                user.Role = usersRole.RoleName;

                return user;
            }

            return null;
        }

        public UserFull GetUserFull(string login)
        {
            var currentUser = _userRepository.GetUserByLoginStrict(login);
            Role usersRole = null;

            if (currentUser != null)
            {
                usersRole = _roleRepository.GetRole(currentUser.RoleId);
            }

            if (currentUser != null && usersRole != null)
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserFull>()));
                var user = mapper.Map<UserFull>(currentUser);
                user.RoleName = usersRole.RoleName;
                user.AccessRight = usersRole.AccessRight;

                return user;
            }

            return null;
        }

        public UserShort GetUserShortStrict(string login)
        {
            var currentUser = _userRepository.GetUserByLoginStrict(login);
            Role usersRole = null;

            if (currentUser != null)
            {
                usersRole = _roleRepository.GetRole(currentUser.RoleId);
            }

            if (currentUser != null && usersRole != null)
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserShort>()));
                var user = mapper.Map<UserShort>(currentUser);
                user.Role = usersRole.RoleName;

                return user;
            }

            return null;
        }

        public List<UserShort> GetAllUsersShort()
        {
            var users = _userRepository.GetUsers();
            var usersRoles = _roleRepository.GetRoles();

            if (users != null && usersRoles != null)
            {
                var shortUsers = users.Join(usersRoles,
                u => u.RoleId,
                r => r.Id,
                (u, r) => new UserShort() { Id = u.Id, Login = u.Login, Password = u.Password, Role = r.RoleName }).ToList();

                return shortUsers;
            }

            return null;
        }

        public void CreateUser(User user)
        {
            if (user.FirstName != "" && user.LastName != "" && user.MiddleName != "" && user.Password != "" && user.Password.Length >= 4 && user.RoleId != 0)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 13);
                _userRepository.CreateUser(user);
            }
            else
            {
                throw new Exception("Invalid user data.");
            }
        }

        public void UpdateUser(User user)
        {
            if (user.FirstName != "" && user.LastName != "" && user.MiddleName != "" && user.Password != "" && user.Password.Length >= 4 && user.RoleId != 0)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 13);
                _userRepository.UpdateUser(user);
            }
            else
            {
                throw new Exception("Invalid user data.");
            }
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

    }
}
