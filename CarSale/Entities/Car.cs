using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarSale.Entities
{


    [Table("tblCars")]
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string UniqueName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Decimal Price { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Required]
        public string State { get; set; }
        public virtual ICollection<Filter> Filtres { get; set; }
    }
}
