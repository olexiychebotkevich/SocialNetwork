using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetworkMvc.Models
{
    public class RegisterModelPost
    {
        [Required]
        [EmailAddress(ErrorMessage = "No correct Email!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Country { get; set; }
     
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Name must be shorter than 20 signs and longer than 4s ")]
        [RegularExpression(@"^[A-Z]+(?:[A-Za-z]+)*$", ErrorMessage = "No correct Name")]
        public string Name { get; set; }

        [Required]
        [Range(12, 60, ErrorMessage = "Age should be [12 and 60]")]
        public int Age { get; set; }
    }
}