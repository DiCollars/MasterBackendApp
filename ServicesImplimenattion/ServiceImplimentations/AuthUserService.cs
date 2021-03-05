﻿using Microsoft.AspNetCore.Http;
using RepositoryContractsDb.Contracts;
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
            UserShort logedUser = _userRepository.GetUserShort(userLogin); //_userRepository.GetShortUserByLoginAndRole(userLogin, userRole); //TODO: Тут использовать метод который достаёт из базы роли и юзера и мапит их в шортюзера

            if (logedUser == null)
            {
                throw new Exception("Login or password is invalid.");
            }

            return logedUser;
        }
    }
}
