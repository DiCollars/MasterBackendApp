using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryContractsDb.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("LocationTypeId")]
        public int LocationTypeId { get; set; }

        public string LocationName { get; set; }

        public string Coordinates { get; set; }

        public List<Master> Master { get; set; }

        public LocationType LocationType { get; set; }

        public Address Address { get; set; }
    }
}
