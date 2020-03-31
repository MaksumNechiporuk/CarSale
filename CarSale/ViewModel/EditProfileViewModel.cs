using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarSale.ViewModel
{
    public class EditProfileViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "City can`t less 5 chareccters.")]
        public string City { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Street can`t less 5 chareccters.")]
        public string Country { get; set; }

        public IFormFile Img { get; set; }

        public string ExistImgPath { get; set; }

    }
}
