using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class MasterRepository : IMasterRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public MasterRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateMaster(Master master)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"Masters\" (\"UserId\", \"LocationId\", \"SpecializationId\") VALUES(@UserId, @LocationId, @SpecializationId)";
            db.Execute(sqlQuery, master);
        }

        public void DeleteMaster(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"Masters\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public Master GetMaster(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Master>("SELECT \"Id\", \"UserId\", \"LocationId\", \"SpecializationId\" FROM \"Masters\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public Master GetMasterByUserId(int Userid)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Master>("SELECT \"Id\", \"UserId\", \"LocationId\", \"SpecializationId\" FROM \"Masters\" WHERE \"UserId\" = @UserId", new { Userid }).FirstOrDefault();
        }

        public List<Master> GetMasters()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Master>("SELECT \"Id\", \"UserId\", \"LocationId\", \"SpecializationId\" FROM \"Masters\"").ToList();
        }

        public List<Master> GetMastersByServiceId(int specializationId)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Master>("SELECT \"Id\", \"UserId\", \"LocationId\", \"SpecializationId\" FROM \"Masters\" WHERE \"SpecializationId\" = @SpecializationId", new { specializationId }).ToList();
        }

        public List<Master> GetMastersByServiceIdAndLocationId(int specializationId, int locationId)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Master>("SELECT \"Id\", \"UserId\", \"LocationId\", \"SpecializationId\" FROM \"Masters\" WHERE \"SpecializationId\" = @SpecializationId AND \"LocationId\" = @locationId", new { specializationId, locationId }).ToList();
        }

        public void UpdateMaster(Master master)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"Masters\" SET \"UserId\" = @UserId, \"LocationId\" = @LocationId, \"SpecializationId\" = @SpecializationId WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, master);
        }
    }
}
