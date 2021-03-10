using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public SpecializationRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateSpecialization(Specialization specialization)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"Specializations\" (\"SpecializationName\", \"Icon\") VALUES(@SpecializationName, @Icon)";
            db.Execute(sqlQuery, specialization);
        }

        public void DeleteSpecialization(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"Specializations\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public Specialization GetSpecialization(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Specialization>("SELECT \"Id\", \"SpecializationName\", \"Icon\" FROM \"Specializations\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public List<Specialization> GetSpecializations()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Specialization>("SELECT \"Id\", \"SpecializationName\", \"Icon\" FROM \"Specializations\"").ToList();
        }

        public void UpdateSpecialization(Specialization specialization)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"Specializations\" SET \"SpecializationName\" = @SpecializationName, \"Icon\" = @Icon WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, specialization);
        }
    }
}
