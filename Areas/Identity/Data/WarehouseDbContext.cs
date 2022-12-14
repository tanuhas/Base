using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Text.RegularExpressions;
using WebApplication5.Models;
using WebApplication5.Models.Domain;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication5.Areas.Identity.Data
{
    public class WarehouseDbContext : IdentityDbContext

    {
        public WarehouseDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        // public DbSet<Owner> Departaments { get; set; }
        //public DbSet<Manager> Managers { get; set; }
        //public DbSet<Storekeeper> Storekeepers { get; set; }
        //public DbSet<Packer> Packers { get; set; }
        //public DbSet<Accountant> Accountants { get; set; }
    }
}
