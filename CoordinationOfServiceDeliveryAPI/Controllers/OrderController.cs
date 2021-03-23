﻿using Microsoft.AspNetCore.Authorization;
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


        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-picture-from-order")]
        public IActionResult GetPictureOrder(int orderId)
        {
            var data = _orderService.GetPictureFromOrder(orderId, HttpContext);

            return PhysicalFile(data.Item1, data.Item2, data.Item3);
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

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("reject-order")]
        public void RejectOrder([FromQuery] int orderId)
        {
            _orderService.RejectOrderByClient(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("not-done-order")]
        public void NotDoneOrder([FromQuery] int orderId)
        {
            _orderService.NotDoneOrderByClient(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("accept-order")]
        public void AcceptOrder([FromQuery] int orderId)
        {
            _orderService.AcceptOrderByClient(orderId, HttpContext);
        }

        [Authorize(Roles = "ADMIN, CLIENT")]
        [HttpPut("not-accept-order")]
        public void NotAcceptOrder([FromQuery] int orderId)
        {
            _orderService.NotAcceptOrderByClient(orderId, HttpContext);
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
