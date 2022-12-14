using Microsoft.EntityFrameworkCore;
using WebApplication5.Models.Domain;

namespace WebApplication5.Areas.Identity.Data
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
