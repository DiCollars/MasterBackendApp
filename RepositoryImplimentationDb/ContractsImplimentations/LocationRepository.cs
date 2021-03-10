﻿using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public LocationRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateLocation(Location location)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"Locations\" (\"LocationId\", \"LocationTypeId\", \"LocationName\", \"Coordinates\") VALUES(@LocationId, @LocationTypeId, @LocationName, @Coordinates)";
            db.Execute(sqlQuery, location);
        }

        public void DeleteLocation(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"Locations\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public Location GetLocation(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Location>("SELECT \"Id\", \"LocationId\", \"LocationTypeId\", \"LocationName\", \"Coordinates\" FROM \"Locations\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public List<Location> GetLocations()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Location>("SELECT \"Id\", \"LocationId\", \"LocationTypeId\", \"LocationName\", \"Coordinates\" FROM \"Locations\"").ToList();
        }

        public void UpdateLocation(Location location)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"Locations\" SET \"LocationId\" = @LocationId, \"LocationTypeId\" = @LocationTypeId, \"LocationName\" = @LocationName, \"Coordinates\" = @Coordinates WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, location);
        }
    }
}
