using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IMasterService
    {
        Master GetMaster(int id);

        List<Master> GetMasters();

        void UpdateMaster(Master master);

        void DeleteMaster(int id);

        void CreateMaster(Master master);
    }
}
