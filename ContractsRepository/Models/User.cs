using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryContractsDb.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }
        
        public string Login { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public Role Role { get; set; }

        public Master Master { get; set; }

        public Order Order { get; set; }
    }
}
