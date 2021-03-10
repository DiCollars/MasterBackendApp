using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface ISpecializationRepository
    {
        void CreateSpecialization(Specialization specialization);

        Specialization GetSpecialization(int id);

        List<Specialization> GetSpecializations();

        void UpdateSpecialization(Specialization specialization);

        void DeleteSpecialization(int id);
    }
}
