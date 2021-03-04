using Microsoft.AspNetCore.Http;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class AuthUserService : IAuthUserService
    {
        public async Task<User> GetLoggedUser(HttpContext httpContext)
        {
            var userLogin = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var userRole = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            User logedUser = null; // await _context.Users.FirstOrDefaultAsync(U => U.Login == userLogin && U.Role == userRole); TODO: Сделать метод для проверки наличия пользователя в базе.

            if (logedUser == null)
            {
                throw new Exception("Login or password is invalid.");
            }

            return logedUser;
        }
    }
}
