using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public OrderRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateOrder(Order order)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"Orders\" (\"MasterId\", \"UserId\", \"ServiceId\", \"AddressId\", \"Decription\", \"StartDate\", \"EndDate\", \"Status\", \"StatusColor\", \"Comment\", \"Picture\") VALUES(@MasterId, @UserId, @ServiceId, @AddressId, @Decription, @StartDate, @EndDate, @Status, @StatusColor, @Comment, @Picture)";
            db.Execute(sqlQuery, order);
        }

        public void DeleteOrder(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"Orders\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public Order GetOrder(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Order>("SELECT \"Id\", \"MasterId\", \"UserId\", \"ServiceId\", \"AddressId\", \"Decription\", \"StartDate\", \"EndDate\", \"Status\", \"StatusColor\", \"Comment\", \"Picture\" FROM \"Orders\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public List<Order> GetOrders()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Order>("SELECT \"Id\", \"MasterId\", \"UserId\", \"ServiceId\", \"AddressId\", \"Decription\", \"StartDate\", \"EndDate\", \"Status\", \"StatusColor\", \"Comment\", \"Picture\" FROM \"Orders\"").ToList();
        }

        public List<Order> GetOrdersByMasterId(int masterId)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Order>("SELECT \"Id\", \"MasterId\", \"UserId\", \"ServiceId\", \"AddressId\", \"Decription\", \"StartDate\", \"EndDate\", \"Status\", \"StatusColor\", \"Comment\", \"Picture\" FROM \"Orders\" WHERE \"MasterId\" = @masterId AND \"Status\" != 'WAIT_OPERATOR'", new { masterId }).ToList();
        }

        public List<Order> GetOrdersForOperator()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Order>("SELECT \"Id\", \"MasterId\", \"UserId\", \"ServiceId\", \"AddressId\", \"Decription\", \"StartDate\", \"EndDate\", \"Status\", \"StatusColor\", \"Comment\", \"Picture\" FROM \"Orders\" WHERE \"Status\" != 'WAIT_OPERATOR'").ToList();
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Order>("SELECT \"Id\", \"MasterId\", \"UserId\", \"ServiceId\", \"AddressId\", \"Decription\", \"StartDate\", \"EndDate\", \"Status\", \"StatusColor\", \"Comment\", \"Picture\" FROM \"Orders\" WHERE \"UserId\" = @userId", new { userId }).ToList();
        }

        public void UpdateOrder(Order order)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"Orders\" SET \"MasterId\" = @MasterId, \"UserId\" = @UserId, \"ServiceId\" = @ServiceId, \"AddressId\" = @AddressId, \"Decription\" = @Decription, \"StartDate\" = @StartDate, \"EndDate\" = @EndDate, \"Status\" = @Status, \"StatusColor\" = @StatusColor , \"Comment\" = @Comment, \"Picture\" = @Picture WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, order);
        }
    }
}
