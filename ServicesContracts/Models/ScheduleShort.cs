using System;

namespace ServicesContracts.Models
{
    public class ScheduleShort
    {
        public int MasterId { get; set; }

        public DateTime WorkingHoursFrom { get; set; }

        public DateTime WorkingHoursTo { get; set; }

        public string Status { get; set; }
    }
}
