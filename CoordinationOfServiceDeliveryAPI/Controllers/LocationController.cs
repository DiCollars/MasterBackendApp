using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpPost("add-location")]
        public void AddLocation([FromBody] Location location)
        {
            _locationService.CreateLocation(location);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpGet("get-location")]
        public Location GetLocation([FromQuery] int id)
        {
            return _locationService.GetLocation(id);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY, OPERATOR")]
        [HttpGet("get-locations")]
        public List<FullLocation> GetLocations()
        {
            return _locationService.GetLocations();
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpPut("edit-location")]
        public void EditLocation([FromBody] Location location)
        {
            _locationService.UpdateLocation(location);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpDelete("delete-location")]
        public void DeleteLocation([FromQuery] int id)
        {
            _locationService.DeleteLocation(id);
        }
    }
}
