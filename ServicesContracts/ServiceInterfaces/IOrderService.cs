using Microsoft.AspNetCore.Http;
using ServicesContracts.Models;
using System.Collections.Generic;

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
    }
}
