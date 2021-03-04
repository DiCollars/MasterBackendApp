using ServicesContracts.Models;
using System.Threading.Tasks;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IHashPasswordService
    {
        string HashPassword(string password);

        Task<User> Logging(string userName, string userPassword);
    }
}
