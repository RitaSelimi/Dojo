// using System;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Collections.Generic;
// using System.ComponentModel;

// namespace SoloProject.Models
// {
//     public class Product
//     {
//         [Key]
//         public int ProductId {get; set;}
        
//         [MinLength(2, ErrorMessage="Name must be at least 2 characters long")]
//         public string Name { get; set; }
//         public float Price { get; set; }
//         public List<OrderProduct> Orders { get; set; }
//         public List<ProductUser> Users {get; set; }
//     }
// }
