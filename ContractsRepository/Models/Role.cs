using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RepositoryContractsDb.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; }

        public AccessRights AccessRight { get; set; }

        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
