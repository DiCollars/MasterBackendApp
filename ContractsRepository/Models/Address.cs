using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryContractsDb.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("LocationId")]
        public int LocationId { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Flat { get; set; }

        public Location Location { get; set; }

        public Order Order { get; set; }
    }
}
