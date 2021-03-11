using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IAddressService
    {
        Address GetAddress(int id);

        List<Address> GetAddresss();

        void UpdateAddress(Address address);

        void DeleteAddress(int id);

        void CreateAddress(Address address);
    }
}
