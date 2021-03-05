using ServicesContracts.Models;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IHashPasswordService
    {
        string HashPassword(string password);

        UserShort Logging(string userName, string userPassword);
    }
}
