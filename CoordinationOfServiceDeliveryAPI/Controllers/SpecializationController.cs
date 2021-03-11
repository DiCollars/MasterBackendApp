using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpPost("add-specialization")]
        public void AddSpecialization([FromBody] Specialization specialization)
        {
            _specializationService.CreateSpecialization(specialization);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpGet("get-specialization")]
        public Specialization GetSpecialization([FromQuery] int id)
        {
            return _specializationService.GetSpecialization(id);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpGet("get-specializations")]
        public List<Specialization> GetSpecializations()
        {
            return _specializationService.GetSpecializations();
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpPut("edit-specialization")]
        public void EditSpecialization([FromBody] Specialization specialization)
        {
            _specializationService.UpdateSpecialization(specialization);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpDelete("delete-specialization")]
        public void DeleteSpecialization([FromQuery] int id)
        {
            _specializationService.DeleteSpecialization(id);
        }
    }
}
