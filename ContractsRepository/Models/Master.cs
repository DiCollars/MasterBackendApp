using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryContractsDb.Models
{
    public class Master
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("LocationId")]
        public int LocationId { get; set; }

        [ForeignKey("SpecializationId")]
        public int SpecializationId { get; set; }

        public User User { get; set; }

        public Location Location { get; set; }

        public Specialization Specialization { get; set; }

        public List<Order> Order { get; set; }

        public Schedule Schedule { get; set; }
    }
}
