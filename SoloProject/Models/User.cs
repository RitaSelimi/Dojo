using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SoloProject.Models
{
    public class RegisterUser
    {
        [Key]
        public int UserId {get; set;}
        
        [Required]
        [Display(Name="First Name")]
        [MinLength(2, ErrorMessage="First name must be at least 2 characters long")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Last Name")]
        [MinLength(2, ErrorMessage="Last name must be at least 2 characters long")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters long")]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name="Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

        public List<Order> Orders {get; set; }
        public List<Comment> Comments {get; set; }
        public List<ProductX> Products {get; set; }
        public List<ProductUser> Favorites {get; set; }

        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }

    public class LoginUser
    {
        [EmailAddress]
        [Required]
        [Display(Name="Email")]
        public string LoginEmail {get;set;}

        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string LoginPassword {get;set;}
    }
}
