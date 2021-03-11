using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public void CreateService(Service service)
        {
            _serviceRepository.CreateService(service);
        }

        public void DeleteService(int id)
        {
            _serviceRepository.DeleteService(id);
        }

        public Service GetService(int id)
        {
            return _serviceRepository.GetService(id);
        }

        public List<Service> GetServices()
        {
            return _serviceRepository.GetServices();
        }

        public void UpdateService(Service service)
        {
            _serviceRepository.UpdateService(service);
        }
    }
}
