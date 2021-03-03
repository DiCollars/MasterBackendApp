namespace RepositoryContractsDb.Models
{
    public class Location
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string LocationName { get; set; }

        public string Address { get; set; }

    }
}
