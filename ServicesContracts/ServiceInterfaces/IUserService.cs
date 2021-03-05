using ServicesContracts.Models;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IUserService
    {
        UserShort GetUserShort(string login);
    }
}
