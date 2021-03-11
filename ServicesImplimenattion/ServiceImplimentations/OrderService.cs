﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthUserService _authUserService;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        public OrderService(IOrderRepository orderRepository, IAuthUserService authUserService, IServiceRepository serviceRepository, IMasterRepository masterRepository, IUserRepository userRepository, IWebHostEnvironment appEnvironment)
        {
            _orderRepository = orderRepository;
            _authUserService = authUserService;
            _serviceRepository = serviceRepository;
            _masterRepository = masterRepository;
            _userRepository = userRepository;
            _appEnvironment = appEnvironment;
        }

        public void FinishedOrderByMaster(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);
            var currentMaster = _masterRepository.GetMasterByUserId(authedUser.Id);

            if (orderForChange.MasterId == currentMaster.Id)
            {
                orderForChange.Status = OrderStatus.FINISHED;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public void NotAgreeOrderByMaster(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);
            var currentMaster = _masterRepository.GetMasterByUserId(authedUser.Id);

            if (orderForChange.MasterId == currentMaster.Id)
            {
                orderForChange.Status = OrderStatus.NOT_AGREE;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public void TakeOrderByMaster(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);
            var currentMaster = _masterRepository.GetMasterByUserId(authedUser.Id);

            if (orderForChange.MasterId == currentMaster.Id)
            {
                orderForChange.Status = OrderStatus.TAKE;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int orderId)
        {
            _orderRepository.DeleteOrder(orderId);
        }

        public List<OrderShort> GetAllOrders()
        {
            var allOrders = _orderRepository.GetOrders();
            var allServices = _serviceRepository.GetServices();
            var allMasters = _masterRepository.GetMasters();
            var allUsers = _userRepository.GetUsers();

            var mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join master in allMasters on order.MasterId equals master.Id
                                join user in allUsers on order.UserId equals user.Id
                                select new OrderShort()
                                {
                                    Id = order.Id,
                                    AddressId = order.AddressId,
                                    Comment = order.Comment,
                                    EndDate = order.EndDate,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();

            return mappedOrders;
        }

        public List<OrderShort> GetAllOrdersForOperator()
        {
            var allOrders = _orderRepository.GetOrdersForOperator();
            var allServices = _serviceRepository.GetServices();
            var allUsers = _userRepository.GetUsers();

            var mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join user in allUsers on order.UserId equals user.Id
                                select new OrderShort()
                                {
                                    Id = order.Id,
                                    AddressId = order.AddressId,
                                    Comment = order.Comment,
                                    EndDate = order.EndDate,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();

            return mappedOrders;
        }

        public List<OrderShort> GetAllOrdersByMasterId(HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var authMaster = _masterRepository.GetMasterByUserId(authedUser.Id);
            List<OrderShort> mappedOrders = null;

            if (authMaster != null)
            {
                var allOrders = _orderRepository.GetOrdersByMasterId(authMaster.Id);
                var allServices = _serviceRepository.GetServices();
                var allUsers = _userRepository.GetUsers();

                mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join user in allUsers on order.UserId equals user.Id
                                select new OrderShort()
                                {
                                    Id = order.Id,
                                    AddressId = order.AddressId,
                                    Comment = order.Comment,
                                    EndDate = order.EndDate,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();
            }

            return mappedOrders;
        }

        public OrderShort GetByOrderId(int id)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderShort>()));
            var order = _orderRepository.GetOrder(id);
            var service = _serviceRepository.GetService(order.ServiceId);
            var user = _userRepository.GetUser(order.UserId);

            var mappedOrder = mapper.Map<OrderShort>(order);
            mappedOrder.ServiceCost = service.Cost;
            mappedOrder.ServiceName = service.Name;
            mappedOrder.MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}";

            return mappedOrder;
        }

        public async Task CreateOrderByClient(Order order, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            order.UserId = authedUser.Id;
            order.Status = OrderStatus.WAIT_OPERATOR;

            var ordersService = _serviceRepository.GetService(order.ServiceId);
            order.EndDate = order.StartDate.AddHours(ordersService.Long);

            _orderRepository.CreateOrder(order);
        }

        public void RejectOrderByClient(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.REJECT;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public void NotDoneOrderByClient(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                if (orderForChange.Status != OrderStatus.FINISHED)
                {
                    orderForChange.Status = OrderStatus.NOT_DONE;
                    _orderRepository.UpdateOrder(orderForChange);
                }
            }
        }

        public void AcceptOrderByClient(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.ACCEPTED;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public void NotAcceptOrderByClient(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.NOT_ACCEPTED;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public List<OrderShort> GetAllOrdersByClientId(HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            List<OrderShort> mappedOrders = null;

            if (authedUser != null)
            {
                var allOrders = _orderRepository.GetOrdersByUserId(authedUser.Id);
                var allServices = _serviceRepository.GetServices();
                var allUsers = _userRepository.GetUsers();
                var allMasters = _masterRepository.GetMasters();

                mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join master in allMasters on order.MasterId equals master.Id
                                join user in allUsers on master.UserId equals user.Id
                                select new OrderShort()
                                {
                                    Id = order.Id,
                                    AddressId = order.AddressId,
                                    Comment = order.Comment,
                                    EndDate = order.EndDate,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();
            }

            return mappedOrders;
        }

        public async Task AddPictureToOrder(IFormFile uploadedFile, int orderId, HttpContext httpContext)
        {
            string path = null;
            var currentOrder = _orderRepository.GetOrder(orderId);
            var authedUser = _authUserService.GetLoggedUser(httpContext);

            if (authedUser.Id == currentOrder.UserId)
            {
                if (uploadedFile != null)
                {
                    path = "\\pictures\\" + uploadedFile.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                }
                if (path != null)
                {
                    currentOrder.Picture = path;

                    _orderRepository.UpdateOrder(currentOrder);
                }
            }
        }

        public (string, string, string) GetPictureFromOrder(int orderId, HttpContext httpContext)
        {
            var order = _orderRepository.GetOrder(orderId);

            var path = "wwwroot" + order.Picture;
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, path);
            file_path = file_path.Replace("/", "\\");

            string file_type = "image/jpeg";

            string pictureName = order.Picture.Split("\\").Last();

            return (file_path, file_type, pictureName);
        }
    }
}