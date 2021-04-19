using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SoloProject.Models
{
    public class ProductX
    {
        [Key]
        public int ProductId {get; set;}
        
        public string Name {get; set; }
        public double Price {get; set; }
        public List<OrderProduct> Orders { get; set; }
        public List<ProductUser> Users {get; set; }
    }
}
