using Microsoft.EntityFrameworkCore;
using RepositoryContractsDb.Models;

namespace RepositoryImplimentationDb
{
    public class Context : DbContext
    {
        public DbSet<Test> Tests { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
