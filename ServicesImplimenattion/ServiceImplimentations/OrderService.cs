using AutoMapper;
using Microsoft.AspNetCore.Http;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthUserService _authUserService;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IAuthUserService authUserService, IServiceRepository serviceRepository, IMasterRepository masterRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _authUserService = authUserService;
            _serviceRepository = serviceRepository;
            _masterRepository = masterRepository;
            _userRepository = userRepository;
        }

        public void FinishedOrderByMaster(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.FINISHED;
                _orderRepository.UpdateOrder(orderForChange);
            }
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

        public List<OrderShort> GetAllOrdersByMasterId(HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var authMaster = _masterRepository.GetMaster(authedUser.Id);
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

        public void NotAgreeOrderByMaster(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.NOT_AGREE;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public void TakeOrderByMaster(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.TAKE;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }
    }
}
