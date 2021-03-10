using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IAddressRepository
    {
        void CreateAddress(Address adress);

        Address GetAddress(int id);

        List<Address> GetAddresss();

        void UpdateAddress(Address adress);

        void DeleteAddress(int id);
    }
}
