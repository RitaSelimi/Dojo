using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SoloProject.Models
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public ProductX Product { get; set; }
    }
}