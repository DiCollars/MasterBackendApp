using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IServiceService
    {
        Service GetService(int id);

        List<Service> GetServices();

        void UpdateService(Service service);

        void DeleteService(int id);

        void CreateService(Service service);
    }
}
