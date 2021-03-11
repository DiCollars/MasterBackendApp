using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationService(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        public void DeleteSpecialization(int id)
        {
            _specializationRepository.DeleteSpecialization(id);
        }

        public Specialization GetSpecialization(int id)
        {
            return _specializationRepository.GetSpecialization(id);
        }

        public List<Specialization> GetSpecializations()
        {
            return _specializationRepository.GetSpecializations();
        }

        public void UpdateSpecialization(Specialization specialization)
        {
            _specializationRepository.UpdateSpecialization(specialization);
        }

        public void CreateSpecialization(Specialization specialization)
        {
            _specializationRepository.CreateSpecialization(specialization);
        }
    }
}
