using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MasterController : Controller
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("add-master")]
        public void AddMaster([FromBody] Master master)
        {
            _masterService.CreateMaster(master);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-master")]
        public Master GetMaster([FromQuery] int id)
        {
            return _masterService.GetMaster(id);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-masters")]
        public List<Master> GetMasters()
        {
            return _masterService.GetMasters();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("edit-master")]
        public void EditMaster([FromBody] Master master)
        {
            _masterService.UpdateMaster(master);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-master")]
        public void DeleteMaster([FromQuery] int id)
        {
            _masterService.DeleteMaster(id);
        }
    }
}
