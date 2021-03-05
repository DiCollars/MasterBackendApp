using RepositoryContractsDb.Contracts;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class HashPasswordService : IHashPasswordService
    {
        private readonly IUserService _userRepository;

        public HashPasswordService(IUserService userRepository)
        {
            _userRepository = userRepository;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 13);
        }

        public UserShort Logging(string userName, string userPassword)
        {
            UserShort user = _userRepository.GetUserShort(userName);

            if (user == null)
            {
                throw new Exception("User isnt exist.");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(userPassword, user.Password);

            if (isValidPassword && user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Password is invalid.");
            }
        }
    }
}
