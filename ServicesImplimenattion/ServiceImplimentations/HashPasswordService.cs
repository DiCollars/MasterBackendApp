using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System;
using System.Threading.Tasks;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class HashPasswordService : IHashPasswordService
    {
        public HashPasswordService()
        {
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 13);
        }

        public async Task<User> Logging(string userName, string userPassword)
        {
            User user = null; // await _context.Users.FirstOrDefaultAsync(u => u.Login == userName); TODO: Сделать метод ищущий в базе такого пользователя.

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
