using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class LocationTypeRepository : ILocationTypeRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public LocationTypeRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateLocationType(LocationType locationType)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"LocationTypes\" (\"LocationName\") VALUES(@LocationName)";
            db.Execute(sqlQuery, locationType);
        }

        public void DeleteLocationType(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"LocationTypes\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public LocationType GetLocationType(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<LocationType>("SELECT \"Id\", \"LocationName\" FROM \"LocationTypes\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public List<LocationType> GetLocationTypes()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<LocationType>("SELECT \"Id\", \"LocationName\" FROM \"LocationTypes\"").ToList();
        }

        public void UpdateLocationType(LocationType locationType)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"LocationTypes\" SET \"LocationName\" = @LocationName WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, locationType);
        }
    }
}
