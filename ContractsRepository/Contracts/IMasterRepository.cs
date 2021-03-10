using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IMasterRepository
    {
        void CreateMaster(Master master);

        Master GetMaster(int id);

        List<Master> GetMasters();

        Master GetMasterByUserId(int Userid);

        void UpdateMaster(Master master);

        void DeleteMaster(int id);
    }
}
