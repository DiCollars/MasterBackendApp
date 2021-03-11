using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface ISpecializationService
    {
        Specialization GetSpecialization(int id);

        List<Specialization> GetSpecializations();

        void UpdateSpecialization(Specialization specialization);

        void DeleteSpecialization(int id);

        void CreateSpecialization(Specialization specialization);
    }
}
