namespace RepositoryContractsDb.Models
{
    public class Address
    {
        public int Id { get; set; }

        public int LocationId { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Flat { get; set; }
    }
}
