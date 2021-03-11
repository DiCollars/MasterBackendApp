using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("add-role")]
        public void AddRole([FromBody] Role role)
        {
            _roleService.CreateRole(role);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-role")]
        public Role GetRole([FromQuery] int id)
        {
            return _roleService.GetRole(id);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-roles")]
        public List<Role> GetRoles()
        {
            return _roleService.GetRoles();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("edit-role")]
        public void EditRole([FromBody] Role role)
        {
            _roleService.UpdateRole(role);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-role")]
        public void DeleteRole([FromQuery] int id)
        {
            _roleService.DeleteRole(id);
        }
    }
}
