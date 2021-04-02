using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _masterRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;

        public MasterService(IMasterRepository masterRepository, ISpecializationRepository specializationRepository, ILocationRepository locationRepository, IUserRepository userRepository)
        {
            _masterRepository = masterRepository;
            _specializationRepository = specializationRepository;
            _locationRepository = locationRepository;
            _userRepository = userRepository;
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

        public List<MasterFull> GetMastersByServiceId(int specializationId)
        {
            var masters = _masterRepository.GetMastersByServiceId(specializationId);
            var users = _userRepository.GetUsers();
            var locations = _locationRepository.GetLocations();
            var specializations = _specializationRepository.GetSpecializations();

            if (users != null && masters != null  && specializations != null)
            {
                var result = (from master in masters
                              join user in users on master.UserId equals user.Id
                              join location in locations on master.LocationId equals location.Id
                              join specialization in specializations on master.SpecializationId equals specialization.Id
                              select new MasterFull
                              {
                                  Id = master.Id,
                                  MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}",
                                  Icon = specialization.Icon,
                                  SpecializationName = specialization.SpecializationName,
                                  SpecializationId = specialization.Id,
                                  MainLocationId = location.Id,
                                  Locations = _locationRepository.GetInnerLocations(location.Id)
                              }).ToList();

                return result;
            }

            return null;
        }

        public List<MasterFull> GetMastersByServiceIdAndLocationId(int specializationId, int locationId)
        {
            var masters = _masterRepository.GetMastersByServiceIdAndLocationId(specializationId, locationId);
            var users = _userRepository.GetUsers();
            var locations = _locationRepository.GetLocations();
            var specializations = _specializationRepository.GetSpecializations();

            if (users != null && masters != null && specializations != null)
            {
                var result = (from master in masters
                              join user in users on master.UserId equals user.Id
                              join location in locations on master.LocationId equals location.Id
                              join specialization in specializations on master.SpecializationId equals specialization.Id
                              select new MasterFull
                              {
                                  Id = master.Id,
                                  MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}",
                                  Icon = specialization.Icon,
                                  SpecializationName = specialization.SpecializationName,
                                  SpecializationId = specialization.Id,
                                  MainLocationId = location.Id,
                                  Locations = _locationRepository.GetInnerLocations(location.Id)
                              }).ToList();

                return result;
            }

            return null;
        }

        public void UpdateMaster(Master master)
        {
            _masterRepository.UpdateMaster(master);
        }
    }
}
