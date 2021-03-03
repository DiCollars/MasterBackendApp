using System;

namespace RepositoryContractsDb.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int MasterId { get; set; }

        public int UserId { get; set; }

        public int ServiceId { get; set; }

        public string Decription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string StatusColor { get; set; }

        public string Comment { get; set; }

        public string Picture { get; set; }
    }
}
