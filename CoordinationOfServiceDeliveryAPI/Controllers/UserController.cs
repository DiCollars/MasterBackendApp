using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic; 

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add-user")]
        public void AddUser([FromBody] User user)
        {
            _userService.CreateUser(user);
        }

        [HttpGet("get-user")]
        public UserShort GetUser([FromHeader] string login)
        {
            return _userService.GetUserShort(login);
        }

        [HttpGet("get-users")]
        public List<UserShort> GetUsers()
        {
            return _userService.GetAllUsersShort();
        }

        [HttpPut("edit-user")]
        public void EditUser([FromBody] User user)
        {
            _userService.UpdateUser(user);
        }

        [HttpDelete("delete-user")]
        public void DeleteUser([FromQuery] int id)
        {
            _userService.DeleteUser(id);
        }
    }
}
