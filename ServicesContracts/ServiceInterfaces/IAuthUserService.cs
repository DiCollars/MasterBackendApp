using Microsoft.AspNetCore.Http;
using ServicesContracts.Models;
using System.Threading.Tasks;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IAuthUserService
    {
        Task<User> GetLoggedUser(HttpContext httpContext);
    }
}
