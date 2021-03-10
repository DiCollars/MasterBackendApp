using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        //TODO: Исправить - вынести работу с БД в слой бизнес логики
        private readonly IRoleRepository _roleService;

        public RoleController(IRoleRepository roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("add-role")]
        public void AddRole([FromBody] Role role)
        {
            _roleService.CreateRole(role);
        }

        [HttpGet("get-role")]
        public Role GetRole([FromQuery] int id)
        {
            return _roleService.GetRole(id);
        }

        [HttpGet("get-roles")]
        public List<Role> GetRoles()
        {
            return _roleService.GetRoles();
        }

        [HttpPut("edit-role")]
        public void EditRole([FromBody] Role role)
        {
            _roleService.UpdateRole(role);
        }

        [HttpDelete("delete-role")]
        public void DeleteRole([FromQuery] int id)
        {
            _roleService.DeleteRole(id);
        }
    }
}
