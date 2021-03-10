using RepositoryContractsDb.Models;

namespace ServicesContracts.Models
{
    public class UserFull
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string RoleName { get; set; }

        public AccessRights AccessRight { get; set; }
    }
}
