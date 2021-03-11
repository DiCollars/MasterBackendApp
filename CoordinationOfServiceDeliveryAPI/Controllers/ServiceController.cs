using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost("add-service")]
        public void AddService([FromBody] Service service)
        {
            _serviceService.CreateService(service);
        }

        [HttpGet("get-service")]
        public Service GetService([FromQuery] int id)
        {
            return _serviceService.GetService(id);
        }

        [HttpGet("get-services")]
        public List<Service> GetServices()
        {
            return _serviceService.GetServices();
        }

        [HttpPut("edit-service")]
        public void EditService([FromBody] Service service)
        {
            _serviceService.UpdateService(service);
        }

        [HttpDelete("delete-service")]
        public void DeleteService([FromQuery] int id)
        {
            _serviceService.DeleteService(id);
        }
    }
}
