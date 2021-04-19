using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SoloProject.Models
{
    public class EditUser
    {
        public int UserId {get; set;}
        
        [Display(Name="First Name")]
        [MinLength(2, ErrorMessage="First name must be at least 2 characters long")]
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        [MinLength(2, ErrorMessage="Last name must be at least 2 characters long")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters long")]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name="Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }
}
