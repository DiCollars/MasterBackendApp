using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepositoryContractsDb.Models
{
    public class LocationType
    {
        [Key]
        public int Id { get; set; }
        
        public string LocationName { get; set; }

        public List<Location> Location { get; set; }
    }
}
