using System;

namespace ServicesContracts.Models
{
    public class OrderShort
    {
        public int Id { get; set; }

        public string MasterFullName { get; set; }

        public string ServiceName { get; set; }

        public decimal ServiceCost { get; set; }

        public string AddressName { get; set; }

        public string Decription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string Icon { get; set; }

        public string StatusColor { get; set; }

        public string Comment { get; set; }

        public string Picture { get; set; }
    }
}
