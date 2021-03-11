using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);

        Order GetOrder(int id);

        List<Order> GetOrdersForOperator();

        List<Order> GetOrders();

        List<Order> GetOrdersByMasterId(int masterId);

        List<Order> GetOrdersByUserId(int userId);

        void UpdateOrder(Order order);

        void DeleteOrder(int id);
    }
}
