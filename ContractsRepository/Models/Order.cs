using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryContractsDb.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MasterId")]
        public int MasterId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("ServiceId")]
        public int ServiceId { get; set; }
        
        [ForeignKey("AddressId")]
        public int AddressId { get; set; }

        public string Decription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string StatusColor { get; set; }

        public string Comment { get; set; }

        public string Picture { get; set; }

        public Master Master { get; set; }

        public User User { get; set; }

        public Address Address { get; set; }

        public Service Service { get; set; }
    }
}
