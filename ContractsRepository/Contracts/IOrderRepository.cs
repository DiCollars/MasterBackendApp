using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);

        Order GetOrder(int id);

        List<Order> GetOrders();

        List<Order> GetOrdersByMasterId(int masterId);

        void UpdateOrder(Order order);

        void DeleteOrder(int id);
    }
}
