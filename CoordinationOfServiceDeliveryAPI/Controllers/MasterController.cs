using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MasterController : Controller
    {
        //TODO: Исправить - вынести работу с БД в слой бизнес логики
        private readonly IMasterRepository _masterRepository;

        public MasterController(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        [HttpPost("add-master")]
        public void AddMaster([FromBody] Master master)
        {
            _masterRepository.CreateMaster(master);
        }

        [HttpGet("get-master")]
        public Master GetMaster([FromQuery] int id)
        {
            return _masterRepository.GetMaster(id);
        }

        [HttpGet("get-masters")]
        public List<Master> GetMasters()
        {
            return _masterRepository.GetMasters();
        }

        [HttpPut("edit-master")]
        public void EditMaster([FromBody] Master master)
        {
            _masterRepository.UpdateMaster(master);
        }

        [HttpDelete("delete-master")]
        public void DeleteMaster([FromQuery] int id)
        {
            _masterRepository.DeleteMaster(id);
        }
    }
}
