using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    //TODO: Контроллер для теста - исправить, логика работы с базой должна быть в слое бизнес логики
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost("add-user")]
        //public void AddUser(User user)
        //{
        //    _userService.CreateUser(user);
        //}

        [HttpGet("get-user")]
        public UserShort GetUser([FromHeader] string login)
        {
            return _userService.GetUserShort(login);
        }

        //[HttpGet("get-shortuser")]
        //public User GetShortUser(string login)
        //{
        //    return _userService.GetUserByLogin(login);
        //}

        //[HttpGet("get-users")]
        //public List<User> GetUsers()
        //{
        //    return _userService.GetUsers();
        //}

        //[HttpPut("edit-user")]
        //public void EditUser(User user)
        //{
        //    _userService.UpdateUser(user);
        //}

        //[HttpDelete("delete-user")]
        //public void DeleteUser(int id)
        //{
        //    _userService.DeleteUser(id);
        //}
    }
}
