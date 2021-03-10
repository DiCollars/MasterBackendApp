using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface ILocationTypeRepository
    {
        void CreateLocationType(LocationType locationType);

        LocationType GetLocationType(int id);

        List<LocationType> GetLocationTypes();

        void UpdateLocationType(LocationType locationType);

        void DeleteLocationType(int id);
    }
}
