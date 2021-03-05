namespace ServicesContracts.ServiceInterfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, string password);
    }
}
