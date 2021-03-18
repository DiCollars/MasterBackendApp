using Microsoft.AspNetCore.Http;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IUserService _userRepository;

        public AuthUserService(IUserService userRepository)
        {
            _userRepository = userRepository;
        }

        public UserShort GetLoggedUser(HttpContext httpContext)
        {
            var userLogin = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var userRole = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            UserShort logedUser = _userRepository.GetUserShortStrict(userLogin);

            if (logedUser == null)
            {
                throw new Exception("Login or password is invalid.");
            }

            return logedUser;
        }

        public UserFull GetLoggedUserFull(HttpContext httpContext)
        {
            if (httpContext.User.Claims.ToList().Count() == 0)
            {
                throw new Exception("Login or password is invalid.");
            }

            var userLogin = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var userRole = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            UserFull logedUser = _userRepository.GetUserFull(userLogin);

            return logedUser;
        }
    }
}
