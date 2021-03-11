using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class LocationTypeService : ILocationTypeService
    {
        private readonly ILocationTypeRepository _locationTypeRepository;

        public LocationTypeService(ILocationTypeRepository locationTypeRepository)
        {
            _locationTypeRepository = locationTypeRepository;
        }

        public void CreateLocationType(LocationType locationType)
        {
            _locationTypeRepository.CreateLocationType(locationType);
        }

        public void DeleteLocationType(int id)
        {
            _locationTypeRepository.DeleteLocationType(id);
        }

        public LocationType GetLocationType(int id)
        {
            return _locationTypeRepository.GetLocationType(id);
        }

        public List<LocationType> GetLocationTypes()
        {
            return _locationTypeRepository.GetLocationTypes();
        }

        public void UpdateLocationType(LocationType locationType)
        {
            _locationTypeRepository.UpdateLocationType(locationType);
        }
    }
}
