using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface ILocationTypeService
    {
        LocationType GetLocationType(int id);

        List<LocationType> GetLocationTypes();

        void UpdateLocationType(LocationType locationType);

        void DeleteLocationType(int id);

        void CreateLocationType(LocationType locationType);
    }
}
