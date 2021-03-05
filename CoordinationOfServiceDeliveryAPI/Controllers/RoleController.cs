using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    //TODO: Контроллер для теста - исправить, логика работы с базой должна быть в слое бизнес логики
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleService;

        public RoleController(IRoleRepository roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("add-role")]
        public void AddRole(Role role)
        {
            _roleService.CreateRole(role);
        }

        [HttpGet("get-role")]
        public Role GetRole(int id)
        {
            return _roleService.GetRole(id);
        }

        [HttpGet("get-roles")]
        public List<Role> GetRoles()
        {
            return _roleService.GetRoles();
        }

        [HttpPut("edit-role")]
        public void EditRole(Role role)
        {
            _roleService.UpdateRole(role);
        }

        [HttpDelete("delete-role")]
        public void DeleteRole(int id)
        {
            _roleService.DeleteRole(id);
        }
    }
}
