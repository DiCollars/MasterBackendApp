using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public void CreateLocation(Location location)
        {
            _locationRepository.CreateLocation(location);
        }

        public void DeleteLocation(int id)
        {
            _locationRepository.DeleteLocation(id);
        }

        public Location GetLocation(int id)
        {
            return _locationRepository.GetLocation(id);
        }

        public List<Location> GetInnerLocations(int id)
        {
            return _locationRepository.GetInnerLocations(id);
        }

        public List<Location> GetLocations()
        {
            return _locationRepository.GetLocations();
        }

        public void UpdateLocation(Location location)
        {
            _locationRepository.UpdateLocation(location);
        }
    }
}
