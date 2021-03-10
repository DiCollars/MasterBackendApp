using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpGet("get-masters-orders")]
        public List<OrderShort> GetOrders()
        {
            return _orderService.GetAllOrdersByMasterId(HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-all-orders")]
        public List<OrderShort> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-order")]
        public OrderShort GetOrder([FromQuery] int id)
        {
            return _orderService.GetByOrderId(id);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpPut("finished-order")]
        public void FinishedOrder([FromQuery] int orderId)
        {
            _orderService.FinishedOrderByMaster(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpPut("take-order")]
        public void TakeOrder([FromQuery] int orderId)
        {
            _orderService.TakeOrderByMaster(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpPut("not-agree-order")]
        public void NotAgreeOrder([FromQuery] int orderId)
        {
            _orderService.NotAgreeOrderByMaster(orderId, HttpContext);
        }
    }
}
