using RepositoryContractsDb.Models;

namespace RepositoryContractsDb.Contracts
{
    public interface ITestServiceInterface
    {
        Test Get(int id);

        void Create(Test item); 
    }
}
