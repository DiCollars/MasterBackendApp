using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryContractsDb.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public int Long { get; set; }

        public string Specialization { get; set; }

        public List<Order> Order { get; set; }
    }
}
