namespace RepositoryContractsDb.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public int Long { get; set; }

        public int SpecializationId { get; set; }
    }
}
