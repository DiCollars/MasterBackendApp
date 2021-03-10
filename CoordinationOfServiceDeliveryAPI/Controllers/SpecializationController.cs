using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecializationController : Controller
    {
        //TODO: Исправить - вынести работу с БД в слой бизнес логики
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationController(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        [HttpPost("add-specialization")]
        public void AddSpecialization([FromBody] Specialization specialization)
        {
            _specializationRepository.CreateSpecialization(specialization);
        }

        [HttpGet("get-specialization")]
        public Specialization GetSpecialization([FromQuery] int id)
        {
            return _specializationRepository.GetSpecialization(id);
        }

        [HttpGet("get-specializations")]
        public List<Specialization> GetSpecializations()
        {
            return _specializationRepository.GetSpecializations();
        }

        [HttpPut("edit-specialization")]
        public void EditSpecialization([FromBody] Specialization specialization)
        {
            _specializationRepository.UpdateSpecialization(specialization);
        }

        [HttpDelete("delete-specialization")]
        public void DeleteSpecialization([FromQuery] int id)
        {
            _specializationRepository.DeleteSpecialization(id);
        }
    }
}
