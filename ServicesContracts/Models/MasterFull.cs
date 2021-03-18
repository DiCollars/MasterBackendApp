using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace ServicesContracts.Models
{
    public class MasterFull
    {
        public int Id { get; set; }

        public string MasterFullName { get; set; }

        public int SpecializationId { get; set; }

        public string SpecializationName { get; set; }

        public string Icon { get; set; }

        public int MainLocationId { get; set; }

        public List<Location> Locations { get; set; }
    }
}
