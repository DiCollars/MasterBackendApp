using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _masterRepository;

        public MasterService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public void CreateMaster(Master master)
        {
            _masterRepository.CreateMaster(master);
        }

        public void DeleteMaster(int id)
        {
            _masterRepository.DeleteMaster(id);
        }

        public Master GetMaster(int id)
        {
            return _masterRepository.GetMaster(id);
        }

        public List<Master> GetMasters()
        {
            return _masterRepository.GetMasters();
        }

        public void UpdateMaster(Master master)
        {
            _masterRepository.UpdateMaster(master);
        }
    }
}
