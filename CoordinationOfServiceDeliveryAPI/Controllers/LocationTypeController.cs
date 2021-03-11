using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationTypeController : Controller
    {
        private readonly ILocationTypeService _locationTypeService;

        public LocationTypeController(ILocationTypeService locationTypeService)
        {
            _locationTypeService = locationTypeService;
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpPost("add-locationType")]
        public void AddLocationType([FromBody] LocationType locationType)
        {
            _locationTypeService.CreateLocationType(locationType);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpGet("get-locationType")]
        public LocationType GetLocationType([FromQuery] int id)
        {
            return _locationTypeService.GetLocationType(id);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpGet("get-locationTypes")]
        public List<LocationType> GetLocationTypes()
        {
            return _locationTypeService.GetLocationTypes();
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpPut("edit-locationType")]
        public void EditLocationType([FromBody] LocationType locationType)
        {
            _locationTypeService.UpdateLocationType(locationType);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpDelete("delete-locationType")]
        public void DeleteLocationType([FromQuery] int id)
        {
            _locationTypeService.DeleteLocationType(id);
        }
    }
}
