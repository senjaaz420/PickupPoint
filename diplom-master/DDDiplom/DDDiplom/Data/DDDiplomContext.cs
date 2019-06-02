using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DDDiplom.Models
{
    public class DDDiplomContext : DbContext
    {
        public DDDiplomContext (DbContextOptions<DDDiplomContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WorkPlace> WorkPlaces { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set;  }
        public DbSet<Role> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-ITJHB8V\\SQLEXPRESS; Database = DDDiplomContext; Trusted_Connection = True;");
        }
    }
}


