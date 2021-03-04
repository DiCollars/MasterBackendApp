using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryContractsDb.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MasterId")]
        public int MasterId { get; set; }

        public DateTime WorkingHoursFrom { get; set; }

        public DateTime WorkingHoursTo { get; set; }

        public string Status { get; set; }

        public Master Master { get; set; }
    }
}
