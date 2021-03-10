using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationTypeController : Controller
    {
        //TODO: Исправить - вынести работу с БД в слой бизнес логики
        private readonly ILocationTypeRepository _locationTypeRepository;

        public LocationTypeController(ILocationTypeRepository locationTypeRepository)
        {
            _locationTypeRepository = locationTypeRepository;
        }

        [HttpPost("add-locationType")]
        public void AddLocationType([FromBody] LocationType locationType)
        {
            _locationTypeRepository.CreateLocationType(locationType);
        }

        [HttpGet("get-locationType")]
        public LocationType GetLocationType([FromQuery] int id)
        {
            return _locationTypeRepository.GetLocationType(id);
        }

        [HttpGet("get-locationTypes")]
        public List<LocationType> GetLocationTypes()
        {
            return _locationTypeRepository.GetLocationTypes();
        }

        [HttpPut("edit-locationType")]
        public void EditLocationType([FromBody] LocationType locationType)
        {
            _locationTypeRepository.UpdateLocationType(locationType);
        }

        [HttpDelete("delete-locationType")]
        public void DeleteLocationType([FromQuery] int id)
        {
            _locationTypeRepository.DeleteLocationType(id);
        }
    }
}
