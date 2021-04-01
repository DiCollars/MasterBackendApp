using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILocationTypeRepository _locationTypeRepository;

        public LocationService(ILocationRepository locationRepository, ILocationTypeRepository locationTypeRepository)
        {
            _locationRepository = locationRepository;
            _locationTypeRepository = locationTypeRepository;
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

        public List<FullLocation> GetLocations()
        {
            var allLocations = _locationRepository.GetLocations();
            var allLocationTypes = _locationTypeRepository.GetLocationTypes();

            var locationsWithTypes = (from location in allLocations
                                      join locationType in allLocationTypes on location.LocationTypeId equals locationType.Id
                                      select new FullLocation()
                                      {
                                          Id = location.Id,
                                          Coordinates = location.Coordinates,
                                          LocationName = location.LocationName,
                                          Type = locationType.LocationName,
                                          ParentId = location.LocationId,
                                          ChildLocation = new List<FullLocation>()
                                      }).ToList();

            foreach (var locationWithTypes in locationsWithTypes)
            {
                if (locationWithTypes.ParentId != null)
                {
                    AddToParent((int)locationWithTypes.ParentId, locationWithTypes, ref locationsWithTypes);
                }
            }

            var onlyParentLocations = locationsWithTypes.Where(l => l.ParentId == null).ToList();

            return onlyParentLocations;
        }

        private void AddToParent(int parentId, FullLocation currentChild, ref List<FullLocation> allLocations)
        {
            foreach (var location in allLocations)
            {
                if (location.Id == parentId)
                {
                    location.ChildLocation.Add(currentChild);
                }
            }
        }

        public void UpdateLocation(Location location)
        {
            _locationRepository.UpdateLocation(location);
        }
    }
}
