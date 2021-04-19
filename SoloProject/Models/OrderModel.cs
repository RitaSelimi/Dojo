using System.Collections.Generic;

namespace SoloProject.Models
{
    public class OrderModel
    {
        public Order Order { get; set; }
        public List<Order> ListOrders { get; set; }
        public ProductX Product { get; set; }
        public List<ProductX> ListProducts { get; set; }
        public OrderProduct OrderProduct { get; set; }
    }
}