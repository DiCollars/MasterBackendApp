using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public ServiceRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateService(Service service)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"Services\" (\"Name\", \"Cost\", \"Long\", \"SpecializationId\") VALUES(@Name, @Cost, @Long, @SpecializationId)";
            db.Execute(sqlQuery, service);
        }

        public void DeleteService(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"Services\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public Service GetService(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Service>("SELECT \"Id\", \"Name\", \"Cost\", \"Long\", \"SpecializationId\" FROM \"Services\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public List<Service> GetServices()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Service>("SELECT \"Id\", \"Name\", \"Cost\", \"Long\", \"SpecializationId\" FROM \"Services\"").ToList();
        }

        public void UpdateService(Service service)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"Services\" SET \"Name\" = @Name, \"Cost\" = @Cost, \"Long\" = @Long, \"SpecializationId\" = @SpecializationId WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, service);
        }
    }
}
