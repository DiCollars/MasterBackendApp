using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : Controller
    {
        //TODO: Исправить - вынести работу с БД в слой бизнес логики
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpPost("add-service")]
        public void AddService([FromBody] Service service)
        {
            _serviceRepository.CreateService(service);
        }

        [HttpGet("get-service")]
        public Service GetService([FromQuery] int id)
        {
            return _serviceRepository.GetService(id);
        }

        [HttpGet("get-services")]
        public List<Service> GetServices()
        {
            return _serviceRepository.GetServices();
        }

        [HttpPut("edit-service")]
        public void EditService([FromBody] Service service)
        {
            _serviceRepository.UpdateService(service);
        }

        [HttpDelete("delete-service")]
        public void DeleteService([FromQuery] int id)
        {
            _serviceRepository.DeleteService(id);
        }
    }
}
