using System;

namespace RepositoryContractsDb.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int MasterId { get; set; }

        public DateTime WorkingHours { get; set; }

        public string Status { get; set; }
    }
}
