using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        //TODO: Исправить - вынести работу с БД в слой бизнес логики
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpPost("add-address")]
        public void AddLocation([FromBody] Address address)
        {
            _addressRepository.CreateAddress(address);
        }

        [HttpGet("get-address")]
        public Address GetLocation([FromQuery] int id)
        {
            return _addressRepository.GetAddress(id);
        }

        [HttpGet("get-addresses")]
        public List<Address> GetLocations()
        {
            return _addressRepository.GetAddresss();
        }

        [HttpPut("edit-address")]
        public void EditLocation([FromBody] Address address)
        {
            _addressRepository.UpdateAddress(address);
        }

        [HttpDelete("delete-address")]
        public void DeleteLocation([FromQuery] int id)
        {
            _addressRepository.DeleteAddress(id);
        }
    }
}
