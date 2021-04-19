using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SoloProject.Models
{
    public class ProductUser
    {
        [Key]
        public int ProductUserId { get; set; }
        public int UserId { get; set; }
        public RegisterUser User { get; set; }
        public int ProductId { get; set; }
        public ProductX Product { get; set; }
    }
}