using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System.Linq;

namespace RepositoryImplimentationDb.ContractImplimentation
{
    public class TestService : TestServiceInterface
    {
        private readonly Context _context;

        public TestService(Context context)
        {
            _context = context;
        }

        public void Create(Test item)
        {
            _context.Tests.Add(item);
            _context.SaveChanges();
        }

        public Test Get(int id)
        {
            return _context.Tests.First(testc => testc.Id == id);
        }
    }
}
