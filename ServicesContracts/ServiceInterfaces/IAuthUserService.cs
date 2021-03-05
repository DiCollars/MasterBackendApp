using Microsoft.AspNetCore.Http;
using ServicesContracts.Models;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IAuthUserService
    {
        UserShort GetLoggedUser(HttpContext httpContext);
    }
}
