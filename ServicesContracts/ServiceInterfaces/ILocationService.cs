using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface ILocationService
    {
        Location GetLocation(int id);

        List<Location> GetInnerLocations(int id);

        List<FullLocation> GetLocations();

        void UpdateLocation(Location location);

        void DeleteLocation(int id);

        void CreateLocation(Location location);
    }
}
