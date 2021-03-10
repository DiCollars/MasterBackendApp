﻿using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface ILocationRepository
    {
        void CreateLocation(Location location);

        Location GetLocation(int id);

        List<Location> GetLocations();

        void UpdateLocation(Location location);

        void DeleteLocation(int id);
    }
}
