using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoloProject.Models
{
    public class Order
    {
        [Key]
        public int OrderId {get; set;}
        
        public int CustomerId { get; set; }
        public RegisterUser Customer { get; set; }
        
        [JsonIgnore]
        public List<OrderProduct> Products { get; set; }
        
        public double TotalPrice { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        
        // [Required]
        public bool Status { get; set; } = false;
        [Display(Name="Add Delivery")]
        public bool Delivery { get; set; } = false;
    }
}
