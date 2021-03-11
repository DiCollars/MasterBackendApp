using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IOrderService
    {
        void FinishedOrderByMaster(int orderId, HttpContext httpContext);

        void TakeOrderByMaster(int orderId, HttpContext httpContext);

        void NotAgreeOrderByMaster(int orderId, HttpContext httpContext);

        List<OrderShort> GetAllOrdersByMasterId(HttpContext httpContext);

        OrderShort GetByOrderId(int id);

        List<OrderShort> GetAllOrders();

        List<OrderShort> GetAllOrdersByClientId(HttpContext httpContext);

        void RejectOrderByClient(int orderId, HttpContext httpContext);

        void NotDoneOrderByClient(int orderId, HttpContext httpContext);

        void AcceptOrderByClient(int orderId, HttpContext httpContext);

        void NotAcceptOrderByClient(int orderId, HttpContext httpContext);

        void UpdateOrder(Order order);

        void DeleteOrder(int orderId);

        List<OrderShort> GetAllOrdersForOperator();

        Task CreateOrderByClient(Order order, HttpContext httpContext);

        Task AddPictureToOrder(IFormFile uploadedFile, int orderId, HttpContext httpContext);

        (string, string, string) GetPictureFromOrder(int orderId, HttpContext httpContext);
    }
}
