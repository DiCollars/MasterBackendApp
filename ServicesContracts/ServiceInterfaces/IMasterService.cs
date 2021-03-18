using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IMasterService
    {
        Master GetMaster(int id);

        List<Master> GetMasters();

        List<MasterFull> GetMastersByServiceId(int SpecializationId);

        void UpdateMaster(Master master);

        void DeleteMaster(int id);

        void CreateMaster(Master master);
    }
}
