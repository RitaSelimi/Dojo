using Microsoft.EntityFrameworkCore;
namespace SoloProject.Models
{
    public class MyContext : DbContext 
    { 
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<RegisterUser> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<ProductX> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProductUser> ProductUsers { get; set; }
    }
}