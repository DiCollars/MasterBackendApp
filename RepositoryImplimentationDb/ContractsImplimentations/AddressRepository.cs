using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public AddressRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateAddress(Address adress)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"Addresses\" (\"LocationId\", \"City\", \"Street\", \"House\", \"Flat\") VALUES(@LocationId, @City, @Street, @House, @Flat)";
            db.Execute(sqlQuery, adress);
        }

        public void DeleteAddress(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"Addresses\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public Address GetAddress(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Address>("SELECT \"Id\", \"LocationId\", \"City\", \"Street\", \"House\", \"Flat\" FROM \"Addresses\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public List<Address> GetAddresss()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Address>("SELECT \"Id\", \"LocationId\", \"City\", \"Street\", \"House\", \"Flat\" FROM \"Addresses\"").ToList();
        }

        public void UpdateAddress(Address adress)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"Addresses\" SET \"LocationId\" = @LocationId, \"City\" = @City, \"Street\" = @Street, \"House\" = @House, \"Flat\" = @Flat WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, adress);
        }
    }
}
