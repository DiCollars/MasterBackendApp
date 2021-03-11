using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpPost("add-address")]
        public void AddLocation([FromBody] Address address)
        {
            _addressService.CreateAddress(address);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpGet("get-address")]
        public Address GetLocation([FromQuery] int id)
        {
            return _addressService.GetAddress(id);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpGet("get-addresses")]
        public List<Address> GetLocations()
        {
            return _addressService.GetAddresss();
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpPut("edit-address")]
        public void EditLocation([FromBody] Address address)
        {
            _addressService.UpdateAddress(address);
        }

        [Authorize(Roles = "ADMIN, MASTER_DADDY")]
        [HttpDelete("delete-address")]
        public void DeleteLocation([FromQuery] int id)
        {
            _addressService.DeleteAddress(id);
        }
    }
}
