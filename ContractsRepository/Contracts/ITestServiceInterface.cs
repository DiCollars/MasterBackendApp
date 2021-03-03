using RepositoryContractsDb.Models;

namespace RepositoryContractsDb.Contracts
{
    public interface TestServiceInterface
    {
        Test Get(int id);

        void Create(Test item); 
    }
}
