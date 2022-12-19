using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Text.RegularExpressions;
using WebApplication5.Models;
using WebApplication5.Models.Domain;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication5.Areas.Identity.Data
{
    public class WarehouseDbContext : IdentityDbContext<User>
    {
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Owner> Departaments { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Storekeeper> Storekeepers { get; set; }
        public DbSet<Packer> Packers { get; set; }
        public DbSet<Accountant> Accountants { get; set; }
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
