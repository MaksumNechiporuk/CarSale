using CarSale.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Entities.Models
{


    public class AppUser : IdentityUser
    {



        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        public string Img { get; set; }
        public virtual ICollection<UserCar> UserCar { get; set; }

    }
}
