using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class OrderService : IOrderService
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthUserService _authUserService;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IScheduleRepository _scheduleRepository;

        public OrderService(IOrderRepository orderRepository, IAuthUserService authUserService, IServiceRepository serviceRepository, IMasterRepository masterRepository, IUserRepository userRepository, IWebHostEnvironment appEnvironment, ISpecializationRepository specializationRepository, IScheduleRepository scheduleRepository)
        {
            _orderRepository = orderRepository;
            _authUserService = authUserService;
            _serviceRepository = serviceRepository;
            _masterRepository = masterRepository;
            _userRepository = userRepository;
            _appEnvironment = appEnvironment;
            _specializationRepository = specializationRepository;
            _scheduleRepository = scheduleRepository;
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
            var allSpecializations = _specializationRepository.GetSpecializations();

            var mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join master in allMasters on order.MasterId equals master.Id
                                join spec in allSpecializations on master.SpecializationId equals spec.Id
                                join user in allUsers on order.UserId equals user.Id
                                select new OrderShort()
                                {
                                    Id = order.Id,
                                    AddressName = order.Address,
                                    Comment = order.Comment,
                                    EndDate = order.EndDate,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    Icon = spec.Icon,
                                    MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();

            return mappedOrders;
        }

        public List<OrderShort> GetAllOrdersForOperator()
        {
            var allOrders = _orderRepository.GetOrdersForOperator();
            var allServices = _serviceRepository.GetServices();
            var allUsers = _userRepository.GetUsers();
            var allSpecializations = _specializationRepository.GetSpecializations();
            var allMasters = _masterRepository.GetMasters();

            var mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join user in allUsers on order.UserId equals user.Id
                                join master in allMasters on order.MasterId equals master.Id
                                join userMaster in allUsers on master.UserId equals userMaster.Id
                                join spec in allSpecializations on master.SpecializationId equals spec.Id
                                select new OrderShort()
                                {
                                    Id = order.Id,
                                    Comment = order.Comment,
                                    AddressName = order.Address,
                                    EndDate = order.EndDate,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    Icon = spec.Icon,
                                    MasterFullName = $"{userMaster.FirstName} {userMaster.MiddleName} {userMaster.LastName}",
                                    ClientFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();

            var ordersWithoutMasterAndService = allOrders.Where(o => o.MasterId == null && o.ServiceId == null).ToList();

            if (ordersWithoutMasterAndService.Count != 0)
            {
                var mappedOrdersWithoutMasterAndService = (from order in ordersWithoutMasterAndService
                                                           select new OrderShort()
                                                           {
                                                               Id = order.Id,
                                                               AddressName = order.Address,
                                                               Comment = order.Comment,
                                                               EndDate = order.EndDate,
                                                               StartDate = order.StartDate,
                                                               Decription = order.Decription,
                                                               StatusColor = order.StatusColor,
                                                               Status = order.Status,
                                                               Picture = order.Picture
                                                           }).ToList();

                mappedOrders.AddRange(mappedOrdersWithoutMasterAndService);
                mappedOrders = mappedOrders.Distinct().ToList();
            }

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
                var allSpecializations = _specializationRepository.GetSpecializations();
                var allMasters = _masterRepository.GetMasters();

                mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join master in allMasters on order.MasterId equals master.Id
                                join spec in allSpecializations on master.SpecializationId equals spec.Id
                                join user in allUsers on order.UserId equals user.Id
                                select new OrderShort()
                                {
                                    Id = order.Id,
                                    Comment = order.Comment,
                                    EndDate = order.EndDate,
                                    AddressName = order.Address,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    Icon = spec.Icon,
                                    MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();
            }

            return mappedOrders;
        }

        public OrderShort GetByOrderId(int id)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderShort>()));
            var order = _orderRepository.GetOrder(id);

            var mappedOrder = mapper.Map<OrderShort>(order);

            if (order.ServiceId != null)
            {
                var service = _serviceRepository.GetService((int)order.ServiceId);
                mappedOrder.ServiceCost = service.Cost;
                mappedOrder.ServiceName = service.Name;
            }

            if (order.MasterId != null)
            {
                var master = _masterRepository.GetMaster((int)order.MasterId);
                var specialization = _specializationRepository.GetSpecialization(master.SpecializationId);
                var userMaster = _userRepository.GetUser(master.UserId);

                mappedOrder.MasterFullName = $"{userMaster.FirstName} {userMaster.MiddleName} {userMaster.LastName}";
                mappedOrder.Icon = specialization.Icon;
            }
            
            var user = _userRepository.GetUser(order.UserId);
            mappedOrder.ClientFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}";
            mappedOrder.AddressName = order.Address;

            return mappedOrder;
        }

        public int CreateOrderByClient(Order order, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            order.UserId = authedUser.Id;
            order.Status = OrderStatus.WAIT_OPERATOR;

            order.ServiceId = null;
            order.MasterId = null;

            if (DateTime.Compare(order.StartDate.Date, new DateTime(1901, 1, 31)) == 0)
            {
                order.StartDate = new DateTime();
                order.EndDate = new DateTime();
            }
            else
            {
                order.StartDate = new DateTime(order.StartDate.Year, order.StartDate.Month, order.StartDate.Day, order.StartDate.TimeOfDay.Hours, 0, 0);
                order.EndDate = new DateTime();
            }

            var currentOrderId = _orderRepository.CreateOrder(order);

            return currentOrderId;
        }

        public void RejectOrderByClient(int orderId, string comment, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.REJECT;
                orderForChange.Comment = comment;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public void NotDoneOrderByClient(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                if (orderForChange.Status == OrderStatus.FINISHED)
                {
                    orderForChange.Status = OrderStatus.NOT_DONE;
                    _orderRepository.UpdateOrder(orderForChange);
                }
            }
        }

        public void DoneOrderByClient(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                if (orderForChange.Status == OrderStatus.FINISHED)
                {
                    orderForChange.Status = OrderStatus.DONE;
                    _orderRepository.UpdateOrder(orderForChange);
                }
            }
        }

        public void DoneOrderByOperator(int orderId, HttpContext httpContext)
        {
            var orderForChange = _orderRepository.GetOrder(orderId);
            orderForChange.Status = OrderStatus.DONE;
            _orderRepository.UpdateOrder(orderForChange);
        }

        public void SendOrderToClientForAgreeingByOperator(int orderId, HttpContext httpContext)
        {
            var orderForChange = _orderRepository.GetOrder(orderId);
            orderForChange.Status = OrderStatus.WAIT_CLIENT;
            _orderRepository.UpdateOrder(orderForChange);
        }

        public void AcceptOrderByClient(int orderId, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.WAIT_MASTER;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public void NotAcceptOrderByClient(int orderId, string comment, HttpContext httpContext)
        {
            var authedUser = _authUserService.GetLoggedUser(httpContext);
            var orderForChange = _orderRepository.GetOrder(orderId);

            if (orderForChange.UserId == authedUser.Id)
            {
                orderForChange.Status = OrderStatus.NOT_ACCEPTED;
                orderForChange.Comment = comment;
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
                var allSpecializations = _specializationRepository.GetSpecializations();

                mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join master in allMasters on order.MasterId equals master.Id
                                join spec in allSpecializations on master.SpecializationId equals spec.Id
                                join user in allUsers on master.UserId equals user.Id
                                select new OrderShort()
                                {
                                    Id = order.Id,
                                    AddressName = order.Address,
                                    Comment = order.Comment,
                                    EndDate = order.EndDate,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    Icon = spec.Icon,
                                    MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();

                var ordersWithoutMasterAndService = allOrders.Where(o => o.MasterId == null && o.ServiceId == null).ToList();

                if (ordersWithoutMasterAndService.Count != 0)
                {
                    var mappedOrdersWithoutMasterAndService = (from order in ordersWithoutMasterAndService
                                                               select new OrderShort()
                                                               {
                                                                   Id = order.Id,
                                                                   AddressName = order.Address,
                                                                   Comment = order.Comment,
                                                                   EndDate = order.EndDate,
                                                                   StartDate = order.StartDate,
                                                                   Decription = order.Decription,
                                                                   StatusColor = order.StatusColor,
                                                                   Status = order.Status,
                                                                   Picture = order.Picture
                                                               }).ToList();

                    mappedOrders.AddRange(mappedOrdersWithoutMasterAndService);
                }
            }

            return mappedOrders.Distinct().ToList();
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

        public string GetAndConvertPictureToBase64(int orderId)
        {
            var path = GetPictureFromOrder(orderId).Item1;

            using (FileStream fstream = File.OpenRead(path))
            {
                byte[] array = new byte[fstream.Length];

                fstream.Read(array, 0, array.Length);

                string base64 = Convert.ToBase64String(array);

                return base64;
            }
        }

        public (string, string, string) GetPictureFromOrder(int orderId)
        {
            var order = _orderRepository.GetOrder(orderId);

            var path = "wwwroot" + order.Picture;
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, path);
            file_path = file_path.Replace("/", "\\");

            string file_type = "image/jpeg";

            string pictureName = order.Picture.Split("\\").Last();

            return (file_path, file_type, pictureName);
        }

        public void AddMasterAccess(int orderId, HttpContext httpContext)
        {
            var orderForChange = _orderRepository.GetOrder(orderId);
            if (orderForChange.Status == OrderStatus.WAIT_OPERATOR)
            {
                orderForChange.Status = OrderStatus.WAIT_MASTER;
                _orderRepository.UpdateOrder(orderForChange);
            }
        }

        public List<OrderShortWithServLong> GetAllOrdersByMasterIdAndDate(HttpContext httpContext, DateTime date)
        {
            var authUser = _authUserService.GetLoggedUserFull(httpContext);
            var currentMaster = _masterRepository.GetMasterByUserId(authUser.Id);
            List<OrderShortWithServLong> mappedOrders = null;

            if (currentMaster != null)
            {
                var allOrders = _orderRepository.GetAllOrdersByMasterIdAndDate(currentMaster.Id, date);
                var allServices = _serviceRepository.GetServices();
                var allUsers = _userRepository.GetUsers();
                var allSpecializations = _specializationRepository.GetSpecializations();
                var allMasters = _masterRepository.GetMasters();

                mappedOrders = (from order in allOrders
                                join service in allServices on order.ServiceId equals service.Id
                                join master in allMasters on order.MasterId equals master.Id
                                join spec in allSpecializations on master.SpecializationId equals spec.Id
                                join user in allUsers on order.UserId equals user.Id
                                select new OrderShortWithServLong()
                                {
                                    Id = order.Id,
                                    Comment = order.Comment,
                                    EndDate = order.EndDate,
                                    AddressName = order.Address,
                                    StartDate = order.StartDate,
                                    Decription = order.Decription,
                                    StatusColor = order.StatusColor,
                                    Status = order.Status,
                                    Picture = order.Picture,
                                    ServiceCost = service.Cost,
                                    ServiceName = service.Name,
                                    Icon = spec.Icon,
                                    Long = service.Long,
                                    MasterFullName = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                                }).ToList();

                var ordersWithoutMasterAndService = allOrders.Where(o => o.MasterId == null && o.ServiceId == null).ToList();

                if (ordersWithoutMasterAndService.Count != 0)
                {
                    var mappedOrdersWithoutMasterAndService = (from order in ordersWithoutMasterAndService
                                                               select new OrderShortWithServLong()
                                                               {
                                                                   Id = order.Id,
                                                                   AddressName = order.Address,
                                                                   Comment = order.Comment,
                                                                   EndDate = order.EndDate,
                                                                   StartDate = order.StartDate,
                                                                   Decription = order.Decription,
                                                                   StatusColor = order.StatusColor,
                                                                   Status = order.Status,
                                                                   Picture = order.Picture
                                                               }).ToList();

                    mappedOrders.AddRange(mappedOrdersWithoutMasterAndService);
                }
            }

            return mappedOrders.Distinct().ToList();
        }
    }
}
