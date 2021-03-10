using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IServiceRepository
    {
        void CreateService(Service service);

        Service GetService(int id);

        List<Service> GetServices();

        void UpdateService(Service service);

        void DeleteService(int id);
    }
}
