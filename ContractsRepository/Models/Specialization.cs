using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepositoryContractsDb.Models
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }

        public string SpecializationName { get; set; }

        public string Icon { get; set; }

        public List<Master> Master { get; set; }
    }
}
