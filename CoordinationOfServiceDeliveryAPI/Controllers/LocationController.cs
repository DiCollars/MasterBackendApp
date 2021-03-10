using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        //TODO: Исправить - вынести работу с БД в слой бизнес логики
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpPost("add-location")]
        public void AddLocation([FromBody] Location location)
        {
            _locationRepository.CreateLocation(location);
        }

        [HttpGet("get-location")]
        public Location GetLocation([FromQuery] int id)
        {
            return _locationRepository.GetLocation(id);
        }

        [HttpGet("get-locations")]
        public List<Location> GetLocations()
        {
            return _locationRepository.GetLocations();
        }

        [HttpPut("edit-location")]
        public void EditLocation([FromBody] Location location)
        {
            _locationRepository.UpdateLocation(location);
        }

        [HttpDelete("delete-location")]
        public void DeleteLocation([FromQuery] int id)
        {
            _locationRepository.DeleteLocation(id);
        }
    }
}
