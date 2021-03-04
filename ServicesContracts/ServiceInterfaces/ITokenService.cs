using System.Threading.Tasks;

namespace ServicesContracts.ServiceInterfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(string username, string password);
    }
}
