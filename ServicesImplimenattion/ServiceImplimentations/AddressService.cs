using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public void CreateAddress(Address address)
        {
            _addressRepository.CreateAddress(address);
        }

        public void DeleteAddress(int id)
        {
            _addressRepository.DeleteAddress(id);
        }

        public Address GetAddress(int id)
        {
            return _addressRepository.GetAddress(id);
        }

        public List<Address> GetAddresss()
        {
            return _addressRepository.GetAddresss();
        }

        public void UpdateAddress(Address address)
        {
            _addressRepository.UpdateAddress(address);
        }
    }
}
