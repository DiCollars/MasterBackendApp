namespace RepositoryContractsDb.Models
{
    public class Location
    {
        public int Id { get; set; }

        public int LocationTypeId { get; set; }

        public string LocationName { get; set; }

        public string Coordinates { get; set; }
    }
}
