using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarSale.Entities
{
    [Table("tblUsersCar")]
    public class UserCar
    {
        [ForeignKey("UserOf"), Key]
        public string UserId { get; set; }

        public AppUser UserOf { get; set; }

        [ForeignKey("CarOf"), Key]
        public int CarId { get; set; }
        public Car CarOf { get; set; }

    }
}
