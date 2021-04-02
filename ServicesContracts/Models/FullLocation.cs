using System.Collections.Generic;

namespace ServicesContracts.Models
{
    public class FullLocation
    {
        public int Id { get; set; }

        public string LocationName { get; set; }

        public string Coordinates { get; set; }

        public string Type { get; set; }

        public int? ParentId { get; set; }

        public List<FullLocation> Children { get; set; }
    }
}
