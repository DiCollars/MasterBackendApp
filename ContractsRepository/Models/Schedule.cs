using System;

namespace RepositoryContractsDb.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int MasterId { get; set; }

        public DateTime WorkingHoursFrom { get; set; }

        public DateTime WorkingHoursTo { get; set; }

        public string Status { get; set; }
    }
}
