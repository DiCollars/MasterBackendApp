using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IMasterRepository
    {
        void CreateMaster(Master master);

        Master GetMaster(int id);

        List<Master> GetMasters();

        List<Master> GetMastersByServiceId(int SpecializationId);

        List<Master> GetMastersByServiceIdAndLocationId(int specializationId, int locationId);

        Master GetMasterByUserId(int Userid);

        void UpdateMaster(Master master);

        void DeleteMaster(int id);
    }
}
