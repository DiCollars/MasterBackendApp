using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    public class FileModel
    {
        public IFormFile FormFile { get; set; }

        public string Id { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPost("create-order")]
        public int CreateOrder([FromBody] Order order)
        {
            return _orderService.CreateOrderByClient(order, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPost("add-order-picture")]
        public async Task AddPictureOrder([FromForm] FileModel uploadedFile)
        {
            await _orderService.AddPictureToOrder(uploadedFile.FormFile, Convert.ToInt32(uploadedFile.Id), HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpGet("get-masters-orders")]
        public List<OrderShort> GetOrders()
        {
            return _orderService.GetAllOrdersByMasterId(HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpGet("get-masters-orders-by-date/{date}")]
        public List<OrderShortWithServLong> GetOrdersByDate(DateTime date)
        {
            return _orderService.GetAllOrdersByMasterIdAndDate(HttpContext, date);
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-picture-from-order/{orderId}")]
        public IActionResult GetPictureOrder(int orderId)
        {
            var data = _orderService.GetPictureFromOrder(orderId);
            return PhysicalFile(data.Item1, data.Item2, data.Item3);
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-picture-from-order2/{orderId}")]
        public string GetPictureOrder2(int orderId)
        {
            var data = _orderService.GetAndConvertPictureToBase64(orderId);
            return data;
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-all-orders")]
        public List<OrderShort> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-order/{id}")]
        public OrderShort GetOrder(int id)
        {
            return _orderService.GetByOrderId(id);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpPut("finished-order/{orderId}")]
        public void FinishedOrder(int orderId)
        {
            _orderService.FinishedOrderByMaster(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpPut("take-order/{orderId}")]
        public void TakeOrder(int orderId)
        {
            _orderService.TakeOrderByMaster(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpPut("not-agree-order/{orderId}")]
        public void NotAgreeOrder(int orderId)
        {
            _orderService.NotAgreeOrderByMaster(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("reject-order/{orderId}")]
        public void RejectOrder(int orderId, [FromBody] string comment)
        {
            _orderService.RejectOrderByClient(orderId, comment, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("not-done-order/{orderId}")]
        public void NotDoneOrder(int orderId)
        {
            _orderService.NotDoneOrderByClient(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("done-order-by-client/{orderId}")]
        public void DoneOrderByClient(int orderId)
        {
            _orderService.DoneOrderByClient(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, OPERATOR")]
        [HttpPut("done-order-by-operator/{orderId}")]
        public void DoneOrderByOperator(int orderId, [FromBody] string comment)
        {
            _orderService.DoneOrderByOperator(orderId, comment);
        }

        [Authorize(Roles = "ADMIN, OPERATOR")]
        [HttpPut("handle-order-for-client")]
        public void HandleOrderByOperatorForClient([FromBody] Order order)
        {
            _orderService.HandleOrderByOperatorForClient(order);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("accept-order/{orderId}")]
        public void AcceptOrder(int orderId)
        {
            _orderService.AcceptOrderByClient(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("not-accept-order/{orderId}")]
        public void NotAcceptOrder(int orderId, [FromBody] string comment)
        {
            _orderService.NotAcceptOrderByClient(orderId, comment, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpGet("get-all-orders-by-client-id")]
        public List<OrderShort> GetAllOrdersByClientId()
        {
            return _orderService.GetAllOrdersByClientId(HttpContext);
        }

        [Authorize(Roles = "ADMIN, OPERATOR")]
        [HttpPut("update-order")]
        public void UpdateOrder([FromBody] Order order)
        {
            _orderService.UpdateOrder(order);
        }

        [Authorize(Roles = "ADMIN, OPERATOR")]
        [HttpPut("send-order-to-client/{orderId}")]
        public void SendOrderToClientForAgreeing(int orderId)
        {
            _orderService.SendOrderToClientForAgreeingByOperator(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, OPERATOR")]
        [HttpPut("add-master-access/{orderId}")]
        public void AddMasterAccess(int orderId)
        {
            _orderService.AddMasterAccess(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, OPERATOR")]
        [HttpDelete("delete-order")]
        public void DeleteOrder([FromQuery] int id)
        {
            _orderService.DeleteOrder(id);
        }

        [Authorize(Roles = "ADMIN, OPERATOR")]
        [HttpGet("get-all-orders-for-operator")]
        public List<OrderShort> GetAllOrdersForOperator()
        {
            return _orderService.GetAllOrdersForOperator();
        }
    }
}
